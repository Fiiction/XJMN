using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertWave : MonoBehaviour {

    GameObject JC, CS;
    public GameObject[] hasLJC = { null, null, null, null, null, null };
    public GameObject[] hasRJC = { null, null, null, null, null, null };
    public GameObject[] hasCS = { null, null };
    public Vector2[] CSGenPos = { new Vector2(-9F, 9F), new Vector2(10F, 7F) };
    public Vector2[] CSTgtPos = { new Vector2(-2.1F,6.7F), new Vector2(2.1F,4F) };
    public int JCtot = 0, RJCcnt = 0, LJCcnt = 0,CStot = 0;
    public float phaseL,phaseR;
    public 
    int FirstFalse(GameObject[] o)
    {
        int num = -1;
        for (int i = 0; i < o.Length; i++)
            if (o[i] == null)
            {
                num = i;
                return num;
            }
        return num;
    }
    void GenerateLJC()
    {
        if (JCtot >= 10)
            return;

        int num = FirstFalse(hasLJC);
        if (num == -1)
            return;
        JCtot++;
        LJCcnt++;
        GameObject obj = GameObject.Instantiate(JC, new Vector3(-8F, 10F), Quaternion.identity);
        hasLJC[num] = obj;
        var dwjc = obj.AddComponent<DWJCandy>();
        dwjc.isLJC = true;
        dwjc.dw = this;
        dwjc.num = num;

    }
    void GenerateRJC()
    {
        if (JCtot >= 12)
            return;

        int num = FirstFalse(hasRJC);
        if (num == -1)
            return;

        JCtot++;
        RJCcnt++;
        GameObject obj = GameObject.Instantiate(JC, new Vector3(10F, 8F), Quaternion.identity);
        hasRJC[num] = obj;
        var dwjc = obj.AddComponent<DWJCandy>();
        dwjc.isLJC = false;
        dwjc.dw = this;
        dwjc.num = num;
    }

    void GenerateCS()
    {
        if (CStot >= 4)
            return;
        int num = FirstFalse(hasCS);
        Debug.Log(hasCS);
        Debug.Log(num);
        if (num == -1)
            return;
        CStot++;
        GameObject obj = GameObject.Instantiate(CS, (Vector3)CSGenPos[num], Quaternion.identity);
        hasCS[num] = obj;
        obj.GetComponent<CakeSlice>().idlePos = CSTgtPos[num];
        obj.GetComponent<CakeSlice>().isSelfControlled = false;
        var dwcs = obj.AddComponent<DWCakeSlice>();
        dwcs.dw = this;
        dwcs.num = num;
    }

    IEnumerator GenerateJCCoroutine()
    {
        while (true)
        {
            if (LJCcnt < 3)
                GenerateLJC();
            yield return new WaitForSeconds(4F);
            if (RJCcnt < 3)
                GenerateRJC();
            yield return new WaitForSeconds(4F);
        }
    }
    IEnumerator GenerateCSCoroutine()
    {
        yield return new WaitForSeconds(5F);
        while (true)
        {
            GenerateCS();
            yield return new WaitForSeconds(10F);
        }
    }
    // Use this for initialization
    void Start () {
        JC = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/J_Candy");
        CS = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CakeSlice");
        phaseL = 0F;
        phaseR = 180F;
        StartCoroutine(GenerateJCCoroutine());
        StartCoroutine(GenerateCSCoroutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
        phaseL += 45F * Time.deltaTime;
        phaseR += 45F * Time.deltaTime;
        if (phaseL > 360F)
            phaseL -= 360F;
        if (phaseR > 360F)
            phaseR -= 360F;
    }
}
