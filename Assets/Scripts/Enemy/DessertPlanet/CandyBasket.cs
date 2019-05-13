using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBasket : MonoBehaviour {

    Vector2[] bombPos = {new Vector2(-0.5533F,-0.2550F), new Vector2(-0.658F,0.146F), new Vector2(-0.179F,0.566F),
        new Vector2(0.27F,0.463F), new Vector2(0.214F,0.024F), new Vector2(0.635F,0.201F),
        new Vector2(0.541F,-0.266F), new Vector2(-0.225F,0.108F),new Vector2(-0.11F,-0.31F) };

    Rigidbody2D body;
    EnemyController EC;
    GameObject BombObj;
    GameObject LeftJC, RightJC;
    GameObject JC;
    public enum State { Move, Rock };
    public State state = State.Move;

    float p = 3F;
    int cnt = 0;
    int refreshCnt = 0;
    void Refresh()
    {
        CandyBomb[] objs = GetComponentsInChildren<CandyBomb>();
        foreach (var i in objs)
        {
            if (i.active || i.transform.localPosition.magnitude > 1.3F)
                continue;
            Destroy(i.gameObject);
        }

        transform.position = new Vector3(6F, 7F, 0F);
        foreach (var i in bombPos)
        {
            var obj = GameObject.Instantiate(BombObj, transform);
            obj.transform.localPosition = i;
        }
        EC.targetPos = new Vector2(Random.Range(-2.4F, 0F), 7F);
        cnt = 0;
        
        if (!LeftJC && refreshCnt%2 == 0)
        {
            LeftJC = GameObject.Instantiate(JC, new Vector3(-2.8F, 11F, 0F), Quaternion.identity);
            JCandy_AvoidBasket JCAB = LeftJC.AddComponent<JCandy_AvoidBasket>();
            JCAB.targetPos = new Vector2(-2.8F, 6F);
            JCAB.avoidingObj = gameObject;
        }
        if(!RightJC && refreshCnt % 2 == 1)
        {
            RightJC = GameObject.Instantiate(JC, new Vector3(2.8F, 14F, 0F), Quaternion.identity);
            JCandy_AvoidBasket JCAB = RightJC.AddComponent<JCandy_AvoidBasket>();
            JCAB.targetPos = new Vector2(2.8F, 6F);
            JCAB.avoidingObj = gameObject;
        }
        refreshCnt++;
    }

	// Use this for initialization
	void Start () {
        EC = GetComponent<EnemyController>();
        body = GetComponent<Rigidbody2D>();
        JC = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/J_Candy");
        BombObj = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CandyBomb");
        Refresh();
    }

    void StartMove()
    {
        state = State.Move;
        if(cnt == 1)
            EC.targetPos = new Vector2(Random.Range(0F, 2.4F), 7F);
        if (cnt == 2)
            EC.targetPos = new Vector2(Random.Range(-2.4F, 0F), 7F);
        if (cnt == 3)
            EC.targetPos = new Vector2(-1000F, 7F);
    }

    void RockForce()
    {
        CandyBomb[] objs = GetComponentsInChildren<CandyBomb>();
        foreach (var i in objs)
        {
            if (i.active || i.transform.localPosition.magnitude > 1.3F)
                continue;
            i.body.AddForce(new Vector2(Random.Range(-1F, 1F), 0F)*1.0F, ForceMode2D.Impulse);
        }
    }
    IEnumerator RockCoroutine()
    {
        yield return new WaitForSeconds(1F);

        yield return new WaitForSeconds(0.3F);
        EC.targetRot = -24F;
        yield return new WaitForSeconds(0.8F);
        body.AddRelativeForce(Vector2.up * (cnt * 9F + 57F), ForceMode2D.Impulse);
        RockForce();

        yield return new WaitForSeconds(0.3F);
        EC.targetRot = 36F;
        yield return new WaitForSeconds(0.8F);
        body.AddRelativeForce(Vector2.up * (cnt * 9F + 57F), ForceMode2D.Impulse);
        RockForce();

        yield return new WaitForSeconds(0.3F);
        EC.targetRot = -27F;
        yield return new WaitForSeconds(0.8F);
        body.AddRelativeForce(Vector2.up * (cnt * 9F + 57F), ForceMode2D.Impulse);
        RockForce();

        EC.targetRot = 0F;
        yield return new WaitForSeconds(1.6F);
        cnt++;
        StartMove();
    }

    void StartRock()
    {
        state = State.Rock;
        StartCoroutine(RockCoroutine());
    }

	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Move:
                if (((Vector2)transform.position - EC.targetPos).magnitude <= 0.5F)
                    StartRock();
                if (cnt == 3 && transform.position.x <= -6F)
                    Refresh();
                break;
            case State.Rock:

                break;

        }
	}
}
