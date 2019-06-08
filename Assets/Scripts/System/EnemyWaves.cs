using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves {

    public static IEnumerator Dessert_CandyBasketWithJCandy()
    {
        GameObject CandyBasket = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CandyBasket");
        GameObject cb = GameObject.Instantiate(CandyBasket, new Vector3(6F, 7F, 0F), Quaternion.identity);
        yield return null;
    }
    
    public static IEnumerator Dessert_WelcomeClouds()
    {
        GameObject BlueCloud = Resources.Load<GameObject>("Prefabs/Enemy/BlueCloud");
        GameObject DarkCloud = Resources.Load<GameObject>("Prefabs/Enemy/DarkCloud");

        float curX = Random.Range(-3.5F, 3.5F);
        for (int i = 1; i <= 5; i++)
        {
            curX += Random.Range(3F, 3.8F);
            if (curX > 3.5F)
                curX -= 7F;
            GameObject.Instantiate(BlueCloud, new Vector3(curX, 11F), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2.4F, 3.2F));
        }
        var ldc = GameObject.Instantiate(DarkCloud, new Vector3(-2.7F, 11F), Quaternion.identity);
        ldc.GetComponent<EnemyController>().targetPos = new Vector2(-2.7F, 7F);
        yield return new WaitForSeconds(0.8F);

        var rdc = GameObject.Instantiate(DarkCloud, new Vector3(2.7F, 11F), Quaternion.identity);
        rdc.GetComponent<EnemyController>().targetPos = new Vector2(2.7F, 7F);
    }


    public static IEnumerator Dessert_JCandy_And_CakeSlice()
    {
        GameObject JC = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/J_Candy");
        GameObject CS = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CakeSlice");

        var lcs = GameObject.Instantiate(CS, new Vector3(-2.3F, 11F), Quaternion.identity);
        lcs.GetComponent<CakeSlice>().idlePos = new Vector2(-2.3F, 5.8F);

        yield return new WaitForSeconds(1.2F);

        var rcs = GameObject.Instantiate(CS, new Vector3(2.3F, 11F), Quaternion.identity);
        rcs.GetComponent<CakeSlice>().idlePos = new Vector2(2.3F, 4.6F);

        yield return new WaitForSeconds(1.2F);

        while (EnemyController.EnemyCnt > 1)
        {
            Debug.Log("!" + EnemyController.EnemyCnt.ToString());
            yield return 0;
        }


        var ljc = GameObject.Instantiate(JC, new Vector3(-2.8F, 11F), Quaternion.identity);
        ljc.GetComponent<EnemyController>().targetPos = new Vector2(-2.8F, 6.7F);

        yield return new WaitForSeconds(0.8F);

        var rjc = GameObject.Instantiate(JC, new Vector3(2.8F, 11F), Quaternion.identity);
        rjc.GetComponent<EnemyController>().targetPos = new Vector2(2.8F, 6.7F);

        yield return new WaitForSeconds(0.8F);

        var mjc = GameObject.Instantiate(JC, new Vector3(0F, 11F), Quaternion.identity);
        mjc.GetComponent<EnemyController>().targetPos = new Vector2(0F, 7.7F);
    }

}