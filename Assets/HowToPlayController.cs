using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;

    int screen = 1;

    public Button leftButtonObj;
    public Button rightButtonObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(screen == 1)
        {
            leftButtonObj.interactable = false;
            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
        }
        else if (screen == 2)
        {
            leftButtonObj.interactable = true;
            rightButtonObj.interactable = true;
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
        }
        else if (screen == 3)
        {
            rightButtonObj.interactable = false;
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
        }
    }

    public void leftButton()
    {
        screen--;
    }

    public void rightButton()
    {
        screen++;
    }

    public void exitScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
