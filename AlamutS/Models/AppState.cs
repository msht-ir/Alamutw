namespace AlamutS.Models
    {
    public class AppState
        {
        public AppState ()
            {
            Console.WriteLine ("AlamutS.AppState Class ***** AppState instance created!");
            }
        public event Action? OnChange;

        public int? Offset { get; private set; }
        public void SetOffset (int _Offset) { Offset = _Offset; OnChange?.Invoke (); }
        public string? Message { get; private set; }
        public void SetMessage (string _Message) { Message = _Message; OnChange?.Invoke (); }
        public string? Cmd { get; private set; }
        public void SetCmd (string _Cmd) { Cmd = _Cmd; OnChange?.Invoke (); }
        public string? Clps { get; private set; }
        public void SetClps (string _Clps) { Clps = _Clps; OnChange?.Invoke (); }
        public string? ReturnPage { get; private set; }
        public void SetReturnPage (string _ReturnPage) { ReturnPage = _ReturnPage; OnChange?.Invoke (); }

        //user
        public int? UserId { get; private set; }
        public void SetUserId (int _UserId) { UserId = _UserId; OnChange?.Invoke (); }
        public string? UserName { get; private set; }
        public void SetUserName (string _userName) { UserName = _userName; OnChange?.Invoke (); }
        public string? UserPass { get; private set; }
        public void SetUserPass (string _userPass) { UserPass = _userPass; OnChange?.Invoke (); }
        public string? UserNickname { get; private set; }
        public void SetUserNickname (string _userNickname) { UserNickname = _userNickname; OnChange?.Invoke (); }
        public int? UserTags { get; private set; }
        public void SetUserTags (int _UserTags) { UserTags = _UserTags; OnChange?.Invoke (); }
        public int? UserLoginStatus { get; private set; }
        public void SetUserLoginStatus (int _UserLoginStatus) { UserLoginStatus = _UserLoginStatus; OnChange?.Invoke (); }

        //Note
        public int? SelectedNoteId { get; private set; }
        public void SetSelectedNoteId (int _SelectedNoteId) { SelectedNoteId = _SelectedNoteId; OnChange?.Invoke (); }
        public int? NoteId { get; private set; }
        public void SetNoteId (int _NoteId) { NoteId = _NoteId; OnChange?.Invoke (); }
        public int? NoteRefId { get; private set; }
        public void SetNoteRefId (int _NoteRefId) { NoteRefId = _NoteRefId; OnChange?.Invoke (); }
        public int? NoteRefType { get; private set; }
        public void SetNoteRefType (int _NoteRefType) { NoteRefType = _NoteRefType; OnChange?.Invoke (); }
        public string? NoteRefName { get; private set; }
        public void SetNoteRefName (string _NoteRefName) { NoteRefName = _NoteRefName; OnChange?.Invoke (); }
        public string? NoteDateTime { get; private set; }
        public void SetNoteDateTime (string _NoteDateTime) { NoteDateTime = _NoteDateTime; OnChange?.Invoke (); }
        public string? NoteDateTimeDue { get; private set; }
        public void SetNoteDateTimeDue (string _NoteDateTimeDue) { NoteDateTimeDue = _NoteDateTimeDue; OnChange?.Invoke (); }
        public string? NoteText { get; private set; }
        public void SetNoteText (string _NoteText) { NoteText = _NoteText; OnChange?.Invoke (); }
        public int? NoteUserId {get; private set;}
        public void SetNoteUserId (int _NoteUserId) { NoteUserId = _NoteUserId; OnChange?.Invoke (); }
        public bool NoteIsRtl { get; private set; }
        public void SetNoteIsRtl (bool _NoteIsRtl) { NoteIsRtl = _NoteIsRtl; OnChange?.Invoke (); }
        public bool NoteIsTodo { get; private set; }
        public void SetNoteIsTodo (bool _NoteIsTodo) { NoteIsTodo = _NoteIsTodo; OnChange?.Invoke (); }
        //Notebook
        public int? NotebookId { get; private set; }
        public void SetNotebookId (int _NotebookId) { NotebookId = _NotebookId; OnChange?.Invoke (); }
        public int? SectionId { get; private set; }
        public void SetSectionId (int _SectionId) { SectionId = _SectionId; OnChange?.Invoke (); }
        
        //ApgGroup
        public int? ApgGroupId { get; private set; }
        public void SetApgGroupId (int _ApgGroupId) { ApgGroupId = _ApgGroupId; OnChange?.Invoke (); }
        public string? ApgGroupName { get; private set; }
        public void SetApgGroupName (string _ApgGroupName) { ApgGroupName = _ApgGroupName; OnChange?.Invoke (); }
        //Order
        public int? OrderId { get; private set; }
        public void SetOrderId (int _OrderId) { OrderId = _OrderId; OnChange?.Invoke (); }
        public string? OrderName { get; private set; }
        public void SetOrderName (string _OrderName) { OrderName = _OrderName; OnChange?.Invoke (); }
        //Family
        public int? FamilyId { get; private set; }
        public void SetFamilyId (int _FamilyId) { FamilyId = _FamilyId; OnChange?.Invoke (); }
        public string? FamilyName { get; private set; }
        public void SetFamilyName (string _FamilyName) { FamilyName = _FamilyName; OnChange?.Invoke (); }
        //Genus
        public int? GenusId { get; private set; }
        public void SetGenusId (int _GenusId) { GenusId = _GenusId; OnChange?.Invoke (); }
        public string? GenusName { get; private set; }
        public void SetGenusName (string _GenusName) { GenusName = _GenusName; OnChange?.Invoke (); }
        //Species
        public int? SpeciesId { get; private set; }
        public void SetSpeciesId (int _SpeciesId) { SpeciesId = _SpeciesId; OnChange?.Invoke (); }
        public string? SpeciesName { get; private set; }
        public void SetSpeciesName (string _SpeciesName) { SpeciesName = _SpeciesName; OnChange?.Invoke (); }
        //Dataset
        public int? DatasetId { get; private set; }
        public void SetDatasetId (int _DatasetId) { DatasetId = _DatasetId; OnChange?.Invoke (); }
        public string? DatasetName { get; private set; }
        public void SetDatasetName (string _DatasetName) { DatasetName = _DatasetName; OnChange?.Invoke (); }

        }
    }
