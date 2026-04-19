using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObjectPool pool;
    [SerializeField, Min(0.001f)] private float shotsPerSecond;
    [SerializeField] private Transform muzzle;
    private float timer;
    public bool ReadyToLaunch
    {
        get
        {
            return timer >= 1 / shotsPerSecond;
        }
    }
    public UnityEvent OnLaunch;

    private void Update()
    {
        if (!ReadyToLaunch)
        {
            timer += Time.deltaTime;
        }
    }

    public void Launch()
    {
        if (!ReadyToLaunch) return;

        GameObject obj = pool.GetPooledObject();
        obj.SetActive(true);
        obj.transform.position = muzzle.position;
        Projectile proj = obj.GetComponent<Projectile>();
        proj.Launch(muzzle.forward);
        timer = 0;

        OnLaunch?.Invoke();
    }

    public void Attack()
    {
        Launch();
    }
}
