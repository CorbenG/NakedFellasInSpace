using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "Score: " + score;
    }

    public void updateScore (int scoring)
    {
        score += scoring;
        //Animate here
    }
}
