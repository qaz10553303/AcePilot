using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private bool menuReady;
    private bool GameStateReady;
    private bool InstructionsReady;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (menuReady == true)
        {
            if (timer > 0.28)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (GameStateReady == true)
        {
            if (timer > 0.28)
            {
                SceneManager.LoadScene(1);
            }
        }
        if (InstructionsReady == true)
        {
            if (timer > 0.28)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        timer = 0;
        menuReady = true;
    }

    public void LoadGameState()
    {
        Time.timeScale = 1;
        timer = 0;
        GameStateReady = true;
    }

    public void LoadInstructions()
    {
        Time.timeScale = 1;
        timer = 0;
        InstructionsReady = true;
    }
}
