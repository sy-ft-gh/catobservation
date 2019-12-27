using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using cat.DB;

namespace cat.Model {

    /// <summary>
    /// Cat Gender Type Define
    /// </summary>
    public enum CatGenderType: byte {
        Unknown = 0,
        Male = 1,
        Female = 2
    };

    /// <summary>
    /// Cat Body Type Define
    /// </summary>
    public enum CatBodyType : byte {
        Small = 1,
        Midium = 2,
        Large = 3,
        Huge = 4
    }

    /// <summary>
    /// Cat Age Define
    /// </summary>
    public enum CatAge : byte {
        Unknown = 0,
        Baby = 1,
        Young = 2,
        Adult = 3,
        Old = 4
    }

    /// <summary>
    /// Cat Master Data
    /// </summary>
    public class Cat {
        /// <summary>
        /// Cat Identify
        /// Is Null or eq 0 → Newly Entry
        /// gt 0 → Registed Data
        /// 
        /// bacause Identity is 1 or Higher
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// <seealso cref="CatGenderType"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatGenderType.Unknown)]
        public CatGenderType Gender { get; set; }
        /// <summary>
        /// Gender Type
        /// <seealso cref="CatBodyType"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatBodyType.Small)]
        public CatBodyType BodyType { get; set; }

        /// <summary>
        /// FaceType Name
        /// </summary>
        [MaxLength(100, ErrorMessage = "Enter the Face Type within 100 bytes")]
        public string FaceType { get; set; }
        /// <summary>
        /// Age 
        /// <seealso cref="CatAge"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(DefaultValue = (int)CatAge.Unknown)]
        public CatAge Age { get; set; }
        /// <summary>
        /// Personal Note
        /// </summary>
        [MaxLength(200, ErrorMessage = "Enter the Personality within 50 Bytes")]
        public string Personality { get; set; }
        /// <summary>
        /// Lost Status
        /// </summary>
        [MaxLength(1)]
        public string LostFlag { get; set; }
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
        /// Copy Instance
        /// </summary>
        /// <param name="src">Src Instance</param>
        public void CopyFrom(Cat src) {
            this.CatId = src.CatId;
            this.CatName = src.CatName;
            this.HairPattern = src.HairPattern;
            this.Gender = src.Gender;
            this.BodyType = src.BodyType;
            this.FaceType = src.FaceType;
            this.Age = src.Age;
            this.Personality = src.Personality;
            this.LostFlag = src.LostFlag;
            this.RegistDate = src.RegistDate;
            this.UpdateDate = src.UpdateDate;
        }
    }
    /// <summary>
    /// Cat Data for Display
    /// </summary>
    [NotMapped]
    public class CatDisplay : Cat {

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
        /// Return Lost Flag's Category Text
        /// </summary>
        public string LostFlagText {
            get {
                if (string.IsNullOrEmpty(this.LostFlag)) {
                    return string.Empty;
                } else {
                    return "*";
                }
            }
        }
        /// <summary>
        /// Copy Instance
        /// 
        /// Notice: Is Not Deep Clone
        /// </summary>
        public CatDisplay Clone() {
            return (CatDisplay)MemberwiseClone();
        }

    }
}
