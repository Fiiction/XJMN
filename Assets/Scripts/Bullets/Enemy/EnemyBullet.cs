using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float damage = 10F;
    public float angVec = 0F;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 36);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, angVec * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damage);
            Destroy(gameObject);
        }
    }

    
}
