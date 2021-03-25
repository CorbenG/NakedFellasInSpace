using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardProjectile : MonoBehaviour
{
    GameObject player;
    float rotation;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log("X: " + (transform.position.x - player.transform.position.x));
        Debug.Log("Y: " + (transform.position.y - player.transform.position.y));
        rotation = Mathf.Rad2Deg * Mathf.Atan2((transform.position.y - player.transform.position.y), (transform.position.x - player.transform.position.x));
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * -speed * Time.deltaTime);
    }
}
