using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneIndex;

    void SwitchScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
