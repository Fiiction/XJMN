using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCloud : MonoBehaviour {

    public GameObject Breath;
    public float breathCD = 5F;
    float nextBreathTime;

    void Breathe()
    {
        GameObject.Instantiate(Breath, transform.position + 1.2F * (transform.rotation * Vector3.down), transform.rotation);
        nextBreathTime = breathCD;

    }

	// Use this for initialization
	void Start ()
    {
        nextBreathTime = breathCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 9.5 && transform.position.y >= -4.5 && Mathf.Abs(transform.position.x) <= 5)
        {
            nextBreathTime -= Time.deltaTime;
            if (nextBreathTime <= 0F)
            {
                Breathe();
            }
        }

    }
}
