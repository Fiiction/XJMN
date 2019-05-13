using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour {

    public float damage = 1F;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -3.8 || transform.position.y > 9.3 || Mathf.Abs(transform.position.x) > 4.8)
            Destroy(gameObject);

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 12 || collision.gameObject.layer == 14)
        {
            var obj = collision.gameObject.GetComponent<EnemyController>();
            if(obj)
                obj.Damage(damage);
            var b = collision.gameObject.GetComponent<BossController>();
            if (b)
                b.Damage(damage);
            var so = collision.gameObject.GetComponent<EnemySubObjController>();
            if (so)
                so.Damage(damage);
        }
        Destroy(gameObject);
    }

}
