using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    [SerializeField] private PlayerPulseWave pulseWave;
    [SerializeField] private MeshRenderer shockwaveMesh;
    [SerializeField] private float coefficient;
    [SerializeField] private float add;
    [SerializeField] private string innerRadiusPropertyName;
    private int innerRadiusParamId;

    private void OnEnable()
    {
        innerRadiusParamId = Shader.PropertyToID(innerRadiusPropertyName);
    }

    private void Update()
    {
        shockwaveMesh.transform.localScale = Vector3.one * 2 * pulseWave.CurRadius;
        shockwaveMesh.material.SetFloat(innerRadiusParamId, coefficient * pulseWave.CurRadius + add);
    }
}
