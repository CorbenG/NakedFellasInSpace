using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
