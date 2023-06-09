using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public Color ColorEstado;

    protected MachineState maquinaDeEstados;
    protected  DriverNavMesh controladorNavMesh;
    protected DriverVision controladorVision;

    protected float SpeedNavMesh;

    protected virtual void Awake()
    {
        maquinaDeEstados = GetComponent<MachineState>();
        controladorNavMesh = GetComponent<DriverNavMesh>();
        controladorVision = GetComponent<DriverVision>();
        
    }


}
