using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour {

    PlayerInterface pInterface;
    Rigidbody2D body;

    const float Xbound = 4.7F;
    public float x, y;

    // Use this for initialization
    void Start ()
    {
        pInterface = GetComponent<PlayerInterface>();
        body = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        GetComponent<PolygonCollider2D>();
	}

    void Move()
    {
        x = pInterface.axesX;
        y = pInterface.axesY;

        body.AddTorque(x * -3F);
        body.AddTorque(-0.06F * body.rotation);

        if (body.rotation < -180)
            body.rotation += 360;
        if (body.rotation > 180)
            body.rotation -= 360;
        
        body.AddForce(Vector2.left * 0.5F * body.velocity.x);

        body.AddRelativeForce(Vector2.up * (7F + 9 * y));
        body.AddForce(Vector2.up * (0.5F + 2.5F * y));
        body.AddForce(Vector2.right * 6F*x);

        if (transform.position.x < Xbound)
            transform.position += Vector3.right * 2 * Xbound;
        if (transform.position.x > Xbound)
            transform.position += Vector3.left * 2 * Xbound;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
    }
}
