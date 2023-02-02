using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallController : MonoBehaviour
{
    private HealthBar healthBar;
    
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            healthBar.setHealth(0);
        }
    }
}
