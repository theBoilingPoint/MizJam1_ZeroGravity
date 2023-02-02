using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DissolveMaterialController : MonoBehaviour
{
    Material material;

    public bool isDissolving;
    float fade = 1f;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if(fade <= 0f)
            {
                fade = 0;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
