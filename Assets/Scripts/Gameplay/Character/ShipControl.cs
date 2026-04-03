using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ECM2.Character character;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float stopThreshold;
    [SerializeField] private PlayerSkills playerSkills;

    public void PrepareForLevel()
    {
        character.maxWalkSpeed = baseMoveSpeed + playerSkills.AppliedSkillEffects.MoveSpeedAdd;
        character.TeleportPosition(Vector3.zero);
    }

    private void Update()
    {
        Vector3 vecToCursor = playerInput.CursorWorldPosition - character.transform.position;
        Vector3 moveVec = vecToCursor.magnitude > stopThreshold ? vecToCursor.normalized : Vector3.zero;
        character.SetMovementDirection(moveVec);
    }
}
