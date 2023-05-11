﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealt = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f; //Velocidad de hundimiento.
    public int scoreValue = 10; //Puntos
    public AudioClip deathClip; //Sonido de muerte

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles; 
    CapsuleCollider capsuleCollider;

    bool isDead; // esta muerto
    bool IsSinkin; // Enterrarse 


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealt;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsSinkin)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;
        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealth <= 0)
        {
            Death();
        }

    }
    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }
    public void StartSinking()
    {
        GetComponent<DriverNavMeshAI>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        IsSinkin = true;
        Destroy(gameObject, 2f);
    }
}