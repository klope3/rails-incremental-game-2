//the parent object holding the full structure of the game's save data. this class needs to exist for the save system to work, but its contents will be game-specific.
[System.Serializable]
public class SaveData
{
    public SaveMetaData metaData;
    public PlayerData playerData;

    public SaveData()
    {
        metaData = new SaveMetaData();
        playerData = new PlayerData();
    }
}
