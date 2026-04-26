using UnityEngine;

//simple data structure used in SkillTierSO to define a list of the tier's requirements.
[System.Serializable]
public class SkillTierRequirement
{
    [field: SerializeField] public SkillRequirements.SkillRequirementType RequirementType { get; private set; }
    //params allow flexible control of how a requirement type is applied, at the cost of clarity. they are expressed as floats but can represent ints, percentages, bools, etc. depending on the requirement's implementation.
    //some requirements may leave the params completely unused.
    [field: SerializeField] public float Param1 { get; private set; }
}
