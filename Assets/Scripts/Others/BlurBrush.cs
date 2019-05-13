using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurBrush : MonoBehaviour {

    public bool fadeOut = false;
    public float alpha = 0;
    public Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        if (fadeOut)
        {
            alpha -= Time.deltaTime * 2F;
            if (alpha <= 0F)
                Destroy(gameObject);
            mat.SetFloat("_Alpha", alpha);
        }
        else
        {
            if (alpha <= 1F)
            {
                alpha += Time.deltaTime * 2F;
                mat.SetFloat("_Alpha", alpha);
            }
        }
	}
}
