using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Start is called before the first frame update
    public float ShotJerk;
    public float ShotShake;
    public float RecoverySpeed;
    public float ShakeFalloffSpeed;

    public Vector3 JerkOffest;
    public float ShakeAmplitude;
    Vector3 Origin;
    void Start()
    {
        Origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        JerkOffest = Vector3.Lerp(JerkOffest, Vector3.zero, Time.deltaTime * RecoverySpeed);
        ShakeAmplitude = Mathf.Max(ShakeAmplitude - Time.deltaTime * ShakeFalloffSpeed, 0.0f);
        transform.position = Origin + JerkOffest + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized * ShakeAmplitude;
    }


}
