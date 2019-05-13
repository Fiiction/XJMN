using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFork : MonoBehaviour {

    IEnumerator ForkCoroutine()
    {
        yield return new WaitForSeconds(2F);
        GetComponent<Rigidbody2D>().velocity = transform.rotation * (12F * Vector2.down);
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(ForkCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
