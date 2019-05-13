using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pie : MonoBehaviour {

    public GameObject[] piePart;
    public GameObject fullPie;
    Vector2[] pieV = { new Vector2(0F, 1.286F), new Vector2(1.485F, 0.501F), new Vector2(1.485F, -0.501F),
        new Vector2(0F, -1.286F),  new Vector2(-1.485F, -0.501F), new Vector2(-1.485F, 0.501F)};
    float r = 0F,idleTime = 5F;
    EnemyController ec;
    public enum State { Idle, Split };
    public State state = State.Idle;
    Vector2 tgtPos = new Vector2(0F, 5F), cenPos;
    // Use this for initialization
    void Start () {
        ec = GetComponent<EnemyController>();
    }

    void Split()
    {
        Debug.Log("Split!");
        state = State.Split;
        cenPos = (Vector2)fullPie.transform.position;
        fullPie.SetActive(false);
        foreach (var i in piePart)
        {
            i.SetActive(true);
            i.transform.position = cenPos;
        }
            
        r = 0;
    }

    void Merge()
    {
        cenPos = Vector2.zero;
        foreach (var i in piePart)
        {
            cenPos += (Vector2)i.transform.position;
            i.SetActive(false);
        }
        cenPos /= 6;
        fullPie.SetActive(true);
        fullPie.transform.position = cenPos;


        idleTime = 8F;
        state = State.Idle;
    }
    IEnumerator SplitCoroutine()
    {
        Split();

        DOTween.To(() => r, x => r = x, 1, 1.2F).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(1.2F);
        DOTween.To(() => r, x => r = x, 2, 1.2F).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(2F);

        DOTween.To(() => r, x => r = x, 10F, 3F).SetEase(Ease.InOutElastic);
        yield return new WaitForSeconds(3F);
        tgtPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        yield return new WaitForSeconds(1F);
        DOTween.To(() => r, x => r = x, 2, 3F).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(5F);

        DOTween.To(() => r, x => r = x, 0, 0.6F);
        yield return new WaitForSeconds(1.6F);

        Merge();

        yield return new WaitForSeconds(1.6F);
        tgtPos = new Vector2(0F, 5F);
    }
    
	// Update is called once per frame
	void Update () {
        
        switch (state)
        {
            case State.Idle:
                fullPie.GetComponent<EnemyController>().targetPos = tgtPos;
                idleTime -= Time.deltaTime;
                if (idleTime <= 0F)
                {
                    StartCoroutine(SplitCoroutine());
                }
                break;

            case State.Split:
                for (int i = 0; i < 6; i++)
                    piePart[i].GetComponent<EnemyController>().targetPos = tgtPos + r * pieV[i];
                break;

        }
    }
}
