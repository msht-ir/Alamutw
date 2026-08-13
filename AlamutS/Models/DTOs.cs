using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AlamutS.Models
    {
    //BaseInfo
    public class User
        {
        public int UserId { get; set; } = 0;
        public string UserNickname { get; set; } = "";
        public string Username { get; set; } = "";
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public DateTimeOffset? ExpDateTime { get; set; } = new DateTimeOffset ();
        public int UserTags { get; set; } = 0;
        public bool IsActive
            {
            get => (UserTags & 1) == 1;
            set
                {
                if (value)
                    UserTags |= 1;
                else
                    UserTags &= ~1;
                }
            }
        public bool UseFaCalendar
            {
            get => (UserTags & 128) == 2;
            set
                {
                if (value)
                    UserTags |= 2;
                else
                    UserTags &= ~2;
                }
            }
        public bool SilverAcc
            {
            get => (UserTags & 4) == 4;
            set
                {
                if (value)
                    UserTags |= 4;
                else
                    UserTags &= ~4;
                }
            }
        public bool GoldAcc
            {
            get => (UserTags & 8) == 8;
            set
                {
                if (value)
                    UserTags |= 8;
                else
                    UserTags &= ~8;
                }
            }
        public bool AdminAcc
            {
            get => (UserTags & 16) == 16;
            set
                {
                if (value)
                    UserTags |= 16;
                else
                    UserTags &= ~16;
                }
            }
        public List<Set> StudentGroups { get; set; } = new List<Set> ();
        public List<Dataset> StudentCourses { get; set; } = new List<Dataset> ();
        public List<Note> StudentNotes { get; set; } = new List<Note> ();
        }
    public class Setting
        {
        public string SettingKey { get; set; } = "";
        public string SettingValue { get; set; } = "";
        }
    public class Country
        {
        public int CountryId { get; set; } = 0;
        public string CountryName { get; set; } = "";
        }
    public class Province
        {
        public int ProvinceId { get; set; } = 0;
        public int CountryId { get; set; } = 0;
        public string ProvinceName { get; set; } = "";
        public List<Dataset> Datasets { get; set; } = new List<Dataset> ();
        }
    //Taxonomy
    public class ApgGroup
        {
        public int ApgGroupId { get; set; } = 0;
        public int ApgGroupSeq { get; set; } = 0;
        public string ApgGroupName { get; set; } = "";
        public List<Order> Orders { get; set; } = new List<Order> ();
        }
    public class Order
        {
        public int OrderId { get; set; } = 0;
        public int ApgGroupId { get; set; } = 0;
        public string OrderName { get; set; } = "";
        public int OrderSequence { get; set; } = 0;
        public string OrderNote { get; set; } = "";
        }
    public class Family
        {
        public int FamilyId { get; set; } = 0;
        public int OrderId { get; set; } = 0;
        public string FamilyName { get; set; } = "";
        public string FamilyAut { get; set; } = "";
        public string Protolog { get; set; } = "";
        public string DeltaNote { get; set; } = "";
        public string FlowerFormula { get; set; } = "";
        public int FamilyTags { get; set; } = 0;
        public List<Genus> Genera { get; set; } = new List<Genus> ();
        }
    public class Genus
        {
        public int GenusId { get; set; } = 0;
        public int FamilyId { get; set; } = 0;
        public string GenusName { get; set; } = "";
        public string GenusAut { get; set; } = "";
        }
    public class Species
        {
        public string SpeciesId { get; set; } = "";
        public string AcceptedId { get; set; } = "";
        public int GenusId { get; set; } = 0;
        public string Epithet { get; set; } = "";
        public int Rank { get; set; } = 0;
        public string InfraEpithet { get; set; } = "";
        public string Author { get; set; } = "";
        public int NomStatus { get; set; } = 0;
        public int TaxStatus { get; set; } = 0;
        public int ConsStatus { get; set; } = 0;
        public int Characters { get; set; } = 0;
        public string Chorotype { get; set; } = "";
        public string Lifeform { get; set; } = "";
        public bool IsHybrid { get; set; } = false;
        public string SciName { get; set; } = "";
        }
    //DATA
    public class DatasetDataFL
        {
        public int DatasetDataFLId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public int SpeciesId { get; set; } = 0;
        public string Infraspecific { get; set; } = "";
        }
    public class DatasetDataCN
        {
        public int DatasetDataCNId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public string LineString { get; set; } = "";
        public string ParsedGenus { get; set; } = "";
        public string ParsedEpithet { get; set; } = "";
        public int SpeciesId { get; set; } = 0;
        public string Infraspecific { get; set; } = "";
        }
    public class DatasetDataLM
        {
        public int DatasetDataLMId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public int SpeciesId { get; set; } = 0;
        }
    public class DatasetDataST
        {
        public int DatasetDataSTId { get; set; } = 0;
        public int StationId { get; set; } = 0;
        public int Yr { get; set; } = 0;
        public string DataType { get; set; } = "";
        public decimal M01 { get; set; } = 0;
        public decimal M02 { get; set; } = 0;
        public decimal M03 { get; set; } = 0;
        public decimal M04 { get; set; } = 0;
        public decimal M05 { get; set; } = 0;
        public decimal M06 { get; set; } = 0;
        public decimal M07 { get; set; } = 0;
        public decimal M08 { get; set; } = 0;
        public decimal M09 { get; set; } = 0;
        public decimal M10 { get; set; } = 0;
        public decimal M11 { get; set; } = 0;
        public decimal M12 { get; set; } = 0;
        }
    public class Dataset
        {
        public int DatasetId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int DatasetType { get; set; } = 0;
        public string DatasetAbbr { get; set; } = "";
        public string DatasetName { get; set; } = "";
        public string DatasetRef { get; set; } = "";
        public int RefYear { get; set; } = 0;
        public string RefLang { get; set; } = "";
        public string DatasetManager { get; set; } = "";
        public int AreaHa { get; set; } = 0;
        public int ProvinceId { get; set; } = 0;
        public string Coords { get; set; } = "";
        public int AltMax { get; set; } = 0;
        public int AltMin { get; set; } = 0;
        public int AltAver { get; set; } = 0;
        public Decimal TempMax { get; set; } = 0;
        public Decimal TempMin { get; set; } = 0;
        public Decimal TempAver { get; set; } = 0;
        public Decimal Precipitation { get; set; } = 0;
        public int DatasetTags { get; set; } = 0;
        }
    public class Station
        {
        public int StationId { get; set; } = 0;
        public int StationCode { get; set; } = 0;
        public string StationName { get; set; } = "";
        public int StationAlt { get; set; } = 0;
        public string StationCoords { get; set; } = "";
        }
    public class Set
        {
        public int SetId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int SetType { get; set; } = 0;
        public string SetName { get; set; } = "";
        public int SetTags { get; set; } = 0;
        }
    public class SetData
        {
        public int SetDataId { get; set; } = 0;
        public int SetId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public int Subsets { get; set; } = 0;
        }
    public class Note
        {
        public int NoteId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int RefType { get; set; } = 0;
        public int RefId { get; set; } = 0;
        public DateTimeOffset NoteDateTime { get; set; } = new DateTimeOffset ();
        public DateTimeOffset? NoteDateTimeDue { get; set; } = new DateTimeOffset ();
        public string NoteText { get; set; } = "";
        public int NoteTags { get; set; } = 0;
        public bool IsRead
            {
            get => (NoteTags & 1) == 1;
            set
                {
                if (value)
                    NoteTags |= 1;
                else
                    NoteTags &= ~1;
                }
            }
        public bool IsTodo
            {
            get => (NoteTags & 2) == 2;
            set
                {
                if (value)
                    NoteTags |= 2;
                else
                    NoteTags &= ~2;
                }
            }
        public bool IsDone
            {
            get => (NoteTags & 4) == 4;
            set
                {
                if (value)
                    NoteTags |= 4;
                else
                    NoteTags &= ~4;
                }
            }
        }
    public class Notebook
        {
        public int NotebookId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string NotebookName { get; set; } = "";
        public int NotebookTags { get; set; }
        //wrapper properties for each flag
        public bool IsActive
            {
            get => (NotebookTags & 1) == 1;
            set
                {
                if (value)
                    NotebookTags |= 1;
                else
                    NotebookTags &= ~1;
                }
            }
        public List<Section> Sections { get; set; } = new List<Section> ();
        public List<Note> Notes { get; set; } = new List<Note> ();
        }
    public class Section
        {
        public int SectionId { get; set; } = 0;
        public int NotebookId { get; set; } = 0;
        public string SectionName { get; set; } = "";
        public int SectionTags { get; set; } = 0;
        public List<Note> Notes { get; set; } = new List<Note> ();
        }

    }
