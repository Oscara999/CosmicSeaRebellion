using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrolChicken : StateChicken
{
    public Transform[] WayPoints;
    public CapsuleCollider collider;
    private int siguienteWayPoint;

    protected override void Awake()
    {

        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        // Ve al jugador?
        RaycastHit hit;
        if (controladorVision.PuedeVerAlJugador(out hit))
        {
            //controladorNavMesh.fleeTarget = hit.transform;
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.stateFlee);
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
        collider = gameObject.GetComponent<CapsuleCollider>();
        collider.radius = 5f;
        Anim.SetBool("Walk", true);
        Anim.SetBool("Run", false);
        ActualizarWayPointDestino();
        SpeedNavMesh = 0.5f;
        controladorNavMesh.navMeshAgent.speed = SpeedNavMesh;
    }

    void ActualizarWayPointDestino()
    {
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Si el player entra el collider y no esta en modo alerta
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.stateFlee);
        }
    }
}