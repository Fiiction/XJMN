using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    float alpha = 0F,basicMFM;
    public float fadeInTime = 1F;
    SpriteRenderer SR;
    EnemyController EC;


    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        EC = GetComponent<EnemyController>();
        SR.color = new Color(1F, 1F, 1F, 0F);
        basicMFM = EC.moveForceMagnitude;
        EC.moveForceMagnitude = 0F;
    }

    // Update is called once per frame
    void Update()
    {
        alpha += Time.deltaTime / fadeInTime;
        if (alpha > 1)
            alpha = 1;
        SR.color = new Color(1F, 1F, 1F, alpha);
        EC.moveForceMagnitude = basicMFM * alpha;
        if (alpha >= 1F)
            Destroy(this);
    }
}
