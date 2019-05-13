using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBreath : MonoBehaviour {

    public float startScale = 0.2F, endScale = 0.8F, speed = 2F, inflationSpeed = 0.2F;
    float scale;
    public Vector3 vec,deltaPos;
	// Use this for initialization
	void Start () {
        scale = startScale;
        vec = transform.rotation * Vector3.down;
	}
	
	// Update is called once per frame
	void Update () {

        scale += inflationSpeed * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, 1F);
        if (scale > endScale)
            Destroy(gameObject);

        deltaPos = vec * Time.deltaTime * (1F + (scale - startScale) / (endScale - startScale));

        transform.position = transform.position + deltaPos;

	}
}
