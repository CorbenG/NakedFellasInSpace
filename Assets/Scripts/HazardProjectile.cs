using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HazardProjectile : MonoBehaviour
{
    public float warningTime;
    public GameObject warning;
    public GameObject fadeAnim;

    public float vertWarningOffset;
    public float horiWarningOffset;

    GameObject player;
    float rotation;

    public int direction;

    public float speed;

    float counter;

    public AudioClip hitSound;
    public AudioClip warningSound;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameController").GetComponent<AudioSource>().PlayOneShot(warningSound);
        player = GameObject.Find("Player");
        
        GameObject newWarning = Instantiate(warning, transform.position, Quaternion.identity);
        //Top
        if (direction == 0)
        {
            newWarning.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - vertWarningOffset, transform.position.z));
        }
        //Bottom
        else if (direction == 1)
        {
            newWarning.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + vertWarningOffset - 0.5f, transform.position.z));
        }
        //Left Side
        else if (direction == 2)
        {
            newWarning.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + horiWarningOffset - 0.5f, transform.position.y, transform.position.z));
        }
        //Right Side
        else if (direction == 3)
        {
            newWarning.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - horiWarningOffset + 0.5f, transform.position.y, transform.position.z));
        }
        newWarning.GetComponent<EnemySpawn>().aliveTime = warningTime + 0.5f;
        newWarning.transform.SetParent(GameObject.Find("Canvas").transform);
        newWarning.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= warningTime)
        {
            gameObject.transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }
        else
        {
            rotation = Mathf.Rad2Deg * Mathf.Atan2((transform.position.y - player.transform.position.y), (transform.position.x - player.transform.position.x));
            transform.eulerAngles = new Vector3(0, 0, rotation);
        }
        

        counter += Time.deltaTime;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<GameController>().health -= 1;
            GameObject.Find("GameController").GetComponent<GameController>().updateHealth();
            GameObject.Find("GameController").GetComponent<AudioSource>().PlayOneShot(hitSound);
            Instantiate(fadeAnim, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
