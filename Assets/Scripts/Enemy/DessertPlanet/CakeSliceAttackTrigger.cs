using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSliceAttackTrigger : MonoBehaviour {

    public CakeSlice cakeSlice;
    bool active = false;
	// Use this for initialization
	void Start () {
		
	}

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.32F);
        active = true;
        yield return new WaitForSeconds(0.2F);
        active = false;

    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (active)
                cakeSlice.DealDamage(collision.gameObject);
            else
                cakeSlice.TryAttack();
        }
    }

}
