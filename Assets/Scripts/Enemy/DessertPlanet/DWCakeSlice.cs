using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWCakeSlice : MonoBehaviour
{

    public DessertWave dw;
    public CakeSlice cs;
    public int num;
    float trig;
    // Use this for initialization
    void Start()
    {
        cs = GetComponent<CakeSlice>();
        trig = num == 0 ? 110F : 290F;
    }

    // Update is called once per frame
    void Update()
    {
        trig = num == 0 ?dw.phaseL : dw.phaseR;
        if (trig >= 0F && trig <= 10F && cs.state == CakeSlice.State.Idle
            && ((Vector2)transform.position-cs.idlePos).magnitude<=0.5F)
            cs.StartMoveOn();
    }
    
    private void OnDestroy()
    {
        dw.hasCS[num] = null;
    }
}
