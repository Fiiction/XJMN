using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Player pl;
    RectTransform rt;
    float basicHealth, basicWidth;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<Player>();
        rt = GetComponent<RectTransform>();
        basicWidth = rt.rect.width;
        basicHealth = pl.health;
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(basicWidth * pl.health/basicHealth,rt.sizeDelta.y);
    }
}
