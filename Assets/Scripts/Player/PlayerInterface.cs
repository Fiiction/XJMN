using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInterface : MonoBehaviour {

    public float axesX, axesY;
    public bool isShooting;

    public void ShootButtonDown()
    {
        isShooting = true;
    }
    public void ShootButtonUp()
    {
        isShooting = false;

    }

    // Use this for initialization
    void Start () {
		
	}

    //bool flag=false;

	// Update is called once per frame
	void Update ()
    {
        axesX = CrossPlatformInputManager.GetAxis("Horizontal");
        axesY= CrossPlatformInputManager.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            axesY += 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            axesY -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            axesX += 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            axesX -= 1;

        float mag = new Vector2(axesX, axesY).magnitude;
        if (mag > 1)
        {
            axesX /= mag;
            axesY /= mag;
        }

        //flag = Input.GetKey(KeyCode.Space) || Input.touchCount > 0;

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space))
            ShootButtonDown();
        if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.Space))
            ShootButtonUp();
        
    }
    
    

}
