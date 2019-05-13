using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloud : MonoBehaviour {

    GameObject Flash;
    bool startedShooting = false;
    void Shoot()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 vec = (target.transform.position - transform.position).normalized;
        GameObject obj = GameObject.Instantiate(Flash, transform.position + 0.4F * vec, Quaternion.identity);

        obj.GetComponent<Rigidbody2D>().velocity = (Vector2)vec * 5F;


    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(0.5F);
            Shoot();
            yield return new WaitForSeconds(0.5F);
            Shoot();

            yield return new WaitForSeconds(3F);
        }

    }

    // Use this for initialization
    void Start ()
    {
        Flash = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/Flash");
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= 9.5 && transform.position.y >= -4.5 && Mathf.Abs(transform.position.x) <= 5
            && !startedShooting)
        {
            startedShooting = true;
            StartCoroutine(ShootCoroutine());
        }
    }
}
