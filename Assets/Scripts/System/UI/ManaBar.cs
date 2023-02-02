using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private float maxMana;
    private float currentMana;

    [SerializeField] private float manaIncreaseValue;
    [SerializeField] private float manaIncreaseStartTime;
    private float manaIncreaseTime;

    [SerializeField] private float manaBufferStartTime;
    private float manaBufferTime;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        setMaxMana(maxMana);
        currentMana = maxMana;
        manaIncreaseTime = manaIncreaseStartTime;
        manaBufferTime = manaBufferStartTime;
    }

    void Update()
    {
        increaseMana();
        setSlider(currentMana);
        //Debug.Log("Current mana is: " + getCurrentMana());
    }

    public void decreaseMana(float decrement)
    {
        currentMana -= decrement;
    }

    public float getCurrentMana()
    {
        return currentMana;
    }

    private void increaseMana()
    {
        if (currentMana > 0)
        {
            manaIncreaseTimer();
        }
        else
        {
            manaBufferTimer();
        }
    }

    private void manaBufferTimer()
    {
        if (manaBufferTime <= 0)
        {
            currentMana = maxMana;
            manaBufferTime = manaBufferStartTime;
        }
        else
        {
            manaBufferTime -= Time.deltaTime;
        }
    }

    private void manaIncreaseTimer()
    {
        if (manaIncreaseTime <= 0)
        {
            currentMana += manaIncreaseValue;
            manaIncreaseTime = manaIncreaseStartTime;
        }
        else
        {
            manaIncreaseTime -= Time.deltaTime;
        }
    }

    private void setSlider(float mana)
    {
        slider.value = mana;
    }

    private void setMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
}
