using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour {

    public bool homing = false, inflating = false;
    float speed = 4F;
    Rigidbody2D body;
    GameObject target;
    Vector2 vec;
    float bornTime, inflRate = 1F,basicScale,scale;
	// Use this for initialization
	void Start ()
    {
        bornTime = Time.time;
        body = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        if (!target)
            Destroy(gameObject);

        float r = Random.Range(0F, 1F);
        /*
        if (r <= 0.25F)
        {
            homing = true;
            body.drag = 0.6F;
        }
        */
        if (r >= 0.75F)
        {
            inflating = true;
            speed *= 0.35F;
            body.drag = 3F;
        }

        vec = (target.transform.position - transform.position).normalized;
        body.velocity = vec * speed;
        basicScale = scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (homing&&target)
        {
            vec = (target.transform.position - transform.position).normalized;
            body.AddForce(vec * speed * 0.4F);
            if (Time.time - bornTime >= 4F)
            {
                homing = false;
                body.drag = 0F;
            }
        }
        if (inflating)
        {
            inflRate = 2F - 1F * Mathf.Cos((Time.time - bornTime) * 1.5F);
            scale = basicScale * inflRate;
            transform.localScale = new Vector3(scale, scale, 1F);
            body.AddForce((4F - scale) * vec * speed);
        }
	}
}
