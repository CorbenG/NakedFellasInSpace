using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject alienSprite;
    public SpriteRenderer freezeSprite;
    public GameObject bubbleSprite;
    public GameObject bounceSound;
    public GameObject voiceSound;
    public GameObject freezeSound;
    public GameObject squisher;
    public float minSpeed;
    public float maxSpeed;
    public float bouncePower;
    public float freezeTime;
    public float shakeTime;
    public float shakeSize;
    public float shakeSpeed;
    public GameObject spawnAnim;
    public GameObject fadeAnim;
    public GameObject display_1;
    public GameObject display_2;
    public GameObject body;
    public GameObject gems;
    public GameObject outline_1;
    public GameObject outline_2;
    public GameObject pants;
    public GameObject shirt;
    public GameObject boxers;
    public GameObject no;


    public Color[] request_colors;
    public Color[] alien_colors;
    public Color[] gem_colors;
    public Color[] freezing_colors;


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

    float voicePitch;
    Color skinColor;

    float jiggle_offset = 0f;
    float jiggle_speed = 0f;
    Vector3 sprite_origin;
    bool lostLife = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        id2_satisfied = needs_one;
        request_id1 = Random.Range(0, 4);
        request_id2 = Random.Range(0, 4);
        pants.SetActive(false);
        shirt.SetActive(false);
        boxers.SetActive(true);

        Game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        speed = Random.Range(minSpeed, maxSpeed);
        Instantiate(spawnAnim, transform.position, transform.rotation);
        sprite = alienSprite.GetComponent<SpriteRenderer>();
        rotation_speed = Random.Range(-100, 100);
        voicePitch = Random.Range(0, 2.5f);
        skinColor = alien_colors[Random.Range(0, alien_colors.Length)];
        sprite.color = skinColor;

        generate_appearance();
    }

    // Update is called once per frame
    void Update()
    {
        jiggle();
        if (voicePitch < 0.5)
        {
            voicePitch = Random.Range(-2.5f, 2.5f);
        }

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
            alienSprite.GetComponent<Animator>().SetTrigger("Bounce");
        }

        if (freezeTimer >= freezeTime)
        {
            sprite.color = Color.blue;
            //alienSprite.GetComponent<Animator>().SetBool("Shaking", false);
            GetComponent<CircleCollider2D>().enabled = false;
            display_1.SetActive(false);
            display_2.SetActive(false);
            bubbleSprite.SetActive(false);
            if (!lostLife)
            {
                Instantiate(freezeSound);
                Game.health -= 1;
                Game.updateHealth();
                lostLife = true;
            }
            if (Vector3.Magnitude(transform.position) > 12)
            {
                Destroy(this.gameObject);
            }
            //Lose a life and die
        }
        else if (freezeTimer > freezeTime - shakeTime)
        {
            //Shake
            Shake(((freezeTimer - freezeTime + shakeTime) / shakeTime) * shakeSize);
        }
        else
        {
            //alienSprite.GetComponent<Animator>().SetBool("Shaking", false);
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
            bop_it(50);
            randomDir = new Vector2(randomDir.x, -randomDir.y);
            rotation_speed *= -1;
            BounceNoise();
        }
        if (collision.gameObject.tag == "WorldBorderSide")
        {
            bop_it(50);
            randomDir = new Vector2(-randomDir.x, randomDir.y);
            BounceNoise();
            rotation_speed *= -1;
        }
    }

    void BounceNoise()
    {
        newBounceSound = Instantiate(bounceSound);
        newBounceSound.GetComponent<AudioSource>().volume = 0.1f;
    }

    void VoiceNoise()
    {
        newBounceSound = Instantiate(voiceSound);
        newBounceSound.GetComponent<AudioSource>().pitch = voicePitch;
    }

    public void get_clothes(int id)
    {
        if ((id == request_id1) && !id1_satisfied)
        {
            //alienSprite.GetComponent<Animator>().speed = 1;
            //alienSprite.GetComponent<Animator>().SetTrigger("Bounce");
            bop_it(100);
            VoiceNoise();
            freezeTimer -= freezeTime / 2;
            id1_satisfied = true;
            Game.updateScore(100);
            pants.SetActive(true);
            pants.GetComponent<SpriteRenderer>().color = request_colors[request_id1];
            boxers.SetActive(false);
        }
        else if ((id == request_id2) && !id2_satisfied)
        {
            //alienSprite.GetComponent<Animator>().speed = 1;
            //alienSprite.GetComponent<Animator>().SetTrigger("Bounce");
            bop_it(100);
            VoiceNoise();
            freezeTimer -= freezeTime / 2;
            id2_satisfied = true;
            Game.updateScore(100);
            shirt.SetActive(true);
            shirt.GetComponent<SpriteRenderer>().color = request_colors[request_id2];
        }
        else
        {
            no.GetComponent<AudioSource>().pitch = voicePitch;
            no.GetComponent<AudioSource>().Play();
        }

        if (id1_satisfied && id2_satisfied)
        {
            StartCoroutine(waitndestroy(0.5f));
        }
    }

    
    void Shake(float amplitude)
    {
        squisher.transform.localPosition = new Vector3(Random.Range(-amplitude,amplitude),Random.Range(-amplitude, amplitude), 0);
        freezeSprite.color = Color.Lerp(freezing_colors[0], freezing_colors[1], amplitude/shakeSize);
        //alienSprite.GetComponent<Animator>().SetBool("Shaking", true);
        //alienSprite.GetComponent<Animator>().speed = (freezeTimer - shakeTime) / (freezeTime - shakeTime);
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

    private void bop_it(float strength)
    {
        jiggle_speed += strength;
    }

    private void jiggle()
    {
        float jiggle_amp = 1.4f;
        jiggle_speed = Mathf.Lerp(jiggle_speed, -jiggle_offset * 30, Time.deltaTime*20);
        jiggle_offset += jiggle_speed * Time.deltaTime;
        squisher.transform.localScale = new Vector3(Mathf.Pow(jiggle_amp, jiggle_offset), Mathf.Pow(jiggle_amp,-jiggle_offset), 0);
    }

    private void generate_appearance()
    {
        outline_1.GetComponent<SpriteRenderer>().color = request_colors[request_id1];
        outline_2.GetComponent<SpriteRenderer>().color = request_colors[request_id2];
        body.GetComponent<SpriteRenderer>().color = alien_colors[request_id1];
        gems.GetComponent<SpriteRenderer>().color = request_colors[request_id2];

    }

    private IEnumerator waitndestroy(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        Instantiate(fadeAnim, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
