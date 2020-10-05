using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameState()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene(2);
    }
}
