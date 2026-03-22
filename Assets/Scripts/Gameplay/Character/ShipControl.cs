using UnityEngine;

namespace V2
{
    public class ShipControl : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ECM2.Character character;
        [SerializeField] private float stopThreshold;

        private void Update()
        {
            Vector3 vecToCursor = playerInput.CursorWorldPosition - character.transform.position;
            Vector3 moveVec = vecToCursor.magnitude > stopThreshold ? vecToCursor.normalized : Vector3.zero;
            character.SetMovementDirection(moveVec);
        }
    }
}
