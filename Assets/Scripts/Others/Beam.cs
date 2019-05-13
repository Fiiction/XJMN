using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    public Vector2 origin, direction;
    Vector2 Lo,Ro;
    RaycastHit2D LHit, RHit;
    public float width, forceMagnitude=16F,angle,damage=16F;
    public GameObject attachedObject;
    LayerMask mask;
    GameObject beamSprite;
    LineRenderer aimLine;
    public float aimTime, castTime;
    float basicAimTime;
    public enum Type { Enemy, Friendly };
    public Type type = Type.Enemy;

    public enum State { Aim, Cast };
    State state = State.Aim;

    Quaternion DirQuaternion()
    {
        float dirAngle = Vector2.Angle(Vector2.up, direction);
        if (direction.x > 0)
            dirAngle = -dirAngle;
        return Quaternion.Euler(0, 0, dirAngle);
    }

    // Use this for initialization
    void Start () {
        if (type == Type.Enemy)
            mask = LayerMask.GetMask("Player");
        if (type == Type.Friendly)
            mask = LayerMask.GetMask( "Enemy");
        beamSprite = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Others/BeamSprite"), Vector3.up * 100, Quaternion.identity);
        beamSprite.transform.localScale = new Vector3(width, width, 1F);
        beamSprite.SetActive(false);
        basicAimTime = aimTime;
        aimLine = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (attachedObject)
        {
            direction = attachedObject.transform.rotation * Vector2.up;
            direction = Quaternion.Euler(0, 0, angle) * direction;

            origin = attachedObject.transform.position;
        }
        else
            direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

        switch (state)
        {
            case State.Aim:
                if (aimTime <= 0)
                {
                    state = State.Cast;
                    beamSprite.SetActive(true);
                    Destroy(aimLine);
                }
                //Debug.DrawRay(origin, direction, Color.magenta);
                aimTime -= Time.deltaTime;

                float aimLength = 1 - (aimTime / basicAimTime);
                aimLength *= 32;
                aimLength = Mathf.Clamp(aimLength, 0, 16);
                aimLine.positionCount = 2;
                aimLine.SetPositions(new Vector3[] { origin, origin + aimLength * direction });
                break;

            case State.Cast:
                castTime -= Time.deltaTime;
                if (castTime <= 0)
                {
                    Destroy(gameObject);
                    Destroy(beamSprite);
                }
                Lo = origin + (Vector2)(Quaternion.Euler(0, 0, 90) * (width * 0.5F * direction));
                Ro = origin + (Vector2)(Quaternion.Euler(0, 0, -90) * (width * 0.5F * direction));

                LHit = Physics2D.Raycast(Lo, direction, 20F,mask);
                RHit = Physics2D.Raycast(Ro, direction, 20F,mask);

                float length = 40;
                if (LHit && RHit)
                    length = Mathf.Max((LHit.point - Lo).magnitude, (RHit.point - Ro).magnitude);

                Vector3 spriteCenterPos = origin + 0.5F * length * direction;
                length /= width;

                beamSprite.GetComponent<SpriteRenderer>().size = new Vector2(1F, length);
                beamSprite.transform.position = spriteCenterPos;
                beamSprite.transform.rotation = DirQuaternion();

                if (LHit)
                {
                    Debug.Log("LHit");
                    var col = LHit.collider;
                    var body = LHit.rigidbody;
                    Debug.Log(body);
                    if (body)
                    {
                        //body.AddForceAtPosition((Vector2)(Quaternion.Euler(0, 0, -45) * (forceMagnitude * direction)), LHit.point);
                        body.AddForce((Vector2)(Quaternion.Euler(0, 0, -45) * (forceMagnitude * direction)));
                        body.AddTorque(-direction.y * 0.3F * forceMagnitude);
                    }
                    if (type == Type.Friendly)
                    {
                        if (body.GetComponent<BossController>())
                            body.GetComponent<BossController>().Damage(damage * Time.deltaTime);
                        if (body.GetComponent<EnemyController>())
                            body.GetComponent<EnemyController>().Damage(damage * Time.deltaTime);
                    }
                    if (type == Type.Enemy)
                    {
                        if (body.GetComponent<Player>())
                            body.GetComponent<Player>().Damage(damage * Time.deltaTime);
                    }

                }
                if (RHit)
                {
                    Debug.Log("RHit");
                    var col = RHit.collider;
                    var body = RHit.rigidbody;
                    Debug.Log(body);
                    if (body)
                    {
                        //body.AddForceAtPosition((Vector2)(Quaternion.Euler(0, 0, 45) * (forceMagnitude * direction)), RHit.point);
                        body.AddForce((Vector2)(Quaternion.Euler(0, 0, 45) * (forceMagnitude * direction)));
                        body.AddTorque(direction.y * 0.3F * forceMagnitude);
                    }

                    if (type == Type.Friendly)
                    {
                        if (body.GetComponent<BossController>())
                            body.GetComponent<BossController>().Damage(damage * Time.deltaTime);
                        if (body.GetComponent<EnemyController>())
                            body.GetComponent<EnemyController>().Damage(damage * Time.deltaTime);
                    }
                    if (type == Type.Enemy)
                    {
                        if (body.GetComponent<Player>())
                            body.GetComponent<Player>().Damage(damage * Time.deltaTime);
                    }
                }
                break;
        }
    }
}
