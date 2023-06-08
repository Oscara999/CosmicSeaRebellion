using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChicken : MonoBehaviour
{
    public Animator Anim;
    public Color ColorEstado;

    protected MachineStateChicken maquinaDeEstados;
    protected NavMeshChicken controladorNavMesh;
    protected DriverVisionChicken controladorVision;

    public float SpeedNavMesh;

    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        maquinaDeEstados = GetComponent<MachineStateChicken>();
        controladorNavMesh = GetComponent<NavMeshChicken>();
        controladorVision = GetComponent<DriverVisionChicken>();
    }
}
