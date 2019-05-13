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
    

}