using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public IEnumerator IE = EnemyWaves.Dessert_CandyBasketWithJCandy();

	// Use this for initialization
	void Start () {
        StartCoroutine(IE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
