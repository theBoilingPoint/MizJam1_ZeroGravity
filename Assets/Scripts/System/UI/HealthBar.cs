using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public bool isVulnerable;

    [SerializeField] private float healthDecreaseValue;
    [SerializeField] private float healthDecreaseScaleFactor;
    [SerializeField] private float healthDreaseStartTime;
    private float healthDreaseTime;

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        setMaxHealth(maxHealth);
        isVulnerable = true;
        healthDreaseTime = healthDreaseStartTime;
    }

    private void Update()
    {
        checkDecrease();
        decreaseHealth(healthDecreaseValue);
        setSlider(currentHealth);
        checkLose();
    }

    public float getCurrentMaxHealthRatio()
    {
        return currentHealth / maxHealth;
    }

    public float getMaxCurrentHealthRatio()
    {
        return maxHealth / currentHealth;
    }

    private void checkDecrease()
    {
        if (healthDreaseTime <= 0)
        {
            healthDecreaseValue += getMaxCurrentHealthRatio() * healthDecreaseScaleFactor;
            healthDreaseTime = healthDreaseStartTime;
        }
        else
        {
            healthDreaseTime -= Time.deltaTime;
        }
    }

    public void takeDamage(float damage)
    {
        if (isVulnerable) { 
            currentHealth -= damage;
            setSlider(currentHealth);
            //Debug.Log(damage + " damage taken for the player. Current health is: " + currentHealth);
        }
    }

    private void decreaseHealth(float damage)
    {
        if(healthDreaseTime <= 0)
        {
            takeDamage(damage);
            healthDreaseTime = healthDreaseStartTime;
        }
        else
        {
            healthDreaseTime -= Time.deltaTime;
        }
    }

    public float returnMaxHealth()
    {
        return maxHealth;
    }

    public float returnHealth()
    {
        return currentHealth;
    }

    public void setHealth(float health)
    {
        currentHealth = health;
    }

    private void setSlider(float health)
    {
        slider.value = health;
    }

    private void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void checkLose()
    {
        if(currentHealth <= 0)
        {
            saveScore();
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void saveScore()
    {
        int currentScore = scoreController.getCurrentScore();
        PlayerPrefs.SetInt("Score", currentScore);
        if(currentScore > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", currentScore);
        }
    }
}
