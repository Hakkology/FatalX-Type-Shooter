using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteAlways]
public class ArcLayout : MonoBehaviour
{
    public float radius = 200f;
    public float startAngle = 180f;
    public float endAngle = 360f;

    [Tooltip("Butonları manuel atan veya otomatik al")]
    public List<RectTransform> items;

    void OnValidate()
    {
        if (items == null || items.Count == 0)
        {
            // Panel altındaki tüm RectTransform çocukları al
            items = new List<RectTransform>();
            foreach (RectTransform rt in GetComponentsInChildren<RectTransform>())
                if (rt != (RectTransform)transform)
                    items.Add(rt);
        }
        UpdateLayout();
    }

    void UpdateLayout()
    {
        int count = items.Count;
        if (count == 0) return;
        float angleRange = endAngle - startAngle;
        float step = (count == 1) ? 0 : angleRange / (count - 1);

        for (int i = 0; i < count; i++)
        {
            float angleDeg = startAngle + step * i;
            float rad = angleDeg * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            items[i].anchoredPosition = pos;
            // İsteğe bağlı: butonun dönmesini engellemek için:
            items[i].localEulerAngles = Vector3.zero;
        }
    }
}
