using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{ 

    public Transform[] WayPoints;

    public Animator Anim;
    private int siguienteWayPoint;

    protected override void Awake()
    {
        Anim = GetComponent<Animator>();
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        // Ve al jugador?
        RaycastHit hit;
        if (controladorVision.PuedeVerAlJugador(out hit))
        {
            controladorNavMesh.perseguirObjectivo = hit.transform;
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
            return;
        }

        if (controladorNavMesh.HemosLlegado())
        {
            siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
            ActualizarWayPointDestino();
        }
    }

    void OnEnable()
    {
        Anim.SetBool("Atack", false);
        Anim.SetBool("Walk", true);
        Anim.SetBool("Alert", false);
        Anim.SetBool("Looking", false);
        Anim.SetBool("Ataque", false);
        Anim.SetBool("Muerte", false);
        Anim.SetBool("RecibiendoDaño", false);
        Anim.SetBool("Huyendo", false);

        ActualizarWayPointDestino();
    }

    void ActualizarWayPointDestino()
    {
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enabled ) // Si el player entra el collider y no esta en modo alerta
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
        }
    }
}
