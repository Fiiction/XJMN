using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefault : MonoBehaviour {

    PlayerInterface pInterface;
    Rigidbody2D body;
    public float shootInterval = 0.25F, bulletSpeed = 8F;
    float lastShootTime;
    GameObject Bullet;
    Vector2 direction;

	// Use this for initialization
	void Start ()
    {
        pInterface = GetComponent<PlayerInterface>();
        body = GetComponent<Rigidbody2D>();
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet/FriendlyBullet/FrdBlt_Bullet");

    }

    void Shoot()
    {
        direction = transform.rotation * Vector2.up;
        GameObject obj = GameObject.Instantiate(Bullet, transform.position + 0.5F*(Vector3)direction, Quaternion.identity);

        obj.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        body.AddForce(direction * -2.5F, ForceMode2D.Impulse);

        lastShootTime = Time.time;
    }

	// Update is called once per frame
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        if (pInterface.isShooting && Time.time >= lastShootTime + shootInterval)
        {
            Shoot();
        }
    }
}
