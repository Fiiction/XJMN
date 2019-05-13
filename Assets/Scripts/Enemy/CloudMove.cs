using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    Vector2 moveVec;
    public float basicForce = 1F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var Body = GetComponent<Rigidbody2D>();
        Body.AddForce( Quaternion.Euler(0F,0F, Body.rotation) * Vector2.down * basicForce);
	}
}
