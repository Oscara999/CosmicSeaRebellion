using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeing : State
{
    public float duracionHuida = 4f;

    public Animator Anim;

    private float tiempoBuscando;

    public float TiempoHuyendo;
    

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
        TiempoHuyendo = 0;

    }

    void OnEnable()
    {
        Anim.SetBool("Atack", false);
        Anim.SetBool("Walk", false);
        Anim.SetBool("Alert", false);
        Anim.SetBool("Looking", false);
        Anim.SetBool("Ataque", false);
        Anim.SetBool("Muerte", false);
        Anim.SetBool("RecibiendoDaño", false);
        Anim.SetBool("Huyendo", true);


        //controladorNavMesh.DetenerNavMeshAgent();
        tiempoBuscando = 0f;

    }

    void Update()
    {
        if (controladorNavMesh.HemosLlegado())
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoBuscando);
            return;
        }

        TiempoHuyendo += Time.deltaTime;
        controladorNavMesh.HuyendoDelPeligro();


        if (TiempoHuyendo >= duracionHuida)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
            return;
        }


    }

}