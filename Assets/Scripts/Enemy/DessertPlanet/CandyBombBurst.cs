using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBombBurst : MonoBehaviour {

    public float damage = 15F;

    IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(21F / 12F);
        GetComponent<CircleCollider2D>().radius = 1.9F;
        yield return new WaitForSeconds(3F / 12F);
        Destroy(GetComponent<CircleCollider2D>());
    }
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 32F / 12F);
        StartCoroutine(BurstCoroutine());
	}


	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8)
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damage);
            Vector2 vec = collision.gameObject.transform.position - transform.position;
            
            collision.GetComponent<Rigidbody2D>().AddForce(vec.normalized * (9F - 1.5F * vec.magnitude), ForceMode2D.Impulse);
        }
    }
}
