using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBall : MonoBehaviour {

    public float damage = 24F;
    float maxY = 7.85F, minY = -2.35F, maxX = 3.85F;
    Rigidbody2D body;
    Vector3 deltaPos = Vector3.zero; float catchTime = -1000, tossTime = -1000, rebTime = -1000;
    Vector2 vel = Vector2.zero;
    float V = 6F;
    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody2D>();
        vel.x = Random.Range(0F, 1F) < 0.5F ? -1F : 1F;
        vel.y = Random.Range(-0.6F, -1.2F);
        vel = V * vel.normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > maxX && Time.time > rebTime + 0.1F)
        {
            vel.x = -vel.x;
            rebTime = Time.time;
            body.velocity = body.velocity.magnitude * vel.normalized;
        }
        if (transform.position.y < minY && Time.time > rebTime + 0.1F && vel.y < 0)
        {
            vel.y = -vel.y;
            rebTime = Time.time;
            body.velocity = body.velocity.magnitude * vel.normalized;
        }
        if (transform.position.y > maxY && Time.time > rebTime + 0.1F && vel.y > 0)
        {
            vel.y = -vel.y;
            rebTime = Time.time;
            body.velocity = body.velocity.magnitude * vel.normalized;
        }
        vel = vel.normalized * V;
        body.AddForce(vel*12);
        Debug.Log(body.velocity.magnitude);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 p = collision.GetContact(0).point;
        if (collision.gameObject.layer == 8)
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damage);
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
                rb.AddForceAtPosition(1.2F * (collision.transform.position-transform.position), p, ForceMode2D.Impulse);
        }
    }
}
