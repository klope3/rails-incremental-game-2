//Context object that gets passed to each owned skill effect, which can then apply logic to the object in its own unique ways.
//This class needs to exist for the skill system to work, but the data structure inside it is project-specific.
public class AppliedSkillEffects
{
    public float EnergyAdd { get; set; }
    public float MoveSpeedAdd { get; set; }
    public float EnergyDepletionRateAdd { get; set; }

    public AppliedSkillEffects()
    {
    }
}
