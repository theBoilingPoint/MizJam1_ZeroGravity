using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float blinkTime;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        StartCoroutine(DoBlinks());
    }

    public IEnumerator DoBlinks()
    {
        boxCollider.enabled = false;
        circleCollider.enabled = false;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        circleCollider.enabled = true;
    }
}
