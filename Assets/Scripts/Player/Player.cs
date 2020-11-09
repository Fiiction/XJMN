using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float health = 100F;
    float lastDamageTime = -999F;
    void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0F)
            PlayerDeath();
        lastDamageTime = Time.time;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time <= lastDamageTime + 0.2F)
            GetComponent<SpriteRenderer>().color = new Color(1F, 0.7F, 0.7F);
        else
            GetComponent<SpriteRenderer>().color = Color.white;


    }
}
