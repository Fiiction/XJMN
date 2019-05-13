using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPText : MonoBehaviour {

    Text text;
    BossController b;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        b = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!b) return;
        if (b.health > 0)
            text.text = "Boss = " + b.health.ToString("0.");
        else
            text.text = "!!!You Win!!!";

    }
}
