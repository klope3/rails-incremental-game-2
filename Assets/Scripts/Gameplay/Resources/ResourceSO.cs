using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSO", menuName = "Scriptable Objects/ResourceSO")]
public class ResourceSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}
