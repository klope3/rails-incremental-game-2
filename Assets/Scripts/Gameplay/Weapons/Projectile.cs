using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private LayerMask layerMask;
    private Vector3 moveVec;
    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    public void Launch(Vector3 vec)
    {
        moveVec = vec.normalized;
        transform.forward = vec;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 nextPos = transform.position + moveVec * speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, nextPos);

        bool hit = Physics.Raycast(new Ray(transform.position, moveVec), out RaycastHit hitInfo, distance, layerMask);
        if (!hit)
        {
            transform.position = nextPos;
            return;
        }

        IProjectileImpactable impactable = hitInfo.collider.GetComponent<IProjectileImpactable>();
        if (impactable != null)
        {
            impactable.Impact(this, hitInfo);
        }

        transform.position = hitInfo.point;
        gameObject.SetActive(false);
    }
}
