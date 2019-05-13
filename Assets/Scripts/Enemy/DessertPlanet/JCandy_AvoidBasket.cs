using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCandy_AvoidBasket : MonoBehaviour {

    public EnemyController ec;
    public Vector2 targetPos;
    public GameObject avoidingObj = null;
    // Use this for initialization
    void Start ()
    {
        ec = GetComponent<EnemyController>();
        ec.targetPos = targetPos;

    }
	
	// Update is called once per frame
	void Update () {

        if (avoidingObj != null)
        {
            Vector2 d = targetPos - (Vector2)avoidingObj.transform.position;
            if (d.magnitude <= 3F)
            {
                Vector2 r = (Vector2)avoidingObj.transform.position + d.normalized * 3F;
                ec.targetPos = r;
            }
        }
        else
        {
            Debug.Log("Null");
            ec.targetPos = targetPos;
        }

    }
}
