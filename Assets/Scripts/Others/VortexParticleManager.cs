using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexParticleManager : MonoBehaviour {

    public float strength = 1F, minDist = 2F, maxDist = 5F,offset = 1F;
    public GameObject par,player;
    public Color color;

    void Generate()
    {
        Debug.Log("G");
        GameObject obj = GameObject.Instantiate(par,
            player.transform.position + Quaternion.Euler(0F,0F,Random.Range(0F,360F))* Vector3.up * Random.Range(0F, 1.2F),
            Quaternion.identity);
        obj.GetComponent<VortexParticle>().target = gameObject;
        obj.GetComponent<SpriteRenderer>().color = color;
        obj.GetComponent<VortexParticle>().offset = 
            Quaternion.Euler(0F, 0F, Random.Range(0F, 360F)) * Vector3.up * Random.Range(0F, offset);
    }


	// Use this for initialization
	void Start ()
    {
        par = Resources.Load<GameObject>("Prefabs/Others/VortexParticle");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (!player)
            player = GameObject.Find("Player");
        if (!player)
            return;

        float dist = (player.transform.position - transform.position).magnitude;
        if (dist < minDist)
            dist = 0;
        else if (dist < maxDist)
            dist = (dist - minDist) / (maxDist - minDist);
        else dist = 1;

        float p = strength * 0.5F *  (Mathf.Pow(0.01F, dist)-0.01F);
        //Debug.Log(p);
        if (Random.Range(0F, 1F) < p)
            Generate();
	}
}
