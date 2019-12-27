using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

using cat.Model;

namespace cat.View {
    /// <summary>
    /// ObservateDate Filter Type Define
    /// </summary>
    public enum ObservateDateFilterType : byte {
        Up = 0,
        Down = 1,
    };


    public class CatsViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = null;
        /// <summary>
        /// Cat Mst Edit Data Field
        /// </summary>
        private CatObservationDisplay editData;
        /// <summary>
        /// Cat Observation List Data Field
        /// </summary>
        private List<CatObservationDisplay> codList;
        /// <summary>
        /// Selected OvservationDate Filter 
        /// </summary>
        private ObservateDateFilterType selObservateDateFilter;

        /// <summary>
        /// Get Propaty's Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="propertyName">Property Name</param>
        /// <returns>Custom Attribute's</returns>
        public static T GetPropertyAttribute<T>(Type clsType, string propertyName) where T : Attribute {
            var attrType = typeof(T);
            var property = clsType.GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }

        /// <summary>
        /// Entry Field ObservateDate I/F
        /// </summary>
        public DateTime? ObservateDate {
            get { return editData.ObservateDate; }
            set {
                editData.ObservateDate = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(ObservateDate));
            }
        }
        /// <summary>
        /// Entry Field ObservateTime I/F
        /// </summary>
        public DateTime? ObservateTime {
            get { return editData.ObservateTime; }
            set {
                editData.ObservateTime = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(ObservateTime));
            }
        }
        /// <summary>
        /// Cat Mst List Data Field
        /// </summary>
        public ObservableCollection<Cat> CatMasters { get; set; }

        /// <summary>
        /// Entry Field CatId I/F
        /// </summary>
        public int? CatId { 
            get { return editData.CatId; }
            set {
                editData.CatId = value ;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatId));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string CatName {
            get { return editData.CatName; }
            set {
                editData.CatName = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatName));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string HairPattern {
            get { return editData.HairPattern; }
            set {
                editData.HairPattern = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(HairPattern));
            }
        }
        /// <summary>
        /// Entry Field Gender I/F
        /// </summary>
        public CatGenderType Gender {
            get { return editData.Gender; }
            set {
                editData.Gender = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// Entry Field BodyType I/F
        /// </summary>
        public CatBodyType BodyType {
            get { return editData.BodyType; }
            set {
                editData.BodyType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(BodyType));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string FaceType {
            get { return editData.FaceType; }
            set {
                editData.FaceType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(FaceType));
            }
        }
        /// <summary>
        /// Entry Field Age I/F
        /// </summary>
        public CatAge Age {
            get { return editData.Age; }
            set {
                editData.Age = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Age));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string Personality {
            get { return editData.Personality; }
            set {
                editData.Personality = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Personality));
            }
        }
        /// <summary>
        /// Entry Field Country I/F
        /// </summary>
        public string Country { 
            get { return editData.Country; }
            set {
                editData.Country = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Country));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string Area { 
            get { return editData.Area; }
            set {
                editData.Area = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Area));
            }
        }

        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public ObservateDateFilterType ObservateDateFilter {
            get { return selObservateDateFilter; }
            set {
                selObservateDateFilter = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(ObservateDateFilterType));
            }
        }
        


        /// <summary>
        /// Editing Field Datas I/F
        /// </summary>
        public CatObservationDisplay CatEdit { 
            get { return editData; }
            set {
                this.editData = value.Clone();
                //Post Change Event  
                this.OnPropertyChanged(nameof(ObservateDate));
                this.OnPropertyChanged(nameof(ObservateTime));
                this.OnPropertyChanged(nameof(CatId));
                this.OnPropertyChanged(nameof(CatName));
                this.OnPropertyChanged(nameof(HairPattern));
                this.OnPropertyChanged(nameof(FaceType));
                this.OnPropertyChanged(nameof(Personality));
                this.OnPropertyChanged(nameof(Gender));
                this.OnPropertyChanged(nameof(BodyType));
                this.OnPropertyChanged(nameof(Age));
                this.OnPropertyChanged(nameof(Country));
                this.OnPropertyChanged(nameof(Area));
            }
        }
        /// <summary>
        /// Display Cat Master Date items in input area 
        /// </summary>
        /// <param name="master"></param>
        public void CatMasterToEdit(Cat master) { 
            // TODO: Make Code(Master Data Items into Input Area)
        
        }

        /// <summary>
        /// Grid Data Items　I/F
        /// </summary>
        public void SetCatMasters(List<Cat> cats) {
            this.CatMasters.Clear();
            this.CatMasters.Add(new Cat() { CatId = -1, CatName = "未選択" });
            foreach (var cat in cats)
                this.CatMasters.Add(cat);
            this.OnPropertyChanged(nameof(CatMasters));
            
            this.CatId = -1;
        }

        /// <summary>
        /// Grid Data Items　I/F
        /// </summary>
        public List<CatObservationDisplay> CatObservations {
            get {
                return codList;
            }
            set {
                this.codList = new List<CatObservationDisplay>(value);

                //Post Change Event 
                this.OnPropertyChanged(nameof(CatObservations));
            }
        }
        /// <summary>
        /// All clear Editing Fields 
        /// </summary>
        public void ClearEdit() {
            this.CatEdit = new CatObservationDisplay { Gender = CatGenderType.Unknown, BodyType = CatBodyType.Small, Age = CatAge.Unknown };
        }
        public void CopyEdit() {
            /// Clear ViewData's CatId <see cref="Model.Cat.CatId"/>
            this.CatId = null;
        }

        /// <summary>
        /// Validate And Get Entry Data
        /// </summary>
        /// <returns>Succeed-> Edited Data, Failure -> null </returns>
        public CatObservation ValidateEntry() {
        	// TODO: Make Code(Add Validate)
        	/* Length Check Sample
            var CatNameProp = GetPropertyAttribute<MaxLengthAttribute>(typeof(CatObservation), nameof(CatObservation.CatName));
            if (!string.IsNullOrEmpty(this.editData.CatName) && Encoding.Unicode.GetByteCount(this.editData.CatName) > CatNameProp.Length) {
                MessageBox.Show(CatNameProp.ErrorMessage, "Cat Observation",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }        	
        	*/


            return GetEntry();
        }

        /// <summary>
        /// Get Entry Data
        /// </summary>
        public CatObservation GetEntry() {
            CatObservation result = this.editData.Clone();
            /// Calculate ObservateTime <see cref="CatObservation.ObservateTime"/>
            if (result.ObservateTime != null) {
                result.ObservateTime = CatObservation.OBSERVATE_TIMES_DATE_VALUE.Date + ((DateTime)result.ObservateTime).TimeOfDay;
            }
            return result;
        }

        public void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public CatsViewModel() {
            // Data Init
            ClearEdit();
            this.codList = null;
            this.CatMasters = new ObservableCollection<Cat>();
            this.ObservateDate = DateTime.Today;
            this.selObservateDateFilter = ObservateDateFilterType.Up;
        }
        protected void OnPropertyChanged(string info) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
