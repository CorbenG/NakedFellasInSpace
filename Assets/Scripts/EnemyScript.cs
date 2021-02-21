using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject alienSprite;
    public GameObject bounceSound;
    public float minSpeed;
    public float maxSpeed;
    public float bouncePower;
    public float freezeTime;
    public float shakeTime;
    public float shakeSize;
    public float shakeSpeed;
    public GameObject spawnAnim;
    public GameObject display_1;
    public GameObject display_2;
    public Color[] request_colors;

    GameController Game;
    Vector2 randomDir;
    Rigidbody2D rb;
    float speed;
    GameObject newBounceSound;
    SpriteRenderer sprite;
    float spawnTime = 0.1f;
    float spawnTimer = 0;
    float freezeTimer = 0;

    public int request_id1 = 0;
    public int request_id2 = 0;
    public bool needs_one = false;

    bool id1_satisfied = false;
    bool id2_satisfied = false;
    float rotation_speed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        id2_satisfied = needs_one;
        request_id1 = Random.Range(0, 3);
        request_id2 = Random.Range(0, 3);


        Game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        speed = Random.Range(minSpeed, maxSpeed);
        Instantiate(spawnAnim, transform.position, transform.rotation);
        sprite = alienSprite.GetComponent<SpriteRenderer>();
        rotation_speed = Random.Range(-100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        freezeTimer += Time.deltaTime;
        rb.rotation += rotation_speed*Time.deltaTime;

        //Once spawned
        if (sprite.enabled == true)
        {
            rb.velocity = randomDir * speed;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }

        if (freezeTimer >= freezeTime)
        {
            sprite.color = Color.blue;
            alienSprite.GetComponent<Animator>().SetBool("Shaking", false);
            GetComponent<CircleCollider2D>().enabled = false;
            display_1.SetActive(false);
            display_2.SetActive(false);
            if(Vector3.Magnitude(transform.position) > 12)
            {
                Destroy(this.gameObject);
            }
            //Lose a life and die
        }
        else if (freezeTimer > freezeTime - shakeTime)
        {
            //Shake
            Shake();
        }
        else
        {
            alienSprite.GetComponent<Animator>().SetBool("Shaking", false);
            //Hide until animation spawns in
            if (spawnTimer >= spawnTime && sprite.enabled == false)
            {
                sprite.enabled = true;
            }

            display_1.GetComponent<SpriteRenderer>().color = request_colors[request_id1];
            display_2.GetComponent<SpriteRenderer>().color = request_colors[request_id2];
            display_1.SetActive(!id1_satisfied);
            display_2.SetActive(!id2_satisfied);
        }
        
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ProjectileMover>() != null)
        {
           
            Destroy(collision.gameObject);
            get_clothes(collision.gameObject.GetComponent<ProjectileMover>().AmmoType);
        }
        if (collision.gameObject.tag == "WorldBorderTop")
        {
            randomDir = new Vector2(randomDir.x, -randomDir.y);
            rotation_speed *= -1;
            BounceNoise();
        }
        if (collision.gameObject.tag == "WorldBorderSide")
        {
            randomDir = new Vector2(-randomDir.x, randomDir.y);
            BounceNoise();
            rotation_speed *= -1;
        }
    }

    void BounceNoise()
    {
        newBounceSound = Instantiate(bounceSound);
        newBounceSound.GetComponent<AudioSource>().volume = 0.5f;
    }

    public void get_clothes(int id)
    {
        if ((id == request_id1) && !id1_satisfied)
        {
            freezeTimer -= freezeTime / 2;
            id1_satisfied = true;
            Game.updateScore(100);
        } 
        else if ((id == request_id2) && !id2_satisfied)
        {
            freezeTimer -= freezeTime / 2;
            id2_satisfied = true;
            Game.updateScore(100);
        }

        if (id1_satisfied && id2_satisfied)
        {
            Destroy(gameObject);
        }
    }

    
    void Shake()
    {
        alienSprite.GetComponent<Animator>().SetBool("Shaking", true);
        alienSprite.GetComponent<Animator>().speed = (freezeTimer - shakeTime) / (freezeTime - shakeTime);
        /*
        if(alienSprite.transform.position.x < shakeSize && alienSprite.transform.position.x > -shakeSize)
        {

        }
        if (alienSprite.transform.position.y < shakeSize && alienSprite.transform.position.y > -shakeSize)
        {

        }
        alienSprite.transform.position = new Vector3 (alienSprite.transform.position.x + Random.Range(-shakeSpeed, shakeSpeed), 
            alienSprite.transform.position.y + Random.Range(-shakeSpeed, shakeSpeed), alienSprite.transform.position.z);
        */
    }
    
}
