using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : PlayerController
{
    private enum State { idle, walking, jumping, falling, dead};
    private State state;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateFSM();
        //Debug.Log(state);
    }

    private void stateFSM()
    {
        if (currentHealth > 0)
        {
            if (!playerSensor.isGroundedBoxCast())
            {
                setInAirState();
            }
            else
            {
                setGroundedState();
            }
        }
        else
        {
            setDead();
        }
        
    }

    private void setInAirState()
    {
        if (rb.velocity.y > .1f)
        {
            setJumping();
        }
        else if (rb.velocity.y < .1f)
        {
            setFalling();
        }
    }

    private void setGroundedState()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            setWalking();
        }
        else
        {
            setIdle();
        }
    }

    public void setIdle()
    {
        state = State.idle;
        anim.SetInteger("State", (int)State.idle);
    }

    public void setWalking()
    {
        state = State.walking;
        anim.SetInteger("State", (int)State.walking);
    }

    public void setJumping()
    {
        state = State.jumping;
        anim.SetInteger("State", (int)State.jumping);
    }

    public void setFalling()
    {
        state = State.falling;
        anim.SetInteger("State", (int)State.falling);
    }

    public void setDead()
    {
        state = State.dead;
        anim.SetInteger("State", (int)State.dead);
    }

}
