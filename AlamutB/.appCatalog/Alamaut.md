# ALAMUT web

# Taxon           BaseInfo        Biodiv            UserData
  APG4            Countries       LocalFlora        Sets
  Divisions       Provinces       FloristicData     SetdataCN
  Classes         Stations                          SetdataLM
  Subclasses      Stationdata                       SetdataDS
  Orders          Users                             SetdataST    
  Families        Settings                          Notes    
  Genera          
  Species         




# Taxon.APG4
APG4Id		    nt
sequence	    smallint
ApgGroupNameEn	varchar(60)
ApgGroupNameFa	nvarchar(60)

## sequence     ApgGroupNameEn
   1'000 	    Algae
   2'000 	    Bryophytes
   3'000 	    Lycophytes
   4'000 	    Monilophytes
   5'000 	    Coniferophyt
   6'000 	    Angiosperms
   6'001 	    Unclass. Ang.
   6'100 	    Basal Ang.
   6'200 	    Magnoliids
   6'300 	    Monocots
   6'399 	    Sist of Eudicots
   6'400 	    Eudicots
   6'500 	    Basal Eudicots
   6'600 	    Core Eudicots
   6'610 	    Super Rosids
   6'620 	    Rosids
   6'650 	    Super Asterids
   6'660 	    Asterids
   10'000	    RESERVED

# Taxon.Divisions
DivisionId	    int
DivisionName	varchar(50)

# Taxon.Classes
ClassId		int
DivisionId	int
ClassName	varchar(60)

# Taxon.Subclasses
SubclassId	int
ClassId		int
SubclassName	varchar(50)

# Taxon.Orders
OrderId		int
ApgId		int
OrderName	varchar(50)
OrderSeq	smallint
orderNote	varchar(255)

# Taxon.Families
FamilyId	int
OrderId		int
FamilyName	varchar(70)
FamilyAut	nvarchar(255)
Protolog	nvarchar(500)
DeltaNote	varchar(MAX)
Formula		varchar(100)
FamilyTags	int (1: IsAPG4Family 2: 4: 8: )
	
# Taxon.Genera
GenusId		int
FamilyId	int
GenusName	varchar(100)
GenusAut	nvarchar(100)

# Taxon.Species
SpeciesId	        int
GenusId	            int
GenusName	        varchar(100)
Epithet	            varchar(50)
Rank	            varchar(30)
InfraEpithet	    varchar(100)
Author	            nvarchar(100)
Yr	                smallint
TagsNameStatus	    tinyint
TagsConservation	tinyint
TagsCharacters	    int
Chorotype	        varchar(50)
Lifeform	        varchar(50)
WfoId	            varchar(20)
AccLinkId	        varchar(20)
UrlLink	            varchar(70)
	







# BaseInfo.Countries
CountryId	int
CountryName	varchar(60)
CountryNoet	nvarchar(255)

# bBseInfo.Provinces
ProvinceId	    int
CountryId	    tinyint
ProvinceName	nvarchar(50)

# BaseInfo.Stations
StationId	    int
StationCode	    int
StationName	    nvarchar(200)
StationAlt	    real
StationCoord	varchar(60)

# BaseInfo.Stationdata
StationdataId	int
stationId	    int
Yr		        smallint
JANt		    real
JANp		    real
FEBt		    real
FEBp		    real
MARt		    real
MARp		    real
APRt		    real
APRp		    real
MAYt		    real
MAYp		    real
JUNt		    real
JUNp		    real
JULt		    real
JULp		    real
AUGt		    real
AUGp		    real
SEPt		    real
SEPp		    real
OCTt		    real
OCTp		    real
NOVt		    real
NOVp		    real
DECt		    real
DECp		    real

# BaseInfo.Users
UserId	        int
UserNickname	nvarchar(100)
Username	    nvarchar(30)
Password	    nvarchar(30)
ExpDateTime	    datetimeoffset(0)
UserTags	    int
	

UserTags
0: Active
1: Log user activity
2: Search-Map-Report
3: Sets-CN,LM
4: Dataset Provider
5: Researcher
6: APL Programmer
7: Taxonomist (add gen, sp)
8: Taxonomist (edit all Taxa)
9: Admin







# Biodiv.LocalFlora
LocalFloraId	    int
UserId	            int
LocalFloraAbbr	    varchar(50)
LocalFloraTitle	    nvarchar(1000)
LocalFloraRef	    nvarchar(4000)
RefYear	            int
RefLang	            varchar(2)
CollectionManager	nvarchar(200)
AreaHa	            int
ProvinceId	        int
LocalFloraCoords	nvarchar(40)
AltMax	            int
AltMin	            int
AltAver	            int
TempMax	            real
TempMin	            real
TempAver	        real
Precipitation	    real
LocalFloraTags	    int  {1:Flrst, 2:Partial, 4:Med, 8:Herbarium}

# Biodiv.FloristicData
FloristicDataId	    int
LocalFloraId	    int
SpeciesId	        int
Infraspecific	    varchar(100)
	
# BaseInfo.Settings
SettingId	    int
SettingKey	    varchar(50)
SettingValue	nvarchar(255)








# UserData.Sets
SetId		    int
UserId		    int
SetType		    varchar(20) {1:LocalFlora, 2:CN, 3:LM, 10:Climatic, 11:Climagraph}
SetName		    nvarchar(60)
SetNote		    nvarchar(255)
SetTags		    int

# UserData.SetdataCN	(a set of checkName lists)
SetdataCNId	    int
SetId		    int
LineString	    nvarchar(800)
SarsedGenus	    varchar(60)
ParsedEpithet	varchar(60)
SpeciesId	    int
Infraspecific	varchar(60)
	
# UserData.SetdataLM    (a set of Listmaker lists)
SetdataLMId	    int
SetId		    int
SpeciesId	    int

# UserData.SetdataDS
SetdataDSId	    int
SetId		    int
DatasetId	    int
Membership	    int

# UserData.SetdataST    (a set of stations)
SetdataSTId	    int
SetId		    int
StationID	    int
Membership	    int

# UserData.Notes
NoteId	        int
UserId	        int
ReferenceType	int {1:U 2:Ord 3:F 4:G 5:S 6:Flora 7:FloristicData 8:Set 20:Station}
ReferenceId	    int
NoteDateTime	datetimeoffset(0)
NoteDateTimeDue	datetimeoffset(0)
NoteText	    nvarchar(4000)
NoteTags	    int {1:Done, 2:Todo 4:IsShared}







class       Taxon
Int         SaveMode = 2        -- 0:AddNew 1:Edit 2:NoAction
string      TOI                 -- taxon of interest
string      Group 
int         GrpId 
string      Order 
int         OrdId
string      Family 
int         FamId 
string      Genus 
int         GenId 
string      Epithet 
string      Author 
string      SciName 
int         SpcId 
string      WfoId 
string      AccId 
int         Rstat 
int         Rcons 
long        Rchar



Rchar 0000'0000  0000'0000  0000'0000  0000'0000 (Int)
00    Endem        3188 recs from RedList
01    Rare         1370 recs from RedList
02    Med          2645 recs from RedList
03    Range         209 recs from RedList
04    Weed            0 rec  from RedList
05    Aquatic         7 recs
06    Submerged       3 recs
07    Parasite        4 recs incl 1 Semiparasitic
08    Herb         1191 recs
09    Annual        244 recs
10    Biennial      104 recs
11    Prennial     1588 recs
12    Monocarpic     13 recs
13    Rhizomatous    51 recs
14    Tuberous       29 recs
15    Bulbous        79 recs
16    Corm            4 recs
17    Caespitose    187 recs
18    Woody           9 recs
19    Prostrate      11 recs incl 2 Procombents)
20    Bush          215 recs
21    Pulvinate      76 recs
22    Shrub         113 recs
23    subShrub       93 recs
24    Tree           41 recs
25    Evergreen       1 rec


Rstat 0000'0000 (tinyInt) 
- value 0: Blank/NA
00    No Opinion
01    Unresolved
02    Accepted
03    Synonym
04    Misapplied
05    Invalid
06    Illegitimate
07    -

Rcons 0000'0000 (tinyInt) 
- value 0: NE (not evaluated)
00    DD   [data deficient]
01    LC   [LR:least concearn]
02    CD   [LR:conservation dependent]
03    NT   [LR:near threatened]
04    VU   [Threatened: vulnerable]
05    EN   [Threatened: endangered]
06    CR   [Threatened: critically endangered]
07    EW   [Exttint inWild]
08    EX   [Extinct]

