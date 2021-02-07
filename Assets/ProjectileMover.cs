using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 MovementDirection;
    public float MovementSpeed;
    public float RotationSpeed;
    public GameObject[] sprites;
    public int AmmoType;
    Rigidbody2D body;
    IEnumerator Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprites[0].SetActive(AmmoType == 0);
        sprites[1].SetActive(AmmoType == 1);
        sprites[2].SetActive(AmmoType == 2);
        sprites[3].SetActive(AmmoType == 3);
        yield return StartCoroutine("WaitAndDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        body.MovePosition(transform.position + MovementDirection * Time.deltaTime * MovementSpeed);
        body.MoveRotation(body.rotation + Time.deltaTime * RotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WorldBorder")
        {
            //Debug.Log("destroyed self due to reaching world border");
            //Destroy(gameObject);
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("destroyed self due to timeout");
        Destroy(gameObject);
    }
}
