//represents all the effects that skill tiers in the game can have. this class must exist for the skill system to work, but its implementation will be game-specific.
public static class SkillEffects
{
    public enum SkillEffectType
    {
        MaxEnergyIncrease,
        FasterEnergyRegen,
        MoveSpeedIncrease,
        UnlockPulseAbility,
        AlphaResourceWorth,
        BetaResourceWorth,
        GammaResourceWorth,
    }

    public static System.Action<AppliedSkillEffects, float> MaxEnergyIncrease = (appliedSkillEffects, param1) => appliedSkillEffects.EnergyAdd += param1;
    public static System.Action<AppliedSkillEffects, float> FasterEnergyRegen = (appliedSkillEffects, param1) => appliedSkillEffects.EnergyDepletionRateAdd -= param1;
    public static System.Action<AppliedSkillEffects, float> MoveSpeedIncrease = (appliedSkillEffects, param1) => appliedSkillEffects.MoveSpeedAdd += param1;
}
