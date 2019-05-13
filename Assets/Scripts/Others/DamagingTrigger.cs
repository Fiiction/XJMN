using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTrigger : MonoBehaviour {

    public float damageRate = 10F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var pl=collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damageRate * Time.deltaTime);
        }
    }
}
