using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    private float spawnTimerCounter;
    public float spawnIncreaseFactor;
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
        spawnTimerCounter = 3;
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = score.ToString();
        scoreAnimationSpeed = Mathf.Lerp(scoreAnimationSpeed, -scoreAnimationPosition * scoreBouncyness, Time.deltaTime * scoreSpeed);
        scoreAnimationPosition += scoreAnimationSpeed * Time.deltaTime;
        textContainer.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Pow(scoreAmplitude,scoreAnimationPosition), Mathf.Pow(scoreAmplitude,-scoreAnimationPosition),1);
        if(spawnTimerCounter <= 0)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-3, 3), 0), transform.rotation);
            spawnTimerCounter = spawnTime;
        }
        spawnTimerCounter -= Time.deltaTime + (Time.time / spawnIncreaseFactor);
    }

    public void updateScore (int scoring)
    {
        score += scoring;
        scoreAnimationSpeed = scorePunchPower;
        //Animate here
    }
}
