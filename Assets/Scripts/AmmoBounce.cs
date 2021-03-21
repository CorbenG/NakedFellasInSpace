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
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().position += velocity * Time.deltaTime;
        GetComponent<RectTransform>().Rotate(0, 0, rotateSpeed * Time.deltaTime);
        velocity.y -= gravity * Time.deltaTime;
    }
}
