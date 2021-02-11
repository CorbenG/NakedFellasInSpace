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

    Vector3 speed = Vector3.zero;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        speed.x = Mathf.Lerp(speed.x, PlayerInput.normalized.x * MaxSpeed, Time.deltaTime * CurrentLerp);
        speed.y = Mathf.Lerp(speed.y, PlayerInput.normalized.y * MaxSpeed, Time.deltaTime * CurrentLerp);

        body.velocity = speed;



        float VerticalSquish = Mathf.Pow(MaxSquish, Mathf.Min(Mathf.Abs(speed.y) / MaxSpeed, 1) * Mathf.Sign(speed.y));
        float MovementTilt = Mathf.Min(Mathf.Abs(speed.x) / MaxSpeed , 1) * MaxTilt * (-1.0f) * Mathf.Sign(speed.x);
        graphics.transform.localEulerAngles = new Vector3(0, 0, MovementTilt);
        graphics.transform.localScale = new Vector3(graphics.transform.localScale.x, VerticalSquish, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WorldBorder")
        {
            body.velocity = Vector3.zero;
            speed -= new Vector3(9.0f * transform.position.x, 16.0f * transform.position.y, 0.0f) * BorderPushbackForce;
        }

    }

}
