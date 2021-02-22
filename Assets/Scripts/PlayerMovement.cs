using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed;
    public float AccelerationLerp;
    public float DecelerationLerp;
    public float BorderPushbackForce;
    public float MaxTilt;
    public float MaxSquish;
    public GameObject graphics;
    public GameObject bounceSound;
    public float DecelerationFactor;
    public float AccelerationFactor;
    public float xWorldBounds;
    public float yWorldBounds;

    Vector2 speed = Vector2.zero;
    Rigidbody2D body;
    GameObject Camera;
    GameController game;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 PlayerInput = new Vector3(Input.GetAxisRaw("Horizontal")*1.0f,Input.GetAxisRaw("Vertical")*1.0f,0);
        float CurrentLerp;
        if (PlayerInput.magnitude < 0.5)
        {
            CurrentLerp = DecelerationLerp;
        }
        else
        {
            CurrentLerp = AccelerationLerp;
            if (PlayerInput.x > 0.5f)
            {
                //graphics.transform.localScale = new Vector3(1, 1,1);
            }
            if (PlayerInput.x < 0.5f)
            {
                //graphics.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        //Calculate lerped input into acceleration
        speed.x = Mathf.Lerp(speed.x, PlayerInput.normalized.x * MaxSpeed, Time.deltaTime * CurrentLerp);
        speed.y = Mathf.Lerp(speed.y, PlayerInput.normalized.y * MaxSpeed, Time.deltaTime * CurrentLerp);
        */
        if (!game.gameOver)
        {

            //Calculate velocty based on input
            velocity = Vector2.ClampMagnitude(velocity + new Vector2(Input.GetAxisRaw("Horizontal") * AccelerationFactor * Time.deltaTime,
                Input.GetAxisRaw("Vertical") * AccelerationFactor * Time.deltaTime), MaxSpeed);

            //Slow down if no input
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                velocity.x = velocity.x / DecelerationFactor;
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                velocity.y = velocity.y / DecelerationFactor;
            }

            //Limit position to world bounds
            if (transform.position.x > xWorldBounds)
            {
                transform.position = new Vector3(xWorldBounds, transform.position.y, 0);
            }
            else if (transform.position.x < -xWorldBounds)
            {
                transform.position = new Vector3(-xWorldBounds, transform.position.y, 0);
            }
            if (transform.position.y > yWorldBounds)
            {
                transform.position = new Vector3(transform.position.x, yWorldBounds, 0);
            }
            else if (transform.position.y < -yWorldBounds)
            {
                transform.position = new Vector3(transform.position.x, -yWorldBounds, 0);
            }


            //Update position
            transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime, 0);

            //body.velocity = speed;


            //Squish and strech character to reflect movement
            float VerticalSquish = Mathf.Pow(MaxSquish, Mathf.Min(Mathf.Abs(velocity.y) / MaxSpeed, 1) * Mathf.Sign(velocity.y));
            float MovementTilt = Mathf.Min(Mathf.Abs(velocity.x) / MaxSpeed, 1) * MaxTilt * (-1.0f) * Mathf.Sign(velocity.x);
            graphics.transform.localEulerAngles = new Vector3(0, 0, MovementTilt);
            graphics.transform.localScale = new Vector3(graphics.transform.localScale.x, VerticalSquish, 1);
        }
        //If game is over
        else
        {
            velocity.x = velocity.x / DecelerationFactor;
            velocity.y = velocity.y / DecelerationFactor;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Screen shake and bounce for every collision
        if (collision.gameObject.tag == "WorldBorderTop" || collision.gameObject.tag == "WorldBorderSide")
        {
            GameObject newBounceSound = Instantiate(bounceSound);
            newBounceSound.GetComponent<AudioSource>().volume = 0.2f;
            Camera.GetComponent<CameraShaker>().ShakeAmplitude += Camera.GetComponent<CameraShaker>().ShotShake;
            //Variable shake depending on speed of impact
            Camera.GetComponent<CameraShaker>().JerkOffest += new Vector3(transform.position.x / 9f * (velocity.magnitude / MaxSpeed), transform.position.y / 6f * (velocity.magnitude / MaxSpeed), 0);
        }
        //Bounce off walls
        if (collision.gameObject.tag == "WorldBorderTop")
        {
            velocity.y = -velocity.y;
            //Minimum bounce distance
            if(velocity.y > 0 && velocity.y < 2)
            {
                velocity.y = 2;
            }
            else if (velocity.y < 0 && velocity.y > -2)
            {
                velocity.y = 2;
            }
            //speed.y = -speed.y * BorderPushbackForce;
        }
        if (collision.gameObject.tag == "WorldBorderSide")
        {
            velocity.x = -velocity.x;
            //Minimum bounce distance
            if (velocity.x > 0 && velocity.x < 2)
            {
                velocity.x = 2;
            }
            else if (velocity.x < 0 && velocity.x > -2)
            {
                velocity.x = 2;
            }
            //speed.x = -speed.x * BorderPushbackForce;
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        
        /*
        if (collision.gameObject.tag == "WorldBorderTop" || collision.gameObject.tag == "WorldBorderSide")
        {
            
            //speed -= new Vector3(9.0f * transform.position.x, 16.0f * transform.position.y, 0.0f) * BorderPushbackForce;
        }
        */

    }

    public void push(Vector2 where)
    {
        velocity += where;
    }

}
