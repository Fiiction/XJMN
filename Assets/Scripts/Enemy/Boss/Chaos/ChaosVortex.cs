using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosVortex : MonoBehaviour {

    BossChaos boss;
    public float deltaRot = 0F;

	// Use this for initialization
	void Start () {
        boss = GameObject.Find("BossChaos").GetComponent<BossChaos>();
        if (!boss)
            Destroy(gameObject);

        transform.position = boss.transform.position + Quaternion.Euler(0, 0, boss.rot + deltaRot) * (Vector3.up*boss.vortexRadius);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!boss)
            Destroy(gameObject);
        GetComponent<Vortex>().gRate = 6F * (boss.vortexRadius-1F)*boss.CalcAdjustment();
        transform.position = boss.transform.position + Quaternion.Euler(0, 0, boss.rot + deltaRot) * (Vector3.up * boss.vortexRadius);

    }
}
