using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject ammoPrefab;
    public float enemiesPerAmmo;
    float ammoCountdown;
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
    public GameObject popAnim;
    public GameObject gameOverSound;
    public bool gameOver = false;

    float currentGameStart;

    public AudioClip ammoSpawnClip;

    // Start is called before the first frame update
    void Start()
    {
        currentGameStart = Time.deltaTime;
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
            ammoCountdown += 1;
            if (ammoCountdown > enemiesPerAmmo)
            {
                GetComponent<AudioSource>().PlayOneShot(ammoSpawnClip);
                ammoCountdown -= enemiesPerAmmo;
                Instantiate(ammoPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-3, 3), 0), transform.rotation);
            }
            spawnTimerCounter = spawnTime;
        }
        spawnTimerCounter -= Time.deltaTime + ((Time.time - currentGameStart) / spawnIncreaseFactor);
    }

    public void updateScore (int scoring)
    {
        score += scoring;
        scoreAnimationSpeed = scorePunchPower;
        //Animate here
    }

    public void updateHealth()
    {
        if(health == 2)
        {
            Instantiate(popAnim, healthOne.transform.position, healthOne.transform.rotation);
            healthOne.GetComponent<Image>().enabled = false;
            healthOne.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        else if (health == 1)
        {
            Instantiate(popAnim, healthTwo.transform.position, healthTwo.transform.rotation);
            healthTwo.GetComponent<Image>().enabled = false;
            healthTwo.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        else if (health == 0)
        {
            Instantiate(popAnim, healthThree.transform.position, healthThree.transform.rotation);
            healthThree.GetComponent<Image>().enabled = false;
            healthThree.transform.GetChild(0).GetComponent<Image>().enabled = true;
            GameOver();
        }
    }

    void GameOver()
    {
        if (!gameOver)
        {
            Instantiate(gameOverSound);
        }
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.GetChild(1).GetComponent<Text>().text = "" + score;
        gameOverScreen.transform.GetChild(3).GetComponent<Text>().text = "Time: " + Mathf.Floor((Time.time - currentGameStart)/60) + ":" + Mathf.Round((Time.time - currentGameStart)%60);
        scoretext.enabled = false;
        gameOver = true;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
