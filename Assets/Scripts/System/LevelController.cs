using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public IEnumerator[] Waves = {EnemyWaves.Dessert_WelcomeClouds(), EnemyWaves.Dessert_JCandy_And_CakeSlice() };

    IEnumerator WaveCoroutine()
    {
        foreach(var w in Waves)
        {
            var ie = StartCoroutine(w);
            yield return new WaitForSeconds(1F);
            while (ie != null && EnemyController.EnemyCnt > 0)
                yield return 0;
            yield return new WaitForSeconds(3F);
        }
    }


	// Use this for initialization
	void Start () {
        StartCoroutine(WaveCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(EnemyController.EnemyCnt);

	}
}
