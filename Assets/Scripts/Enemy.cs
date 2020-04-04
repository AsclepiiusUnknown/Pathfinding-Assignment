using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float health; //enemy health
    [Range(0, 100)]
    public int maxHealth = 100; //the max health enemy can have
    [Range(0, 50)]
    public float minDamage, maxDamage; //floats used to determine random damage ammounts within a range
    private float damage; //the damage to inflict
    public Player player; //the player script

    private void Start()
    {
        health = maxHealth; //set the health to the max
    }

    public void DamagePlayer()
    {
        damage = Random.Range(minDamage, maxDamage); // set the damage to a random ammount
        player.TakeDamage(damage); //inflict damage upon the player
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;//take damage inflicted to enemy
    }
}
