using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator camAnim;

    private void Awake()
    {
        camAnim = GetComponent<Animator>();
    }

    public void shakeCamera()
    {
        camAnim.SetTrigger("shake");
    }
}
