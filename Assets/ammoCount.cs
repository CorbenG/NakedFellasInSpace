using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoCount : MonoBehaviour
{
    Camera cam;
    GameObject player;
    Text text;

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
        transform.position = cam.WorldToScreenPoint(player.transform.position - new Vector3(0, 1.5f,  0));
        text.text = player.GetComponent<PlayerShooting>().ammo.ToString();
    }
}
