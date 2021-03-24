using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBounce : MonoBehaviour
{
    public float gravity;
    public float xMaxInitialVelocity;
    public float yMaxInitialVelocity;

    public Vector3 velocity;

    public float MaxRotateSpeed;
    float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Random.Range(-xMaxInitialVelocity, xMaxInitialVelocity), Random.Range(yMaxInitialVelocity/3, yMaxInitialVelocity), 0);
        rotateSpeed = Random.Range(-MaxRotateSpeed, MaxRotateSpeed);
        Debug.Log("popup created at " + transform.position.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
            Debug.Log("popup destroyed at " + transform.position.ToString());
        }
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * Mathf.Abs(velocity.y) / gravity);
        velocity.y -= gravity * Time.deltaTime;
    }
}
