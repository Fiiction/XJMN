using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertWitch : MonoBehaviour {

    WitchController WC;
    BossController BC;
    GameObject Bullet, Fork, player;
    float maxHealth = 20;
    int phase = 0;
    int forkCnt = 0;
    // Use this for initialization

    void Shoot()
    {
        Vector2 vec = transform.rotation * Vector2.down;
        vec *= 8F;

        var obj = GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = vec;
        if(phase <= 1)
            GetComponent<Rigidbody2D>().AddForce(vec * -0.34F, ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().AddForce(vec * -0.28F, ForceMode2D.Impulse);

    }

    void GenerateFork(Vector2 pos, Vector2 target)
    {
        float rot = Vector2.SignedAngle(Vector2.down, (target - pos));
        GameObject obj = GameObject.Instantiate(Fork, (Vector3)pos, Quaternion.Euler(0F, 0F, rot));
    }

    IEnumerator DWCoroutine()
    {
        WC.SetState(WitchController.State.Dodge);
        yield return new WaitForSeconds(6F);
        while (true)
        {
            switch (phase)
            {
                case 0:
                    WC.SetState(WitchController.State.Attack);
                    yield return new WaitForSeconds(0.15F);
                    for (int i = 1; i <= 7; i++)
                    {
                        yield return new WaitForSeconds(0.35F);
                        Shoot();
                        if (transform.position.y > 7F)
                            break;
                    }

                    yield return new WaitForSeconds(0.35F);
                    WC.SetState(WitchController.State.Dodge);
                    yield return new WaitForSeconds(Random.Range(3F, 5F));
                    break;
                case 1:
                    if (Random.Range(0F, 1F) <= 0.3F)
                    {
                        WC.SetState(WitchController.State.Attack);
                        yield return new WaitForSeconds(0.15F);
                        for (int i = 1; i <= 7; i++)
                        {
                            yield return new WaitForSeconds(0.35F);
                            Shoot();
                            if (transform.position.y > 7F)
                                break;
                        }
                        yield return new WaitForSeconds(0.35F);
                        WC.SetState(WitchController.State.Dodge);
                        yield return new WaitForSeconds(Random.Range(2F, 5F));
                    }
                    else
                    {
                        WC.SetState(WitchController.State.Cast);
                        yield return new WaitForSeconds(1F);
                        switch (forkCnt)
                        {
                            case 0:
                                GenerateFork(new Vector2(-2.5F,9F), player.transform.position);
                                yield return new WaitForSeconds(1F);
                                GenerateFork(new Vector2(2.5F, 9F), player.transform.position);
                                break;
                            case 1:
                                GenerateFork(new Vector2(3F, 9F), Vector2.zero);
                                yield return new WaitForSeconds(0.667F);
                                GenerateFork(new Vector2(-3F, 9F), Vector2.zero);
                                yield return new WaitForSeconds(0.667F);
                                GenerateFork(new Vector2(0F, 9F), Vector2.zero);
                                break;
                            case 2:
                                GenerateFork(new Vector2(-3.3F, 9F), 10000* Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(-1.1F, 9F), 10000 * Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(1.1F, 9F), 10000 * Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(3.3F, 9F), 10000 * Vector2.down);
                                break;
                            default:
                                GenerateFork(new Vector2(3.3F, 9F), 10000 * Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(1.1F, 9F), 10000 * Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(-1.1F, 9F), 10000 * Vector2.down);
                                yield return new WaitForSeconds(0.5F);
                                GenerateFork(new Vector2(-3.3F, 9F), 10000 * Vector2.down);
                                break;
                        }
                        forkCnt++;
                        if (forkCnt >= 4)
                            phase = 2;
                        yield return new WaitForSeconds(0.35F);
                        WC.SetState(WitchController.State.Dodge);
                        yield return new WaitForSeconds(Random.Range(5F, 7F));
                    }
                    break;
                case 2:
                    WC.SetState(WitchController.State.Attack);
                    yield return new WaitForSeconds(0.15F);
                    for (int i = 1; i <= 7; i++)
                    {
                        yield return new WaitForSeconds(0.3F);
                        Shoot();
                        if (transform.position.y > 7F)
                            break;
                    }

                    yield return new WaitForSeconds(0.35F);
                    WC.SetState(WitchController.State.Dodge);
                    yield return new WaitForSeconds(Random.Range(2F, 3F));
                    break;
            }
        }
    }

    void Start()
    {
        BC = GetComponent<BossController>();
        maxHealth = BC.health;
        WC = GetComponent<WitchController>();
        WC.advanceRate = 0.08F;
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/SweetBullet_Witch");
        Fork = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/BigFork");
        player = GameObject.Find("Player");
        StartCoroutine(DWCoroutine());
    }
    // Update is called once per frame
    void Update () {
        if (BC.health <= maxHealth * 0.6 && phase <= 0)
            phase = 1;

        if (GetComponent<BossController>().health <= 0)
            Destroy(gameObject);
    }
}
