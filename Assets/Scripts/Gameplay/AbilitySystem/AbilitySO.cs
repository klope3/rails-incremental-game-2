using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Tooltip("How many times this ability can be used per instance, without upgrades.")] 
    public int BaseMaxUses { get; private set; }

    public abstract void ActivateAbility(AbilityUseContext context);
    public abstract void DeactivateAbility(AbilityUseContext context); //only used for abilities where something happens on mouse up (such as the ability turning off)
}
