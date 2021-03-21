using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoCount : MonoBehaviour
{
    Camera cam;
    GameObject player;
    Text text;
    int lastAmmo = 0;

    public GameObject ammoCountBounce;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(player.transform.position - new Vector3(0, 1.2f,  0));

        if(lastAmmo != player.GetComponent<PlayerShooting>().ammo)
        {
            text.text = player.GetComponent<PlayerShooting>().ammo.ToString();
            if (lastAmmo > player.GetComponent<PlayerShooting>().ammo)
            {
                GameObject newAmmoBounce = Instantiate(ammoCountBounce, transform.position, transform.rotation);
                newAmmoBounce.GetComponent<Text>().text = lastAmmo.ToString();
                newAmmoBounce.transform.parent = GameObject.Find("Canvas").transform;
                //newAmmoBounce.GetComponent<AmmoBounce>().velocity += new Vector3(player.GetComponent<PlayerMovement>().velocity.x, player.GetComponent<PlayerMovement>().velocity.y, 0);
            }
        }

        lastAmmo = player.GetComponent<PlayerShooting>().ammo;
    }
}
