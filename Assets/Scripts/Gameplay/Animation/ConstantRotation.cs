using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 rotation;

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
