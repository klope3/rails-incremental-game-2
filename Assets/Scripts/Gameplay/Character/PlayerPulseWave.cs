using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class PlayerPulseWave : MonoBehaviour
{
    [SerializeField] private Transform debugVisual;
    private SphereCollider col;
    private float timer;
    private float timerMax;
    private float rate;
    public bool IsPulsing { get; private set; }
    public float CurRadius { get; private set; }
    public UnityEvent OnStartExpand;
    public UnityEvent OnFinishExpand;

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        SetRadius(0.0001f);
    }

    private void Update()
    {
        if (!IsPulsing)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            FinishExpand();
            return;
        }

        SetRadius(col.radius + rate * Time.deltaTime);
    }

    public void DoExpand(float rate, float duration)
    {
        IsPulsing = true;
        timer = 0;
        timerMax = duration;
        col.enabled = true;
        SetRadius(0.0001f);
        this.rate = rate;
        OnStartExpand?.Invoke();
    }

    private void SetRadius(float radius)
    {
        col.radius = radius;
        CurRadius = radius;
        debugVisual.localScale = Vector3.one * radius * 2;
    }

    private void FinishExpand()
    {
        IsPulsing = false;
        col.enabled = false;
        SetRadius(0.0001f);
        OnFinishExpand?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        WorldObjectSpawnableNew obj = other.GetComponent<WorldObjectSpawnableNew>();
        if (obj == null)
        {
            return;
        }

        obj.ReceivePulseImpact(this);
    }
}
