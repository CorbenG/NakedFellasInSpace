using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    int AmmoType = 0;

    public GameObject AmmoTypeDisplay;
    public float DisplayLerpSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                AmmoType = (AmmoType + 1)%4;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                AmmoType = (AmmoType - 1)%4;
            }
        }

        AmmoTypeDisplay.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(AmmoTypeDisplay.transform.localEulerAngles.z, 90.0f * AmmoType, Time.deltaTime * DisplayLerpSpeed) );
        
    }
}
