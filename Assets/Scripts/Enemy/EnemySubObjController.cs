using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubObjController : MonoBehaviour
{
    public EnemyController Master;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Damage(float dmg)
    {
        Master.Damage(dmg);
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        if(sr)
            sr.color = Master.spriteColor;
    }
}
