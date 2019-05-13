using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSprite : MonoBehaviour {

    float basicVelocity = 0.5F;
    public static float moveRate = 1F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + basicVelocity * moveRate * Time.deltaTime * Vector3.down;
        if (transform.position.y <= -36F)
            transform.position += 108F * Vector3.up;
	}
}
