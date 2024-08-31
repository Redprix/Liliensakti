using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private Slider hpSlider;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            hpSlider.value = health;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        hpSlider.minValue = 0;
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void InstantDeath()
    {
        Health = 0.0f;
    }
}
