using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverVisionChicken : MonoBehaviour
{
    [Header("Vision parameters")]

    public Transform Ojos;
    public float rangoVision = 20f;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0.75f, 0f);

    [SerializeField]
    private NavMeshChicken controladorNavMesh;

    void Awake()
    {
        controladorNavMesh = GetComponent<NavMeshChicken>();
    }

    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {
        Vector3 vectorDireccion;
        if (mirarHaciaElJugador)
        {
            vectorDireccion = (controladorNavMesh.playerTarget.position + offset) - Ojos.position;
        }
        else
        {
            vectorDireccion = Ojos.forward;
        }

        return Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
    }
}
