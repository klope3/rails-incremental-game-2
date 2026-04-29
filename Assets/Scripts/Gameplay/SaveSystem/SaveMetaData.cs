//metadata about the save. project-agnostic and pretty much every project should have this, even those that only use a single save file.
[System.Serializable]
public class SaveMetaData
{
    public string saveName; //the display name of the save file, e.g. "Bob's Save", "Sally's Game," etc.
    //createDate and lastModifiedDate are probably provided by the file itself, so not needed??
    //public string createDate; //the stringified DateTime when the save was first created
    //public string lastModifiedDate; //the stringified DateTime when the save was most recently modified
    public string version; //the version of the game that most recently modified the save file
}
