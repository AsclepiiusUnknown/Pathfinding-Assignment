using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //[HideInInspector]
    public float health;
    [Range(0, 100)]
    public int maxHealth = 100;
    [Range(0, 50)]
    public float minDamage, maxDamage;
    private float damage;
    public Player player;
    public Text healthText;
    private float speed;
    public float normalSpeed, runningSpeed;

    private void Start()
    {
        
        health = maxHealth;
        healthText.text = health.ToString();
        //PlayerPrefs.SetFloat("Health", health);
    }

    public void DamagePlayer()
    {
        damage = Random.Range(minDamage, maxDamage);
        player.TakeDamage(damage);
    }

    public void TakeDamage(float damageToTake)
    {
        if (damageToTake == 69)
        {
            damageToTake = Random.Range(minDamage, maxDamage);
        }
        health -= damageToTake;
        //PlayerPrefs.SetFloat("Health", health);
    }
}
