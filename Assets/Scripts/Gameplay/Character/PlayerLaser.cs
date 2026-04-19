using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform laserParent;
    [SerializeField] private ProjectileLauncher[] launchers;

    private void Update()
    {
        Vector3 vec = playerInput.CursorWorldPosition - laserParent.position;
        vec.y = 0;
        laserParent.forward = vec;
    }

    public void Fire()
    {
        foreach (ProjectileLauncher l in launchers)
        {
            l.Attack();
        }
    }
}
