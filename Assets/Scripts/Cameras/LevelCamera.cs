using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 200;
        float width = Screen.width;
        float height = Screen.height;
        float ratio = height / width;
        GetComponent<Camera>().orthographicSize = 4.5F * ratio;
        transform.position = new Vector3(0, 9F - 4.5F * ratio,-10F);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
