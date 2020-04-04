using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health & Damage")]
    [HideInInspector]
    public float health;
    [Range(0, 100)]
    public int maxHealth = 100;
    [Range(0,50)]
    public float minDamage, maxDamage;
    private float damage;
    public Enemy enemy;

    [Header("Movement")]
    public float speed;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        health = maxHealth;
    }

    public void DamageEnemy()
    {
        damage = Random.Range(minDamage, maxDamage);
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(float damageToTake)
    {
        if (damageToTake == 69)
        {
            damageToTake = Random.Range(minDamage, maxDamage);
        }
        health -= damageToTake;
    }

    void FixedUpdate()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movH, 0, movV);

        rb.velocity = movement * speed;
    }
}
