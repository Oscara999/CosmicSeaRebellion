using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public int startingHealth = 100; //VidaInicial
    public int CurrentHealth; //Nivel De energia 
    public Slider HealthSlider; //SliderdeVida
    public Image damageImagen; //PantallaRoja
    public AudioClip deathClip; //AudiodeMuerte
    public float flashSpeed = 5f; //VelocidadDeColor
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //ColorDeLaPantalla

    public Animator Anim;
    AudioSource DeathAudio;
    MachineState maquinaEstados;

    bool isDead;
    bool damaged;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
        CurrentHealth = startingHealth;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImagen.color = flashColour;
        }
        else
        {
            damageImagen.color = Color.Lerp(damageImagen.color, Color.clear, flashSpeed);

        }
        damaged = false;
    }
    public void TakeDamage(int amount)
    {
        damaged = true;

        CurrentHealth -= amount;

        HealthSlider.value = CurrentHealth;

        DeathAudio.Play();

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()

    {
        isDead = true;
        Anim.SetTrigger("Muerte");
        DeathAudio.clip = deathClip;
        DeathAudio.Play();

        //Stop
    }
}
