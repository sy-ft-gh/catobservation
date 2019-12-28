using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Windows;

using log4net;

using cat.View;
using cat.DB;
using cat.Model;

namespace cat {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// View Model
        /// </summary>
        private CatsViewModel vm = new CatsViewModel();

        /// <summary>
        /// DB Context
        /// </summary>
        private CatObservationContext CatCntext = new CatObservationContext();

        /// <summary>
        /// Loaded Cats Data
        /// </summary>
        List<Cat> CatMaster;

        /// <summary>
        /// Last Observate Filter Setting
        /// </summary>
        private CatObservation LastSearchFilter = new CatObservation();

        /// <summary>
        /// Get Log4j Logger
        /// Initialize from Self Class Info
        /// </summary>
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            this.DataContext = vm;
            this.GetCatMasters();
            // Filter Button Execute
            btnFilter_Click(btnFilter, new RoutedEventArgs());
        }

        /// <summary>
        /// Show Cat Observation Data to Editing 
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void dgCatObservation_CurrentCellChanged(object sender, EventArgs e) {
            int RowCnt = dgCatObservation.Items.IndexOf(dgCatObservation.CurrentItem); 
            if (RowCnt >= 0 && vm.CatEdit.CatId != vm.CatObservations[RowCnt].CatId) {
                vm.CatEdit = vm.CatObservations[RowCnt];
            }
        }

        /// <summary>
        /// Clear Entry
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnErase_Click(object sender, RoutedEventArgs e) {
            vm.ClearEdit();
        }

        /// <summary>
        /// Setup Copy Entry
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnCopy_Click(object sender, RoutedEventArgs e) {
            vm.CopyEdit();
        }

        /// <summary>
        /// Regist or Update Cat Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnApply_Click(object sender, RoutedEventArgs e) {
            // 0.PreProcessing(Validattion)
            CatObservation cat = null;
            if ((cat = vm.ValidateEntry()) == null) return;

            /// <see cref="Model.Cat.CatId"/>
            // 1.Start Trans
            using (var dbTransaction = CatCntext.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {
                string ErrorMessage = null;
                try {
                    // 2.Reg or Upd Jdg (ObservateDate & CatId Exists )
                    var exst = CatCntext.CatObservations.FirstOrDefault(x => x.ObservateDate == cat.ObservateDate && x.ObservateTime == cat.ObservateTime &&  x.CatId == cat.CatId);
                    // 3.Execute
                    if (exst is null) {
                        // 3.1 Cat Master Exists Check

                        // Generate Error if there is no Master data 

                        // 3.2 Regist
                        // Insert Record
                        CatObservation ins = new CatObservation();
                        ins.CopyFrom(cat);
                        ins.UpdateDate = CatCntext.GetDBDate();
                        ins.RegistDate = ins.UpdateDate;
                        CatCntext.CatObservations.Add(ins);
                        CatCntext.SaveChanges();
                    } else {
                        // 3.2 Update
                        // Select And Lock Registed Data
                        var RegistedData = CatCntext.Database.SqlQuery<CatObservation>("SELECT * FROM CATOBSERVATIONS WITH (UPDLOCK) WHERE OBSERVATEDATE = '" + ((DateTime)cat.ObservateDate).ToShortDateString() + "' AND OBSERVATETIME = '" + ((DateTime)cat.ObservateTime).ToString() + "' AND CATID = " + cat.CatId.ToString()).ToList();
                        // 3.2.1 Is Not Data
                        if (RegistedData.Count != 1) {
                            // Exit for Show deleted error
                            ErrorMessage = "This Data is Not Exists";
                        } else {
                            // 3.2.2 Is Data
                            // Check Update Time Is Modified
                            if (cat.UpdateDate > RegistedData.First().UpdateDate) {
                                // Is Modified -> Over Write Confirm
                                if (MessageBox.Show("Is Already UPDATED. Wanna Over Write??", "Cat Observation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;
                            }
                            // Is Not Modified or Over Write -> Update Data
                            exst.CopyFrom(cat);
                            exst.UpdateDate = CatCntext.GetDBDate();
                            CatCntext.SaveChanges();
                        }
                    }
                } catch (Exception ex) {
                    ErrorMessage = "Error Rized:" + ex.Message;
                } finally {
                    // 4-5.End Trans & Post Process
                    // Succeed -> Commit, Reload & Init UI
                    // Failue -> Rollback, Show Error Message
                    if (string.IsNullOrEmpty(ErrorMessage)) {
                        dbTransaction.Commit();
                        this.GetCatObservations(LastSearchFilter);
                        vm.ClearEdit();
                    } else {
                        dbTransaction.Rollback();
                        MessageBox.Show(ErrorMessage, "Cat Observation", MessageBoxButton.OK, MessageBoxImage.Error);
                        // TODO: MakeCode (Error Log Output (Eventlog))
                    }
                }
            }
        }

        /// <summary>
        /// Delete Cat Mst Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            // TODO: MekeCode )Output log4netlog (Method in/out))

            CatObservation cat = null;
            // 0.PreProcessing(Validattion)
            // Is there Selected ?
            if (!(vm.CatId > 0) || (cat = vm.ValidateEntry()) == null) {
                MessageBox.Show("Please Select A Record", "Cat Observation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 1.Start Trans
            using (var dbTransaction = CatCntext.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {
                string ErrorMessage = null;
                try {
                    // 2.Execute
                    // 2.1 Delete Check
                    // Select And Lock Registed Data
                    var RegistedData = CatCntext.Database.SqlQuery<Cat>("SELECT * FROM CATOBSERVATIONS WITH (UPDLOCK) WHERE OBSERVATEDATE = '" + ((DateTime)vm.ObservateDate).ToLongDateString() + "' AND OBSERVATETIME = '" + ((DateTime)vm.ObservateTime).ToString() + "' AND CATID = " + vm.CatId.ToString()).ToList();

                    // 2.2.1 Is Not Data
                    // Exit for Show deleted error
                    if (RegistedData.Count == 0) {
                        ErrorMessage = "Selected Data Is Deleted.";
                    } else {
                        // 2.2.2 Is Data
                        var tgt = CatCntext.Cats.Single(x => x.CatId == vm.CatId);
                        // Check Update Time Is Modified
                        if (cat.UpdateDate < tgt.UpdateDate) {
                            // Is Modified -> Confirm Delete (Continue or stop)
                            if (MessageBox.Show("Is Already UPDATED. Wanna Force Delete??", "Cat Observation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;
                        }
                        // Is Not Modified or Force Delete
                        CatCntext.Cats.Remove(tgt);
                        CatCntext.SaveChanges();
                    }
                } catch (Exception ex) {
                    ErrorMessage = "Error Rized:" + ex.Message;
                } finally {
                    // 3-4.End Trans & Post Process
                    // Succeed -> Commit, Reload & Init UI
                    // Failue -> Rollback, Show Error Message
                    if (string.IsNullOrEmpty(ErrorMessage)) {
                        dbTransaction.Commit();
                        this.GetCatObservations(LastSearchFilter);
                        vm.ClearEdit();
                    } else {
                        dbTransaction.Rollback();
                        MessageBox.Show(ErrorMessage, "Cat Observation", MessageBoxButton.OK, MessageBoxImage.Error);
                        // TODO: MakeCode (Error Log Output (log4net))
                    }
                }
            }

        }
        /// <summary>
        /// Select Filtered Cat Observation Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        /// <remarks> Get Filter Args from Input Area </remarks>
        private void btnFilter_Click(object sender, RoutedEventArgs e) {
            CatObservation input = vm.GetEntry();
            LastSearchFilter.ObservateDate = input.ObservateDate;
            LastSearchFilter.ObservateTime = input.ObservateTime;
            // Arange Filter (if you want to do it)

            GetCatObservations(LastSearchFilter);
        }

        /// <summary>
        /// Put Selected Cat Data to Input Area
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void cbCatId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            // TODO: Make Code(Call CatMasterToEdit)
        }

        /// <summary>
        /// Save Cat Observation List File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSave_Click(object sender, RoutedEventArgs e) {
            // TODO: Make Code(Save vm.CatObservations)

            // 1.Save File Name collect
            // Call SaveFileDialog
            try {
                // 2. File Save
                // 2.1 Create StreamWriter
                // 2.2 Each Grid Datas Write
            } catch (SystemException ex) {
                // -> Save Error log

                // -> Error Message Show
            }
        }

        /// <summary>
        /// Get Cat Master List
        /// </summary>
        private void GetCatMasters() {
            CatMaster = CatCntext.Cats.Where(x => x.LostFlag == null).OrderBy(x => x.CatId).ToList();
            vm.SetCatMasters(CatMaster);
        }
        /// <summary>
        /// Get Cat Observation List
        /// </summary>
        private void GetCatObservations(CatObservation filter) {
            // TODO: MakeCode (Add Order By Switch)
            List < CatObservationDisplay > dspList = new List<CatObservationDisplay>();
            var query = from co in CatCntext.CatObservations
                        orderby co.ObservateDate, co.ObservateTime, co.CatId
                        select co;
            foreach (var cat in query) {
                CatObservationDisplay dsp = new CatObservationDisplay();
                dsp.CopyFrom(cat);
                dspList.Add(dsp);
            }

            vm.CatObservations = dspList; 
        }
        /// <summary>
        /// Write Windows EventLog Message
        /// </summary>
        /// <param name="logString"> Log Text </param>
        /// <param name="ErrorOrInfo"> </param>
        private void WriteEventLog(string logString, bool ErrorOrInfo) {
            // Write Entry
            EventLog.WriteEntry(
                "Cat", "CatObservation:r" + logString,
                ErrorOrInfo ? EventLogEntryType.Error : EventLogEntryType.Information);
        }
    }
}
