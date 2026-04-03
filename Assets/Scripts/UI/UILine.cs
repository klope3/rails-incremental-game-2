using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILine : MonoBehaviour
{
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private float thickness;

    public GameObject Draw(Vector2 start, Vector2 end, Color color, Transform parent)
    {
        GameObject go = new GameObject();
        go.name = "Line";

        RectTransform rt = go.AddComponent<RectTransform>();
        rt.SetParent(parent, false);
        rt.pivot = new Vector2(0, 0.5f);
        rt.sizeDelta = new Vector2(Vector2.Distance(start, end), thickness);
        rt.anchoredPosition = start;
        rt.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, end - start));

        Image image = go.AddComponent<Image>();
        image.sprite = lineSprite;
        image.color = color;

        return go;
    }
}
