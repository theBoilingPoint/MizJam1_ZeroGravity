using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSensor : PlayerController
{
    [SerializeField] private float extraHeightText;
    [SerializeField] private LayerMask ground;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public bool isFacingLeft()
    {
        return trans.localScale.x == -1;
    }

    public bool isGroundedBoxCast()
    {
        if (bc != null)
        {
            return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraHeightText, ground);
        }
        return false;

    }

    public bool isGroundedRayCast()
    {
        if (bc != null)
        {
            Vector2 position = isFacingLeft() ? transform.position - new Vector3(0.2f, 0f, 0f) : transform.position + new Vector3(0.2f, 0f, 0f);
            return Physics2D.Raycast(position, Vector2.down, bc.bounds.size.y + extraHeightText, ground);
        }
        return false;
    }

    public RaycastHit2D getCurrentPlatform()
    {
        return Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.size.y + extraHeightText, ground);
    }

    private void OnDrawGizmos()
    {
        if (bc != null)
        {
            Gizmos.color = isGroundedBoxCast() ? Color.green : Color.white;
            Vector2 boxCastGroundCheckCenter = new Vector2(bc.bounds.center.x, bc.bounds.center.y - extraHeightText);
            Gizmos.DrawWireCube(boxCastGroundCheckCenter, bc.bounds.size);

            Gizmos.color = isGroundedRayCast() ? Color.blue : Color.red;
            Vector2 rayCastGroundCheckCenter = bc.bounds.center;
            Gizmos.DrawRay(rayCastGroundCheckCenter, Vector2.down);

        }
    }
}
