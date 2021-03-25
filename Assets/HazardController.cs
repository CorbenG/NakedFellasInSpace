using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public GameObject hazard;

    public float screenWidth;
    public float screenHeight;

    public float meteoriteCooldown;
    public float difficultyRamp;
    public float minCooldown;

    float currentCD;


    int side;

    float counter;
    float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = meteoriteCooldown;
        currentCD = meteoriteCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= targetTime)
        {
            targetTime = Time.time + currentCD;
            currentCD -= difficultyRamp;
            if(currentCD < minCooldown)
            {
                currentCD = minCooldown;
            }
            side = Random.Range(0, 4);
            //Top
            if(side == 0)
            {
                Instantiate(hazard, new Vector3(Random.Range(-screenWidth, screenWidth), screenHeight, 0), transform.rotation);
            }
            //Bottom
            else if (side == 1)
            {
                Instantiate(hazard, new Vector3(Random.Range(-screenWidth, screenWidth), -screenHeight, 0), transform.rotation);
            }
            //Left Side
            else if (side == 2)
            {
                Instantiate(hazard, new Vector3(-screenWidth, Random.Range(-screenHeight, screenHeight), 0), transform.rotation);
            }
            //Right Side
            else if (side == 2)
            {
                Instantiate(hazard, new Vector3(screenWidth, Random.Range(-screenHeight, screenHeight), 0), transform.rotation);
            }
        }

        counter += Time.deltaTime;
    }
}
