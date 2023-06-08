using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineState : MonoBehaviour
{
    public State EstadoPatrulla;
    public State EstadoPersecucion;
    public State EstadoAlerta;
    public State EstadoBuscando;
    public State EstadoAtaque;
    public State EstadoHuyendo;
    public State EstadoDesenfundado;
    public State EstadoGuardarArma;
    public State EstateRecibirDaño;
    public State EstadoMuerte;
    
    public State EstadoInicial;

    public bool PuedoAtacar;
    public bool SigoAtacando;
    public bool ModoAlerta;
    public bool Daño;

    public bool TengoArma;

    public MeshRenderer MeshRendererIndicador;

    private State estadoActual;

    // Start is called before the first frame update
    void Start()
    {
        ActivarEstado(EstadoInicial);
    }

    public void ActivarEstado(State nuevoEstado)
    {
        if (estadoActual != null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;

        MeshRendererIndicador.material.color = estadoActual.ColorEstado;
    }


}
