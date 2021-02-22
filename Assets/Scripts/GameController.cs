﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public float health = 3;
    public GameObject healthOne;
    public GameObject healthTwo;
    public GameObject healthThree;
    public GameObject gameOverScreen;
    public bool gameOver = false;

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

        updateHealth();
    }

    public void updateScore (int scoring)
    {
        score += scoring;
        scoreAnimationSpeed = scorePunchPower;
        //Animate here
    }

    void updateHealth()
    {
        if(health == 2)
        {
            healthOne.GetComponent<Image>().enabled = false;
            healthOne.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else if (health == 1)
        {
            healthTwo.GetComponent<Image>().enabled = false;
            healthTwo.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else if (health == 0)
        {
            healthThree.GetComponent<Image>().enabled = false;
            healthThree.transform.GetChild(0).GetComponent<Image>().enabled = false;
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.GetChild(1).GetComponent<Text>().text = "" + score;
        gameOver = true;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
