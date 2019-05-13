using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float rotateSpeed = 135F, gRate = 36F, bltGRate = 24F;
    float rot = 0F;
    GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update ()
    {
        rot += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0F, 0F, rot);
        if (player)
        {
            Vector3 v = transform.position - player.transform.position;
            float d = v.magnitude;
            Vector2 vec = (Vector2)v.normalized;
            player.GetComponent<Rigidbody2D>().AddForce((vec * gRate) / d);
        }
        GameObject[] Bullets = GameObject.FindGameObjectsWithTag("FriendlyBullet");
        foreach (GameObject i in Bullets)
        {
            Vector3 v = transform.position - i.transform.position;
            float d = v.magnitude;
            Vector2 vec = (Vector2)v.normalized;
            if(i.GetComponent<Rigidbody2D>())
                i.GetComponent<Rigidbody2D>().AddForce((vec * bltGRate) / d);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.GetComponent<Player>().Damage(10000);
        if (collision.gameObject.tag == "FriendlyBullet")
            Destroy(collision.gameObject);
    }
}
