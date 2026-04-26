using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ECM2.Character character;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float stopThreshold;
    [SerializeField] private PlayerSkills playerSkills;
    public Vector3 MoveVec { get; private set; }

    public void PrepareForLevel()
    {
        character.maxWalkSpeed = baseMoveSpeed + playerSkills.AppliedSkillEffects.MoveSpeedAdd;
        Debug.Log($"Speed now {character.maxWalkSpeed}; base {baseMoveSpeed} + {playerSkills.AppliedSkillEffects.MoveSpeedAdd}");
        character.TeleportPosition(Vector3.zero);
    }

    private void Update()
    {
        Vector3 vecToCursor = playerInput.CursorWorldPosition - character.transform.position;
        MoveVec = vecToCursor.magnitude > stopThreshold ? vecToCursor.normalized : Vector3.zero;
        character.SetMovementDirection(MoveVec);
    }
}
