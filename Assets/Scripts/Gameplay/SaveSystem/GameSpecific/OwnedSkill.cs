[System.Serializable]
public class OwnedSkill
{
    public string id;
    public int tierIndex;

    public OwnedSkill(string id, int tierIndex)
    {
        this.id = id;
        this.tierIndex = tierIndex;
    }
}
