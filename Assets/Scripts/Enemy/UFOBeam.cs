using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBeam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.layer == 8 || obj.layer == 11)
        {
            if (obj.GetComponent<Rigidbody2D>())
                obj.GetComponent<Rigidbody2D>().AddForce(5F * (transform.rotation * Vector2.down), ForceMode2D.Impulse);
        }

    }

}
