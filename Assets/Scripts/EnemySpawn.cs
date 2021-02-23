using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float aliveTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > aliveTime)
        {
            Destroy(this.gameObject);
        }
    }
    
    private IEnumerator waitndestroy()
    {
        yield return new WaitForSeconds(1);
    }
}
