using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWavingAI : StateAI
{
    public string misionIndividual;
    public float duracionSaludo = 4f;

    public Animator Anim;

    private float tiempoSaludando;

    public MisionManager misionManager;

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
        misionManager = GameObject.FindGameObjectWithTag("Mision").GetComponent<MisionManager>();

    }
    void OnEnable()
    {
        Anim.SetBool("Walk", false);
        Anim.SetBool("Alert", false);
        Anim.SetBool("RecibiendoDaño", false);
        Anim.SetBool("Huyendo", false);
        Anim.SetBool("Saludando", true);
        controladorNavMesh.DetenerNavMeshAgent();
        SpeedNavMesh = 0;
        tiempoSaludando = 0f;

    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(controladorNavMesh.perseguirObjectivo.transform);
        SpeedNavMesh = 0;
        tiempoSaludando += Time.deltaTime;
        if (tiempoSaludando >= duracionSaludo)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
            return;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (maquinaDeEstados.SoyNpc)
        {
            if (other.gameObject.CompareTag("Player") && enabled) // Si el player entra el collider y no esta en modo alerta
            {
                misionManager.EnabledMisionPanel(misionIndividual);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            misionManager.currentPanel.SetActive(false);
        }
    }
}
