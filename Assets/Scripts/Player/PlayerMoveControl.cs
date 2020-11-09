using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour {

    PlayerInterface pInterface;
    Rigidbody2D body;

    const float Xbound = 4.7F;
    public float xInput, yInput;

    // Use this for initialization
    void Start ()
    {
        pInterface = GetComponent<PlayerInterface>();
        body = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        GetComponent<PolygonCollider2D>();
	}

    const float MaxTargetX = 8f, MaxTargetY = 7f;
    const float MaxFForce = 24f, MinFForce = -8f;
    const float MaxTorque = 5f;
    const float KPX = 1.6f, KPY = 7f,
        KDX = 1f, KDY = 1.0f,
        KIX = 0f, KIY = 6f;
    const float EaseRate = 0.5f;
    const float Threshold = 4f;
    public float pX, pY, dX, dY, iX, iY = 1f;
    public float delayedDX, delayedDY;
    public float force, torque, subForce;
    float lastPX = 0, lastPY = 0;



    void PIDMove()
    {
        pX = xInput * MaxTargetX - body.velocity.x;
        pY = yInput * MaxTargetY - body.velocity.y;
        dX = delayedDX - (lastPX - body.velocity.x) /Time.fixedDeltaTime;
        float tmp = Mathf.Clamp(dX, -Threshold, Threshold);
        delayedDX = dX - tmp;
        dX = tmp;
        dY = delayedDY + (pY - lastPY)/Time.fixedDeltaTime;
        tmp = Mathf.Clamp(dY, -Threshold, Threshold);
        delayedDY = dY - tmp;
        dY = tmp;
        iX += pX * Time.fixedDeltaTime;
        iY += pY * Time.fixedDeltaTime;
        iX = Mathf.Clamp(iX, -1f, 1f);
        iY = Mathf.Clamp(iY, -1f, 1f);


        force = 6f + pY * KPY + dY * KDY + iY * KIY;
        force = Mathf.Clamp(force, MinFForce, MaxFForce);
        body.AddRelativeForce(Vector2.up * force * (1-EaseRate));
        body.AddForce(Vector2.up * force * EaseRate);

        torque = pX * KPX + /* dX * KDX + */ iX * KIX;
        subForce = dX * KDX;
        torque = Mathf.Clamp(torque, -MaxTorque, MaxTorque);
        body.AddTorque(-torque * (1 - EaseRate));
        body.AddForce(torque * Vector2.right * EaseRate);
        body.AddForce(subForce * Vector2.right);

        lastPX = body.velocity.x;
        lastPY = pY;
    }


    void Move()
    {
        xInput = pInterface.axesX;
        yInput = pInterface.axesY;

        if (body.rotation < -180)
            body.rotation += 360;
        if (body.rotation > 180)
            body.rotation -= 360;

        //body.AddTorque(xInput * -2.5F);
        body.AddTorque(-0.06F * body.rotation);

        /*
        body.AddTorque(x * -3F);
        body.AddTorque(-0.06F * body.rotation);

        body.AddForce(Vector2.left * 0.5F * body.velocity.x);
        body.AddRelativeForce(Vector2.up * (7F + 9 * y));
        body.AddForce(Vector2.up * (0.5F + 2.5F * y));
        body.AddForce(Vector2.right * 6F*x);
        */
        /*
        body.AddForce(Vector2.left * 0.5F * body.velocity.x);
        body.AddRelativeForce(Vector2.up * (7F + 9 * yInput));
        body.AddForce(Vector2.up * (0.5F + 2.5F * yInput));
        body.AddForce(Vector2.right * 8F * xInput);
        */
        PIDMove();
        Debug.Log(body.velocity);
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
