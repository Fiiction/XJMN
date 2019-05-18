using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBullet : MonoBehaviour
{
    public float damage = 30F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damage * Time.fixedDeltaTime);
        }
    }
}
