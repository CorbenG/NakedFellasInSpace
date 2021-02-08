using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameController Game;
    ProjectileMover projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.GetComponent<ProjectileMover>() != null)
        {
            Game.score += 100;
            Destroy(hit.gameObject);
            Destroy(this.gameObject);
        }
        
    }


}
