using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bounceSound;
    public float minSpeed;
    public float maxSpeed;
    public float bouncePower;

    GameController Game;
    Vector2 randomDir;
    Rigidbody2D rb;
    float speed;
    GameObject newBounceSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        speed = Random.Range(minSpeed, maxSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = randomDir * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ProjectileMover>() != null)
        {
            Game.updateScore(100);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "WorldBorderTop")
        {
            randomDir = new Vector2(randomDir.x, -randomDir.y);
            BounceNoise();
        }
        if (collision.gameObject.tag == "WorldBorderSide")
        {
            randomDir = new Vector2(-randomDir.x, randomDir.y);
            BounceNoise();
        }
    }

    void BounceNoise()
    {
        newBounceSound = Instantiate(bounceSound);
        newBounceSound.GetComponent<AudioSource>().volume = 0.5f;
    }

}
