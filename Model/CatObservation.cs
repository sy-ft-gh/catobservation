using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using cat.DB;

namespace cat.Model {

    /// <summary>
    /// Cat Gender Type Define
    /// </summary>
    public enum CatObservateGenderType : byte {
        Unknown = 0,
        Male = 1,
        Female = 2
    };

    /// <summary>
    /// Cat Body Type Define
    /// </summary>
    public enum CatObservateBodyType : byte {
        Small = 1,
        Midium = 2,
        Large = 3,
        Huge = 4
    }

    /// <summary>
    /// Cat Age Define
    /// </summary>
    public enum CatObservateAge : byte {
        Unknown = 0,
        Baby = 1,
        Young = 2,
        Adult = 3,
        Old = 4
    }
    public class CatObservation {
        /// <summary>
        /// Observate Time's Date Value 
        /// [1753/1/1] Is Minimum Date on SQL Server
        /// </summary>
        [NotMapped]
        public static readonly DateTime OBSERVATE_TIMES_DATE_VALUE = new DateTime(1753, 1, 1, 0, 0, 0);
        /// <summary>
        /// ObservateDate Use Date Value
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public DateTime? ObservateDate { get; set; }
        /// <summary>
        /// ObjservateTime Use Time Value
        /// 
        /// Date Value = OBSERVATE_TIMES_DATE_VALUE, Time Value = User Entry
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public DateTime? ObservateTime { get; set; }
        /// <summary>
        /// Cat Identify
        /// </summary>
        [Key]
        [Column(Order = 3)]
        public int? CatId { get; set; }
        /// <summary>
        /// Cat Name
        /// </summary>
        [MaxLength(50, ErrorMessage = "Enter the Cat Name within 50 Bytes")]
        public string CatName { get; set; }
        /// <summary>
        /// HairPattern Name
        /// </summary>
        [MaxLength(50, ErrorMessage = "Enter the Hair Pattern within 50 Bytes")]
        public string HairPattern { get; set; }
        /// <summary>
        /// Gender Type
        /// <seealso cref="CatObservateGenderType"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatObservateGenderType.Unknown)]
        public CatGenderType Gender { get; set; }
        /// <summary>
        /// Gender Type
        /// <seealso cref="CatObservateBodyType"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatObservateBodyType.Small)]
        public CatBodyType BodyType { get; set; }
        /// <summary>
        /// FaceType Name
        /// </summary>
        [MaxLength(100, ErrorMessage = "Enter the Face Type within 100 bytes")]
        public string FaceType { get; set; }
        /// <summary>
        /// Age 
        /// <seealso cref="CatObservateAge"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatObservateAge.Unknown)]
        public CatAge Age { get; set; }
        /// <summary>
        /// Personal Note
        /// </summary>
        [MaxLength(200, ErrorMessage = "Enter the Personality within 200 Bytes")]
        public string Personality { get; set; }
        /// <summary>
        /// Country Name
        /// </summary>
        [MaxLength(75, ErrorMessage = "Enter the Country within 75 Bytes")]
        public string Country { get; set; }
        /// <summary>
        /// Area Name
        /// </summary>
        [MaxLength(250, ErrorMessage = "Enter the Area within 250 Bytes")]
        public string Area { get; set; }

        /// <summary>
        /// Regist DateTime
        /// </summary>
        [DefaultValue(DefaultValue = "GETDATE()")]
        public DateTime RegistDate { get; set; }
        /// <summary>
        /// LastUpdate DateTime
        /// </summary>
        [DefaultValue(DefaultValue = "GETDATE()")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Foreign Key to Cat
        /// </summary>
        [ForeignKey("CatId")]
        public Cat CatMaster { get; set; }

        /// <summary>
        /// Copy Instance
        /// </summary>
        /// <param name="src">Src Instance</param>
        public void CopyFrom(CatObservation src) {
            this.ObservateDate = src.ObservateDate;
            this.ObservateTime = src.ObservateTime;
            this.CatId = src.CatId;
            this.CatName = src.CatName;
            this.HairPattern = src.HairPattern;
            this.Gender = src.Gender;
            this.BodyType = src.BodyType;
            this.FaceType = src.FaceType;
            this.Age = src.Age;
            this.Personality = src.Personality;
            this.Country = src.Country;
            this.Area = src.Area;
            this.RegistDate = src.RegistDate;
            this.UpdateDate = src.UpdateDate;
        }
    }
    /// <summary>
    /// Cat Data for Display
    /// </summary>
    [NotMapped]
    public class CatObservationDisplay : CatObservation {
        /// <summary>
        /// Return Gender Type Name
        /// </summary>
        public string GenderText {
            get {
                switch (this.Gender) {
                    case CatGenderType.Male:
                        return "Male";
                    case CatGenderType.Female:
                        return "Female";
                    default:
                        return "Unknown";
                }
            }
        }
        /// <summary>
        /// Return Body Type Name
        /// </summary>
        public string BodyTypeText {
            get {
                switch (this.BodyType) {
                    case CatBodyType.Small:
                        return "Small";
                    case CatBodyType.Midium:
                        return "Midium";
                    case CatBodyType.Large:
                        return "Large";
                    default:
                        return "Huge";
                }
            }
        }
        /// <summary>
        /// Return Age Category Name
        /// </summary>
        public string AgeText {
            get {
                switch (this.Age) {
                    case CatAge.Baby:
                        return "Baby";
                    case CatAge.Young:
                        return "Young";
                    case CatAge.Adult:
                        return "Adult";
                    case CatAge.Old:
                        return "Old";
                    default:
                        return "Unknown";
                }
            }
        }
        /// <summary>
        /// Copy Instance
        /// 
        /// Notice: Is Not Deep Clone
        /// </summary>
        public CatObservationDisplay Clone() {
            return (CatObservationDisplay)MemberwiseClone();
        }

    }
}
