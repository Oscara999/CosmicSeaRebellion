using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeAtaque : MonoBehaviour
{
    public MachineStateAI maquinaDeEstados;
    public GameObject Detector;
    // Start is called before the first frame update
    void Start()
    {
        Detector = GameObject.FindGameObjectWithTag("Enemy");
        maquinaDeEstados = Detector.GetComponent<MachineStateAI>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Si el player entra el collider y no esta en modo alerta
        {
            maquinaDeEstados.PuedoAtacar = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Si el player entra el collider y no esta en modo alerta
        {
            maquinaDeEstados.SigoAtacando = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") ) // Si el player entra el collider y no esta en modo alerta
        {
            maquinaDeEstados.PuedoAtacar = false;
            maquinaDeEstados.SigoAtacando = false;
        }
    }

}
