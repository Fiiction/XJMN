using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBomb : MonoBehaviour {

    public bool active = false;
    float burstY = 0F;
    public Rigidbody2D body;
    GameObject BurstObj;
    CandyBasket Basket;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0F, 1F), 0.7F, 1F);
        body = GetComponent<Rigidbody2D>();
        BurstObj = Resources.Load<GameObject>("Prefabs/Enemy/DessertPlanet/CandyBombBurst");
        Basket = GetComponentInParent<CandyBasket>();
    }

    void Activate()
    {
        active = true;
        burstY = GameObject.FindGameObjectWithTag("Player").transform.position.y + Random.Range(0F, 4F);
        burstY = Mathf.Clamp(burstY, 0F, 5F);
    }

    void Burst()
    {
        if(Mathf.Abs(transform.position.x)<=4.6F)
            GameObject.Instantiate(BurstObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
        

        if(active || transform.localPosition.magnitude>1.5F)
            body.AddForce(Vector2.left * transform.position.x * 0.4F);

        if (transform.localPosition.y <= -0.7F && !active)
            Activate();

        if (active && transform.position.y <= burstY)
        {
            body.gravityScale = 0;
            body.drag += 24F * Time.deltaTime;
            if (body.velocity.magnitude <= 0.25F)
                Burst();
        }
	}
}
