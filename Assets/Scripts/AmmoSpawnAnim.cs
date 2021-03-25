using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawnAnim : MonoBehaviour
{
    float size = 0.0f;
    float target = 1.0f;
    float speed = 10.0f;
    public float lifespan = 5.0f;
    float time = 0.0f;
    public float redZone = 2.0f;
    public Color regularColor;
    public Color redZoneColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        speed = Mathf.Lerp(speed, (target - size) * 30.0f, Time.deltaTime*10);
        size += speed * Time.deltaTime;
        transform.localScale = Vector3.one * size;
        GetComponentInChildren<TextMesh>().text = Mathf.Floor(Mathf.Max(lifespan - time, 0.0f)).ToString();
        if ((lifespan - time) < redZone)
        {
            GetComponentInChildren<TextMesh>().color = redZoneColor;
        }
        else
        {
            GetComponentInChildren<TextMesh>().color = regularColor;
        }

        if (time > lifespan)
        {
            target = -1.0f;
            if (size < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
