using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public Text scoretext;
    public GameObject textContainer;
    public float scorePunchPower;
    public float scoreBouncyness;
    public float scoreSpeed;
    public float scoreAmplitude;
    float scoreAnimationPosition = 0.0f;
    float scoreAnimationSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = score.ToString();
        scoreAnimationSpeed = Mathf.Lerp(scoreAnimationSpeed, -scoreAnimationPosition * scoreBouncyness, Time.deltaTime * scoreSpeed);
        scoreAnimationPosition += scoreAnimationSpeed * Time.deltaTime;
        textContainer.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Pow(scoreAmplitude,scoreAnimationPosition), Mathf.Pow(scoreAmplitude,-scoreAnimationPosition),1);
        
    }

    public void updateScore (int scoring)
    {
        score += scoring;
        scoreAnimationSpeed = scorePunchPower;
        //Animate here
    }
}
