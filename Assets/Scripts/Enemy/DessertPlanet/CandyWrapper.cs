using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyWrapper : MonoBehaviour {

    public enum State { Open, Close };
    public State state = State.Close;
    Vector2 targetPos;
    public GameObject ball;
    public Sprite closeSprite, openSprite;
    public float basicOpenTime = 2F;
    public float openTime = 2F;
    public void Open()
    {
        state = State.Open;
        GetComponent<EnemyController>().moveForceMagnitude /= 3;
        GetComponent<Rigidbody2D>().mass /= 3;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        GetComponent<BoxCollider2D>().enabled = true;
        
        gameObject.layer = 12;
        GameObject.Instantiate(ball, transform.position, Quaternion.identity);
    }
    // Use this for initialization
    void Start ()
    {
        openTime = basicOpenTime + 3F;
        ball = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CandyBall");
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Close:
                openTime -= Time.deltaTime;
                if (openTime <= 0F)
                    Open();
                break;
            case State.Open:

                break;
        }
	}
}
