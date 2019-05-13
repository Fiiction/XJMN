using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour {

    public GameObject player;
    Rigidbody2D body;
    Vector2 targetPos;
    public enum State { Attack, Dodge ,Cast};
    public State state = State.Dodge;
    public float x, y;
    public float advanceRate = 0;
    float deltaX = 0;
    const float Xbound = 4.7F;
    public ParticleSystem P1, P2;
    // Use this for initialization

    public void SetState(State s)
    {
        var e2 = P2.emission;
        switch (s)
        {
            case State.Attack:
                state = s;
                e2.rateOverTime = 0F;
                break;
            case State.Cast:
                state = s;
                e2.rateOverTime = 20F;
                break;
            case State.Dodge:
                state = s;
                e2.rateOverTime = 0F;
                break;
        }

    }

    void CalcDodge()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("FriendlyBullet");
        targetPos = transform.position;
        foreach (var i in obj)
        {
            Vector2 dist = transform.position - i.transform.position;
            Vector2 vl = - body.velocity + i.GetComponent<Rigidbody2D>().velocity;
            float l = Vector2.Dot(dist, vl.normalized);
            Vector2 collidePoint = (Vector2)i.transform.position + l * vl.normalized;
            Vector2 deltaPos = collidePoint - (Vector2)transform.position;
            /*
            Debug.Log(":");
            Debug.Log(i.transform.position);
            Debug.Log(l);
            */
            if (deltaPos.magnitude < 0.05F)
                deltaPos = Vector2.right * 0.1F;
            if (l >= 0.5 && l <= 5 && deltaPos.magnitude < 1.6F)
                targetPos += deltaPos.normalized * -1F * (7F - l) * (3F - deltaPos.magnitude);
            if (l >= -1 && l < 0.5 && deltaPos.magnitude < 1.6F)
                targetPos += deltaPos.normalized * -1F * 4.333F * (l + 1F) * (3F - deltaPos.magnitude);
        }

    }

    void CalcAttack()
    {
        Vector2 deltaPos = player.transform.position - transform.position;
        float dist = deltaPos.magnitude;
        deltaPos += advanceRate * player.GetComponent<Rigidbody2D>().velocity * dist;
        float angle = Vector2.SignedAngle(Vector2.down, deltaPos);
        Debug.Log(angle);
        x = (angle-body.rotation) *0.05F - body.angularVelocity * 0.008F;

    }

    void Start () {
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();
	}

    void Move()
    {
        float d = new Vector2(x, y).magnitude;
        if (d > 1)
        {
            y /= d;
            x /= d;
        }
        var e1 = P1.emission;
        e1.rateOverTime = 5F * Mathf.Pow((y + 0.5F),2F);

        body.AddTorque(x * 3F);
        body.AddTorque(-0.06F * body.rotation);

        if (body.rotation < -180)
            body.rotation += 360;
        if (body.rotation > 180)
            body.rotation -= 360;


        body.AddRelativeForce(Vector2.down * (7F + 9F * y));
        body.AddForce(Vector2.down * (0.5F + 2.5F * y));
        body.AddForce(Vector2.left * 0.5F * body.velocity.x);
        body.AddForce(Vector2.right * 6F * x);

        if (transform.position.x < Xbound)
            transform.position += Vector3.right * 2 * Xbound;
        if (transform.position.x > Xbound)
            transform.position += Vector3.left * 2 * Xbound;
    }

    // Update is called once per frame
    void Update () {

        switch(state)
        {
            case State.Dodge:
                CalcDodge();
                //targetPos = (Vector2)player.transform.position;
                targetPos.y = 6.66F;
                deltaX = player.transform.position.x - transform.position.x;
                if (deltaX > Xbound)
                    deltaX -= 2F * Xbound;
                if (deltaX < -Xbound)
                    deltaX += 2F * Xbound;
                deltaX = Mathf.Clamp(deltaX, -1.3F, 1.3F);
                targetPos.x += deltaX;

                if (transform.position.y - targetPos.y < 0)
                    y = 0.4F * (transform.position.y - targetPos.y);
                else
                    y = 0.6F + 0.4F * (transform.position.y - targetPos.y);
                
                x = (targetPos.x - transform.position.x- 0.2F*body.velocity.x - 0.018F * body.rotation - 0.018F * body.angularVelocity) * 0.4F;
                x = Mathf.Clamp(x, -1, 1);
                y += 0.8F * Mathf.Abs(x);
                break;
            case State.Attack:
                targetPos = (Vector2)player.transform.position;
                targetPos.y = 6.66F;
                CalcAttack();
                if (transform.position.y - targetPos.y < 0)
                    y = 1F + 0.5F * (transform.position.y - targetPos.y);
                else
                    y = 1F;
                //x = 0F;
                break;
            case State.Cast:
                CalcDodge();
                //targetPos = (Vector2)player.transform.position;
                targetPos.y = 6F;
                deltaX = player.transform.position.x - transform.position.x;
                if (deltaX > Xbound)
                    deltaX -= 2F * Xbound;
                if (deltaX < -Xbound)
                    deltaX += 2F * Xbound;
                deltaX = Mathf.Clamp(deltaX, -1.3F, 1.3F);
                targetPos.x += deltaX;

                y = 0.16F;
                if (transform.position.y > 7F)
                    y += 0.23F * (transform.position.y - 7F);
                x = (targetPos.x - transform.position.x - 0.2F * body.velocity.x - 0.018F * body.rotation - 0.018F * body.angularVelocity) * 0.4F;
                x = Mathf.Clamp(x, -1, 1);
                x *= 0.2F;
                y += 0.8F * Mathf.Abs(x);
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}
