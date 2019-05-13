using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSlice : MonoBehaviour {

    GameObject player;
    public Animator faceAnim;
    Animator anim;
    public enum State { Idle, MoveOn, Attack, MoveOff };
    public State state = State.Idle;
    public float damage = 20;
    public bool isSelfControlled = true;
    float idleTime,lastAttackTime,phase;
    public static GameObject attackingInstance = null;
    public Vector2 idlePos;
    Vector2 basicTargetPos,targetPos;
    Vector2 startPos,diffPos;
    EnemyController EC;

    public CakeSliceAttackTrigger attackTrigger;

    float lastDamageTime = -10F;
    public void DealDamage(GameObject obj)
    {
        if (Time.time < lastDamageTime + 0.5F)
            return;
        lastDamageTime = Time.time;
        obj.GetComponent<Player>().Damage(damage);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-6F, -3F),ForceMode2D.Impulse);
    }

    public void TryAttack()
    {
        if (Time.time <= lastAttackTime + 2.5F || state == State.Attack)
            return;
        StartAttack();
    }
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        idleTime = Random.Range(7F, 8F);
        EC = GetComponent<EnemyController>();
	}

    public void StartMoveOn()
    {
        if (attackingInstance) return;

        faceAnim.SetTrigger("Angry");

        basicTargetPos = (Vector2)player.transform.position + Vector2.up*1.5F;
        phase = 0F;
        startPos = transform.position;
        diffPos = (basicTargetPos - (Vector2)transform.position);
        attackingInstance = this.gameObject;
        
        state = State.MoveOn;
    }
    Vector2 CalcMoveOnPos()
    {
        return startPos + new Vector2((1-Mathf.Cos(phase)) * diffPos.x,Mathf.Sin(phase)*diffPos.y)
            + (Vector2)player.transform.position + Vector2.up * 1.5F - basicTargetPos;
    }

    IEnumerator AttackCoroutine()
    {

        yield return new WaitForSeconds(1.2F);
        attackTrigger.Attack();
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.6F);
        StartMoveOff();
    }

    void StartAttack()
    {
        lastAttackTime = Time.time;
        state = State.Attack;
        EC.moveForceMagnitude *= 2F;
        StartCoroutine(AttackCoroutine());
    }

    void StartMoveOff()
    {
        phase = 0F;
        EC.moveForceMagnitude *= 0.5F;
        startPos = transform.position;
        diffPos = (idlePos - (Vector2)transform.position);
        state = State.MoveOff;
    }

    void StartIdle()
    {

        faceAnim.SetTrigger("Smile");
        idleTime = Random.Range(4F, 5F);
        state = State.Idle;
        attackingInstance = null;
    }
    Vector2 CalcMoveOffPos()
    {
        return startPos + new Vector2(Mathf.Sin(phase) * diffPos.x, (1 - Mathf.Cos(phase)) * diffPos.y);
    }
    // Update is called once per frame
    void Update () {
        switch (state)
        {
            case State.Idle:
                EC.targetPos = idlePos;
                if (isSelfControlled)
                {
                    idleTime -= Time.deltaTime;
                    if (idleTime <= 0 && attackingInstance == null)
                        StartMoveOn();
                }
                break;
            case State.MoveOn:
                phase += 12F * Time.deltaTime / diffPos.magnitude;
                EC.targetPos = CalcMoveOnPos();
                if (phase > 0.5F * Mathf.PI)
                    StartAttack();
                break;
            case State.Attack:
                break;
            case State.MoveOff:
                phase += 6F * Time.deltaTime / diffPos.magnitude;
                EC.targetPos = CalcMoveOffPos();
                if (phase > 0.5F*Mathf.PI)
                    StartIdle();
                break;
        }

	}
    private void OnDestroy()
    {
        if (attackingInstance == this.gameObject)
            attackingInstance = null;
    }
}
