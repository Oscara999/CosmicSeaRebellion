using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshChicken : MonoBehaviour
{
    public Transform playerTarget;
    [SerializeField]
    public NavMeshAgent navMeshAgent;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void ActualizarPuntoDestinoNavMeshAgent(Vector3 puntoDestino)
    {
        navMeshAgent.destination = puntoDestino;
        navMeshAgent.Resume();
    }
    public bool HemosLlegado()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }
    public void DetenerNavMeshAgent()
    {
        navMeshAgent.Stop();
    }
    public void ActualizarPuntoDestinoNavMeshAgent()
    {
        ActualizarPuntoDestinoNavMeshAgent(playerTarget.position);
    }

}
