using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStateChicken : MonoBehaviour
{
    [Header("States)")]
    public StateChicken EstadoInicial;
    public StateChicken estadoActual;

    [Header("Chicken")]
    public StateChicken statePatrol; //Satanist Listo
    public StateChicken stateFlee;
    public StateChicken stateDissapear; //Satanist and Ghost


    [SerializeField]
    //private MeshRenderer MeshRendererIndicador;

    public bool inRange;

    void Start()
    {
        ActivarEstado(EstadoInicial);
    }

    public void ActivarEstado(StateChicken nuevoEstado)
    {
        if (estadoActual != null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;

        //MeshRendererIndicador.material.color = estadoActual.ColorEstado;
    }

}
