using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaos : MonoBehaviour {

    public float basicRotateSpeed = 35F,gRate = 24F;
    float rotateSpeed;
    public float rot = 0F,vortexRadius = 3F;
    float phase = 0F;
    public int vortexCnt = 6;
    GameObject player,vortex,beam;

    public enum State { Wait, Swirl, Move ,Beam};
    public State state = State.Wait;
    float waitTime = 5F,beamTime = 0F;
    int lastPos = 0,stayCnt = 0;
    Vector3 pos0 = new Vector3(0F, 9.6F),pos1 = new Vector3(-6F,1.5F),pos2 = new Vector3(6F,-1.5F);

    void CastBeam(float angle)
    {
        GameObject obj = GameObject.Instantiate(beam, Vector3.zero, Quaternion.identity);
        var b = obj.GetComponent<Beam>();
        b.attachedObject = gameObject;
        //b.origin = transform.position;
        b.angle = angle;
        b.aimTime = 1.5F;
        b.castTime = 1.5F;
        b.width = 0.25F;
    }


	// Use this for initialization
	void Start () {
        rotateSpeed = basicRotateSpeed;
        player = GameObject.Find("Player");
        vortex = Resources.Load<GameObject>("Prefabs/Others/Vortex");
        beam = Resources.Load<GameObject>("Prefabs/Others/beam");

        for (int i = 1; i <= vortexCnt; i++)
        {
            float d = 360 * i / vortexCnt;
            GameObject obj = GameObject.Instantiate(vortex, Vector3.up * 100, Quaternion.identity);
            var v = obj.AddComponent<ChaosVortex>();
            v.deltaRot = d;
        }
        //CastBeam();

        if (Random.Range(0F, 1F) <= 0.5F)
        {
            pos1.x = -pos1.x;
            pos2.x = -pos2.x;
        }
    }

    public float CalcAdjustment()
    {
        float y = transform.position.y,r = vortexRadius;
        y = Mathf.Clamp(y, 5F, 8F);
        y -= 5F;
        y /= 3F;
        r = (r-3F) / 3F;

        float ret = (0.6F + 0.4F * y) * (1 + 0.3F * r);
        ret = Mathf.Clamp(ret, 0.5F, 1F);
        //Debug.Log(ret);
        return ret;
    }

    IEnumerator BeamCoroutine()
    {

        Vector2 d= GameObject.Find("Player").transform.position - transform.position;
        float angle = Vector2.Angle(Vector2.up, d);
        if (d.x > 0)
            angle = -angle;
        while (state == State.Beam)
        {
            CastBeam(angle);
            angle += 25F;
            yield return new WaitForSeconds(Random.Range(0.18F, 0.2F));
        }

    }

    void CalcRotateSpeed()
    {
        if (state == State.Wait)
            return;

        float tgt = 35F;
        if (state == State.Beam)
            tgt /= -3F;
        if (state == State.Move)
            tgt *= 2F;

        rotateSpeed -= (rotateSpeed - tgt) * 2F * Time.deltaTime;

    }

	// Update is called once per frame
	void Update () {

        if (GetComponent<BossController>().health <= 0)
            Destroy(gameObject);

        CalcRotateSpeed();
        rot += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0F, 0F, rot);
        if (player)
        {
            Vector3 v = transform.position - player.transform.position;
            float d=v.magnitude-2.0F;
            if (d < 0.5F) d = 0.5F;
            Vector2 vec = (Vector2)v.normalized;
            player.GetComponent<Rigidbody2D>().AddForce((vec*gRate)*CalcAdjustment()/d);
        }
        
        //State Machine
        switch(state)
        {
            case State.Wait:
                waitTime -= Time.deltaTime;
                if (waitTime <= 0F)
                {
                    if (GetComponent<BossController>().health >= 45)
                    {
                        stayCnt++;
                        if (stayCnt == 3)
                        {
                            state = State.Beam;
                            StartCoroutine(BeamCoroutine());
                        }
                        else
                            state = State.Swirl;
                    }
                    else
                    {
                        if ((stayCnt > 1)||(lastPos>=0&&stayCnt>0))
                        {
                            state = State.Move;
                            stayCnt = 0;
                        }
                        else
                        {
                            if (lastPos == 2)
                            {
                                state = State.Beam;
                                StartCoroutine(BeamCoroutine());
                            }
                            else
                                state = State.Swirl;
                            stayCnt++;
                        }

                    }
                    phase = 0F;
                }
                break;
            case State.Swirl:
                phase += Time.deltaTime * 0.6F;
                vortexRadius = 3F + (1 - Mathf.Cos(phase)) * 3.5F;
                if (phase >= 2*Mathf.PI)
                {
                    state = State.Wait;
                    waitTime = Random.Range(0.8F, 1.2F);
                }
                break;
            case State.Move:
                phase += Time.deltaTime * 0.3F;
                float u = 0.5F * (1 - Mathf.Cos(phase));
                if (lastPos == 0)
                    transform.position = Vector3.Lerp(pos0, pos1, u);
                if (lastPos == 1)
                    transform.position = Vector3.Lerp(pos1, pos2, u);
                if (lastPos == 2)
                    transform.position = Vector3.Lerp(pos2, pos0, u);
                if (phase >= 1 * Mathf.PI)
                {
                    state = State.Wait;
                    waitTime = Random.Range(1.6F, 2.2F);
                    lastPos++;
                    lastPos = lastPos % 3;
                }
                break;
            case State.Beam:
                phase += Time.deltaTime;
                if(phase >= 8F)
                {
                    state = State.Wait;
                    waitTime = Random.Range(3.6F, 4.2F);
                }
                break;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().Damage(10000);
    }
}
