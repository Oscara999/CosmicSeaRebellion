using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public int startingHealt = 100;
    public int currentHealt;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool IsSinkin;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealt = startingHealt;
    }
    // Start is called before the first frame update
    void Start()
    {

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

        currentHealt -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealt <= 0)
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