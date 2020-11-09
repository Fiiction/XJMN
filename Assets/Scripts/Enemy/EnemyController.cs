using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public bool isPart = false;
    public EnemyController Master = null;

    public float health;
    float lastDamageTime = -1F;
    public bool moveByTarget = false;
    public Vector2 targetPos = new Vector2(0F,5F);
    public float targetRot = 0F;
    public float moveForceMagnitude = 40F, moveTorque = 1F;
    public float collideDamage = 5F;
    public bool logPosDiff = false;
    public Color spriteColor = Color.white;
    Rigidbody2D body;
    SpriteRenderer sr;

    public static int EnemyCnt = 0;

    public void Damage(float dmg)
    {
        if (isPart)
            Master.Damage(dmg);
        else
        {
            lastDamageTime = Time.time;
            health -= dmg;
        }
    }

    void Awake()
    {
        EnemyCnt++;
    }

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}
    Vector2 pPos, lastPPos, iPos= Vector2.zero, dPos;
    float pR = 1F, iR = 0.1F, dR = 0.3F;
    void Move()
    {
        pPos = targetPos - (Vector2)transform.position;
        iPos *= Mathf.Exp(-Time.fixedDeltaTime);
        iPos += pPos * Time.fixedDeltaTime;
        dPos = (pPos - lastPPos) / Time.fixedDeltaTime;

        Vector2 realIPos = iPos;
        if (realIPos.magnitude > 1F)
            realIPos.Normalize();
        Vector2 force = pPos * pR + realIPos * iR + dPos * dR;
        if (force.magnitude > 1F)
            force.Normalize();
        body.AddForce(force * moveForceMagnitude);
        lastPPos = pPos;
    }

    float lastFrameTargetRot = 0;
    void Rotate()
    {
        if (body.rotation < -180) body.rotation += 360;
        if (body.rotation > 180) body.rotation -= 360;

        float targetAngularVelocity = targetRot - lastFrameTargetRot;
        if (Mathf.Abs(targetAngularVelocity) >= 10F)
            targetAngularVelocity = 0F;
        targetAngularVelocity /= Time.deltaTime;

        float t = (targetRot - body.rotation)*pR;
        t -= body.angularVelocity * dR;
        t += targetAngularVelocity * dR;
        if (t < -180) t += 360;
        if (t > 180) t -= 360;
        if (t < -180) t += 360;
        if (t > 180) t -= 360;
        t *= 0.1F;
        Mathf.Clamp(t, -1F, 1F);

        body.AddTorque(moveTorque * t);

        lastFrameTargetRot = targetRot;
    }

    private void FixedUpdate()
    {
        if (moveByTarget && body)
        {
            Move();
            Rotate();
        }
    }

    private void LateUpdate()
    {
        if (health <= 0F && !isPart)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

        if(sr)
        {
            if (isPart)
                sr.color = Master.spriteColor;
            else
            {
                if (Time.time - lastDamageTime <= 0.1F)
                    spriteColor = new Color(1F, 0.6F, 0.6F);
                else
                    spriteColor = new Color(1F, 1F, 1F);
                sr.color = spriteColor;
            }
            
        }
        if (!moveByTarget && transform.position.y <= -10F)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            var pl = collision.gameObject.GetComponent<Player>();
            Vector2 collidePoint = collision.contacts[0].point;
            if (pl)
            {
                pl.Damage(collideDamage);
                body.AddForceAtPosition(((Vector2)transform.position - collidePoint).normalized * 2F
                    , collidePoint, ForceMode2D.Impulse);
                var plBody = pl.gameObject.GetComponent<Rigidbody2D>();
                if(plBody)
                    plBody.AddForceAtPosition(((Vector2)plBody.transform.position - collidePoint).normalized * 2F
                    , collidePoint, ForceMode2D.Impulse);
            }
        }
    }

    void OnDestroy()
    {
        EnemyCnt--;
    }

}
