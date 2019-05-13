using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPText : MonoBehaviour {

    Text text;
    Player pl;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        pl = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "HP = " + pl.health.ToString("0.") ;
	}
}
