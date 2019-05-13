using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWJCandy : MonoBehaviour {

    public bool isLJC;
    public DessertWave dw;
    public int num;
    EnemyController ec;
    Vector2 tgtPos;
	// Use this for initialization
	void Start () {
        ec = GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isLJC)
        {
            tgtPos = new Vector2(-2.1F, 6.7F) + 2F * (Vector2)(Quaternion.Euler(0F, 0F, dw.phaseL + 120F * (num - 1F)) * Vector2.up);
        }
        else
        {
            tgtPos = new Vector2(2.1F, 4F) + 2F * (Vector2)(Quaternion.Euler(0F, 0F, dw.phaseR - 120F * (num - 1F)) * Vector2.up);
        }

        if (CakeSlice.attackingInstance != null)
        {
            Vector2 avoidPos = (Vector2)CakeSlice.attackingInstance.transform.position
                + CakeSlice.attackingInstance.GetComponent<Rigidbody2D>().velocity.normalized * 0.6F;
            if ((avoidPos - tgtPos).magnitude <= 1.6F)
            {
                tgtPos = avoidPos + 1.6F * (tgtPos - avoidPos).normalized;
            }
        }
        ec.targetPos = tgtPos;
    }

    private void OnDestroy()
    {
        if (isLJC)
        {
            dw.hasLJC[num] = null;
            dw.LJCcnt--;
        }
        else
        {
            dw.hasRJC[num] = null;
            dw.RJCcnt--;
        }
    }
}
