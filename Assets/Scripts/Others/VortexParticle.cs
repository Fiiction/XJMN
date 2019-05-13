using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexParticle : MonoBehaviour {

    public GameObject target;
    public Vector3 offset = Vector3.zero;
    float speed = 4F;
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {

        if (!target)
            Destroy(gameObject);

        speed += 8F * Time.deltaTime;

        transform.position += (target.transform.position + offset - transform.position).normalized * speed * Time.deltaTime;

        if ((target.transform.position + offset - transform.position).magnitude <= 0.2F)
            Destroy(gameObject);

    }
}
