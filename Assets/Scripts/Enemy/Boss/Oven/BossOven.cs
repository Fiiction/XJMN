﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOven : MonoBehaviour {

    public enum State { Idle, DragCloud };
    public State state = State.Idle;
    public float idleTime = 4F;
    GameObject player, AtkCloud;
    public GameObject LHolder, RHolder, LCloud, RCloud;
    Animator anim;
    IEnumerator DragCloudCoroutine()
    {
        Debug.Log("D");
        yield return new WaitForSeconds(1F);
        LCloud = GameObject.Instantiate(AtkCloud, LHolder.transform.position, Quaternion.identity);
        LCloud.GetComponent<EnemyController>().targetPos = new Vector2(-1.98F,3.97F);
        yield return new WaitForSeconds(0.5F);
        LCloud = null;

        yield return new WaitForSeconds(1.5F);
        RCloud = GameObject.Instantiate(AtkCloud, RHolder.transform.position, Quaternion.identity);
        RCloud.GetComponent<EnemyController>().targetPos = new Vector2(1.98F, 3.97F);
        yield return new WaitForSeconds(0.5F);
        RCloud = null;
        yield return new WaitForSeconds(1.3F);
        SetState(State.Idle);
    }

    public void SetState(State s)
    {
        state = s;
        switch (s)
        {
            case State.Idle:
                idleTime = 8F;
                break;
            case State.DragCloud:
                StartCoroutine(DragCloudCoroutine());
                anim.SetBool("dragCloud", false);
                break;
        }

    }


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        AtkCloud = Resources.Load<GameObject>("Prefabs/Enemy/DarkCloud");
    }
	
	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case State.Idle:
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                    anim.SetBool("dragCloud", true);
                break;
            case State.DragCloud:
                break;       
                

        }


        if (RCloud)
        {
            //RCloud.GetComponent<Rigidbody2D>().velocity = RHolder.GetComponent<Rigidbody2D>().velocity;
            RCloud.GetComponent<Rigidbody2D>().velocity = (RHolder.transform.position - RCloud.transform.position) / Time.deltaTime;
            RCloud.transform.position = RHolder.transform.position;
            RCloud.transform.rotation = Quaternion.identity;
        }
        if (LCloud)
        {
            LCloud.GetComponent<Rigidbody2D>().velocity = (LHolder.transform.position - LCloud.transform.position) / Time.deltaTime;
            LCloud.transform.position = LHolder.transform.position;
            LCloud.transform.rotation = Quaternion.identity;
        }

    }
}
