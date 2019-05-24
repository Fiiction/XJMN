using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOven : MonoBehaviour {

    public enum State { Idle, DragCloud ,Fire ,Spit};
    public State state = State.Idle;
    public float idleTime = 4F;
    GameObject player, AtkCloud, Fire, JC;
    public GameObject LHolder, RHolder, LCloud, RCloud;
    Animator anim;
    IEnumerator DragCloudCoroutine()
    {
        anim.SetBool("dragCloud", true);
        yield return new WaitForSeconds(1F);
        LCloud = GameObject.Instantiate(AtkCloud, LHolder.transform.position, Quaternion.identity);
        LCloud.GetComponent<EnemyController>().targetPos = new Vector2(-2.4F,3F);
        yield return new WaitForSeconds(0.6F);
        LCloud = null;

        yield return new WaitForSeconds(1.5F);
        RCloud = GameObject.Instantiate(AtkCloud, RHolder.transform.position, Quaternion.identity);
        RCloud.GetComponent<EnemyController>().targetPos = new Vector2(2.4F, 3F);
        yield return new WaitForSeconds(0.6F);
        RCloud = null;

        anim.SetBool("dragCloud", false);
        yield return new WaitForSeconds(1.3F);
        SetState(State.Idle);
    }

    float fireAngle = -2.8F;
    void GenerateFire()
    {
        fireAngle += Random.Range(0.48F, 0.7F);
        if (fireAngle > -0.4F)
            fireAngle -= 2.8F;
        Vector3 pos = transform.position;
        pos += new Vector3(Random.Range(-1F, 1F), Random.Range(-0.5F, 0.5F));
        var obj = GameObject.Instantiate(Fire, pos, Quaternion.identity, transform);
        obj.GetComponent<OvenFire>().angle = fireAngle;
    }

    IEnumerator FireCoroutine()
    {
        anim.SetBool("fire", true);
        yield return new WaitForSeconds(0.3F);

        for(int i =1;i<=32;i++)
        {
            GenerateFire();
            yield return new WaitForSeconds(0.2F);
        }
        anim.SetBool("fire", false);
        SetState(State.Idle);
    }

    int totSpitCnt = 2, spitCnt = 0;

    Vector2 CalcSpitTgtPos()
    {
        switch (totSpitCnt)
        {
            case 1:
                return new Vector2(0F, 3F);
            case 2:
                switch(spitCnt)
                {
                    case 1:
                        return new Vector2(-2F, 3F);
                    case 2:
                        return new Vector2(2F, 3F);
                }
                break;
            case 3:
                switch (spitCnt)
                {
                    case 1:
                        return new Vector2(-2.5F, 2F);
                    case 2:
                        return new Vector2(0F, 3F);
                    case 3:
                        return new Vector2(2.5F, 2F);
                }
                break;
        }
        return new Vector2(0F, 3F);
    }

    void Spit()
    {
        Vector2 tgtPos = CalcSpitTgtPos();

        var obj = GameObject.Instantiate(JC, transform.position, Quaternion.identity);
        var FI = obj.AddComponent<FadeIn>();
        FI.fadeInTime = 0.5F;
        obj.GetComponent<EnemyController>().targetPos = tgtPos;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3F, 3F), -6F);

    }

    IEnumerator SpitCoroutine()
    {
        anim.SetBool("spit", true);
        yield return new WaitForSeconds(1.45F);

        for (spitCnt = 1;spitCnt<=totSpitCnt;spitCnt++)
        {
            Spit();
            if(spitCnt == totSpitCnt)
            {
                anim.SetBool("spit", false);
                SetState(State.Idle);
                break;
            }
            yield return new WaitForSeconds(2F);
        }
    }
    public void SetState(State s)
    {
        state = s;
        switch (s)
        {
            case State.Idle:
                idleTime = 8F;
                break;
            case State.DragCloud:
                StartCoroutine(DragCloudCoroutine());
                break;
            case State.Fire:
                StartCoroutine(FireCoroutine());
                break;
            case State.Spit:
                StartCoroutine(SpitCoroutine());
                break;
        }
    }



	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        AtkCloud = Resources.Load<GameObject>("Prefabs/Enemy/DarkCloud");
        Fire = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/Fire");
        JC = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/J_Candy");
    }

    Vector3 LLastFramePos, RLastFramePos;
    void FixedUpdate()
    {

        if (RCloud)
        {
            //RCloud.GetComponent<Rigidbody2D>().velocity = (RHolder.transform.position - RLastFramePos) / Time.fixedDeltaTime;
            RCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.7F, -0.3F);
            RCloud.transform.position = RHolder.transform.position;
            RCloud.transform.rotation = Quaternion.identity;
        }
        if (LCloud)
        {
            //LCloud.GetComponent<Rigidbody2D>().velocity = (LHolder.transform.position - LLastFramePos) / Time.fixedDeltaTime;
            LCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(0.7F, -0.3F);
            LCloud.transform.position = LHolder.transform.position;
            LCloud.transform.rotation = Quaternion.identity;
        }
        LLastFramePos = LHolder.transform.position;
        RLastFramePos = RHolder.transform.position;
    }


    int atkCnt = 0;

	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case State.Idle:
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                {
                    atkCnt++;
                    if(atkCnt <= 1)
                        SetState(State.DragCloud);
                    else if(atkCnt == 3)
                        SetState(State.Spit);
                    else
                        SetState(State.Fire);

                }
                break;
            case State.DragCloud:
                break;    
        }

    }
}
