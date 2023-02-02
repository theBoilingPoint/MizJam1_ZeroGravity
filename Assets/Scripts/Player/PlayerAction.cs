using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : PlayerController
{
    [SerializeField] private float mouseTraceDelayStartTime; // This is to prevent the cursor from moving too quickly
    private float mouseTraceDelay;

    private ManaBar manaBar;
    [SerializeField]  private float manaDecrement;

    protected override void Awake()
    {
        base.Awake();
        manaBar = GameObject.Find("ManaBar").GetComponent<ManaBar>();
    }

    protected override void Start()
    {
        base.Start();
        mouseTraceDelay = mouseTraceDelayStartTime;
    }

    protected override void Update()
    {
        base.Update();
        traceMousePosition();
        stop();
    }

    protected void stop()
    {

        bool hasPulled = Input.GetKey(KeyCode.LeftControl);
        float mana = manaBar.getCurrentMana();

        if (hasPulled && mana > 0)
        {
            rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
            manaBar.decreaseMana(manaDecrement);
        }

    }

    private void traceMousePosition()
    {
        if (mouseTraceDelay <= 0)
        {
            rb.AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.trans.position, ForceMode2D.Impulse);
            mouseTraceDelay = mouseTraceDelayStartTime;
        }
        else
        {
            mouseTraceDelay -= Time.deltaTime;
        }
    }

}
