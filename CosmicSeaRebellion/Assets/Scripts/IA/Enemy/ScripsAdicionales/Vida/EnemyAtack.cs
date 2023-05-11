using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.5f; //TiempoEntreAtaques
    public int attackDamage = 10; //Daño

    Animator anim;
    GameObject player;
    HealthPlayer playerHealth;
    bool playerInRange; //EnemigoEnRango
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthPlayer>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBeweenAttacks && playerInRange)
        {
            Attack();
        }
        if (playerHealth.CurrentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead"); //Llamar Al EstadoMuerte
        }     
    }
    void Attack()
    {
        timer = 0f;
        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }

    }
}
