using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFire : MonoBehaviour
{

    public float angle = -1.6F,alpha = 0F;
    bool moving = false;
    SpriteRenderer SR;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 10F);
    }

    void FixedUpdate()
    {
        if (moving)
        {
            angle += 0.44F * Time.fixedDeltaTime;
            body.velocity = 4.8F * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
        }
        else
        {
            alpha += Time.deltaTime / 1.6F;
            if (alpha >= 1F)
            {
                moving = true;
                transform.SetParent(null);
            }
            SR.color = new Color(1F, 1F, 1F, alpha);
        }
    }
}
