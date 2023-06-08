using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateFleeChicken : StateChicken
{
    public CapsuleCollider collider;
    public float detectionRange = 5f; // Rango de detecci�n
    private bool isRunning = false; // Indicador de si est� huyendo

    protected override void Awake()
    {

        base.Awake();
    }

    void OnEnable()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        collider.radius = 1f;
        SpeedNavMesh = 3f;
        Anim.SetBool("Walk", false);
        Anim.SetBool("Run", true);
        controladorNavMesh.navMeshAgent.speed = SpeedNavMesh;

    }
    void Update()
    {
        if (IsPlayerDetected())
        {
            RunAway();
        }
        if (controladorNavMesh.HemosLlegado() && !IsPlayerDetected())
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.statePatrol);
        }
        if (maquinaDeEstados.inRange)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.stateDissapear);
        }
    }
    private void RunAway()
    {
        isRunning = true;

        // Calcular la direcci�n desde el jugador hacia la IA
        Vector3 direction = transform.position - controladorNavMesh.playerTarget.position;

        // Calcular la posici�n de alejamiento multiplicando la direcci�n por una distancia
        Vector3 runPosition = transform.position + direction.normalized * detectionRange;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(runPosition, out hit, detectionRange, NavMesh.AllAreas))
        {
            // Moverse hacia la nueva posici�n de alejamiento
            controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(hit.position);
        }
    }
    private bool IsPlayerDetected()
    {
        // Verificar si el jugador est� dentro del rango de detecci�n
        return Vector3.Distance(transform.position, controladorNavMesh.playerTarget.position) < detectionRange;
    }
}
