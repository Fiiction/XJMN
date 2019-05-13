using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCandy : MonoBehaviour {

    public enum State { Move, Shoot };
    public State state = State.Move;
    public EnemyController ec;
    public float moveTime = 4, shootInterval = 0.6F;
    public int shootCount = 3;
    public GameObject Bullet, JCFace;
    public Animator anim;
    public void ChangeState(State s)
    {
        state = s;
        switch (state)
        {
            case State.Move:
                anim.SetBool("Idle", true);
                anim.SetBool("Atk", false);
                ec.targetRot = 180F;
                break;
            case State.Shoot:
                anim.SetBool("Atk", true);
                anim.SetBool("Idle", false);
                ec.targetRot = Random.Range(-24F,24F);
                break;

        }

    }
    void Shoot()
    {
        Vector2 vec = transform.rotation * Vector2.down;
        vec *= 5F;

        var obj = GameObject.Instantiate(Bullet, transform.position + transform.rotation * new Vector3(-0.19F,-0.65F), Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = vec;

        GetComponent<Rigidbody2D>().AddForce(vec * -1.6F,ForceMode2D.Impulse);
    }
    IEnumerator JCorotine()
    {
        yield return new WaitForSeconds(moveTime + Random.Range(1F, 1F));
        while (true)
        {
            ChangeState(State.Shoot);
            yield return new WaitForSeconds(shootInterval*1F);
            for (int i = 1; i <= shootCount; i++)
            {
                yield return new WaitForSeconds(shootInterval);
                Shoot();
            }
            yield return new WaitForSeconds(shootInterval*1.5F);
            ChangeState(State.Move);
            yield return new WaitForSeconds(moveTime * Random.Range(0.8F, 1F));
        }

    }

	// Use this for initialization
	void Start () {
        ec = GetComponent<EnemyController>();
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/SweetBullet");
        StartCoroutine(JCorotine());
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
