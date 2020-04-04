using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health & Damage")]
    [HideInInspector]
    public float health; //enemy health
    [Range(0, 100)]
    public int maxHealth = 100; //the max health enemy can have
    [Range(0,50)]
    public float minDamage, maxDamage; //floats used to determine random damage ammounts within a range
    private float damage; //the damage to inflict
    public Enemy enemy; //the enemy script

    [Header("Movement")]
    public float speed; // the speed at which to move
    public Rigidbody rb; //our rigidbody component

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //set the rigidbody to the one on the player

        health = maxHealth; //set the health to the max
    }

    public void DamageEnemy()
    {
        damage = Random.Range(minDamage, maxDamage); // set the damage to a random ammount
        enemy.TakeDamage(damage); //inflict damage upon the enemy
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;//take damage inflicted to player
    }

    void FixedUpdate()
    {
        float movH = Input.GetAxis("Horizontal");//set values for the movement on horizontal axis
        float movV = Input.GetAxis("Vertical");//set values for the movement on vertical axis

        Vector3 movement = new Vector3(movH, 0, movV);//set the vector three to equal our new input

        rb.velocity = movement * speed; //move ot the new position at the speed we set
    }
}
