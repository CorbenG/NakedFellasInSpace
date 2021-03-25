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
        currentCD = Time.time + meteoriteCooldown;
        targetTime = currentCD;
        counter = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= targetTime && !gameObject.GetComponent<GameController>().gameOver)
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
                GameObject newHazard = Instantiate(hazard, new Vector3(Random.Range(-(screenWidth - 2), (screenWidth - 2)), screenHeight, 0), transform.rotation);
                newHazard.GetComponent<HazardProjectile>().direction = 0;
            }
            //Bottom
            else if (side == 1)
            {
                GameObject newHazard = Instantiate(hazard, new Vector3(Random.Range(-(screenWidth - 2), (screenWidth - 2)), -screenHeight, 0), transform.rotation);
                newHazard.GetComponent<HazardProjectile>().direction = 1;
            }
            //Left Side
            else if (side == 2)
            {
                GameObject newHazard = Instantiate(hazard, new Vector3(-screenWidth, Random.Range(-(screenHeight - 2), (screenHeight - 2)), 0), transform.rotation);
                newHazard.GetComponent<HazardProjectile>().direction = 2;
            }
            //Right Side
            else if (side == 3)
            {
                GameObject newHazard = Instantiate(hazard, new Vector3(screenWidth, Random.Range(-(screenHeight - 2), (screenHeight - 2)), 0), transform.rotation);
                newHazard.GetComponent<HazardProjectile>().direction = 3;
            }
        }

        counter += Time.deltaTime;
    }
}
