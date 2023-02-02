using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            healthBar.setHealth(0);
        }

        if(collision.tag == "Ball")
        {
            Destroy(collision.gameObject);
        }
    }
}
