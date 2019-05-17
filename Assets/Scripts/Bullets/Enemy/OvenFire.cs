using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFire : MonoBehaviour
{

    public float angle,alpha;
    SpriteRenderer SR;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
