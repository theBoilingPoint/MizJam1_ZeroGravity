using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float currentHealth;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected BoxCollider2D bc;
    protected Transform trans;

    protected PlayerSensor playerSensor;
    protected PlayerState playerState;
    protected PlayerAction playerAction;

    protected HealthBar healthBar;
    protected virtual void Awake()
    {
        playerSensor = GetComponent<PlayerSensor>();
        playerState = GetComponent<PlayerState>();
        playerAction = GetComponent<PlayerAction>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        trans = this.transform;

        currentHealth = healthBar.returnHealth();
    }

    protected virtual void Update()
    {
        updateHealth();
        death();
    }

    public float returnHealth()
    {
        return currentHealth;
    }

    public Rigidbody2D returnRB()
    {
        return rb;
    }

    private void updateHealth()
    {
        currentHealth = healthBar.returnHealth();
    }

    private void death()
    {
        if (currentHealth < 0)
        {
            playerState.setDead();
        }
    }


}
