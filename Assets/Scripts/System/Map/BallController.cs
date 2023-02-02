using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.AddForce(new Vector2(1, 1), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;  
    }

}
