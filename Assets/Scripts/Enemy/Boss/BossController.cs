using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public float health;
    float lastDamageTime = -1F;

    public void Damage(float dmg)
    {
        lastDamageTime = Time.time;
        health -= dmg;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(health);
        if (Time.time - lastDamageTime <= 0.1F)
            GetComponent<SpriteRenderer>().color = new Color(1F, 0.6F, 0.6F);
        else
            GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FriendlyBullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
