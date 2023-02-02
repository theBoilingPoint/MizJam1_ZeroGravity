using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreaseController : MonoBehaviour
{
    [SerializeField] private GameObject ps;
    [SerializeField] private float increment;
    private HealthBar healthBar;

    private ScoreController scoreController;
    private CameraShake camShake;
    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            float temp = healthBar.returnHealth() + increment;
            if(temp < healthBar.returnMaxHealth())
            {
                healthBar.setHealth(temp);
            }
            else
            {
                healthBar.setHealth(healthBar.returnMaxHealth());
            }
            scoreController.increaseScoreBy(5);
            Instantiate(ps, transform.position, Quaternion.identity);
            camShake.shakeCamera();
            AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,0), 1f);
            Destroy(this.gameObject);

        }
    }

}
