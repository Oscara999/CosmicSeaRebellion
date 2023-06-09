using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAType : MonoBehaviour
{
    public enum TypeIA {Soldado, Ladron, Bully, Perro, PatrullaGentil, PatrullaAgresiva}
    public enum Weapon {Espada,Cuchillo,Puños}

    public TypeIA Tipo;
    public Weapon Arma;

    public int N_Tipo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StarSimulation()
    {
        switch (Tipo)
        {
            case TypeIA.Soldado: N_Tipo = 0;
                break;
            case TypeIA.Ladron:N_Tipo = 1;
                break;
            case TypeIA.Bully: N_Tipo = 2;
                break;
            case TypeIA.Perro:N_Tipo = 3;
                break;
            case TypeIA.PatrullaGentil:N_Tipo = 4;
                break;
            case TypeIA.PatrullaAgresiva:N_Tipo = 5;
                break;
            default:
                break;
        }
    }
    public void TipoSeleccion()
    {
        if (Tipo.Equals(TypeIA.Soldado))
        {
            SoySoldado();
        }
        if (Tipo.Equals(TypeIA.Ladron))
        {
            SoyLadron();
        }
        if (Tipo.Equals(TypeIA.Bully))
        {
            SoyBully();
        }
        if (Tipo.Equals(TypeIA.Perro))
        {
            SoyPerro();
        }
        if (Tipo.Equals(TypeIA.PatrullaGentil))
        {
            SoyGentil();
        }
        if (Tipo.Equals(TypeIA.PatrullaAgresiva))
        {
            SoyAgresivo();
        }
    }
    public void SoySoldado()
    {

    }
    public void SoyLadron()
    {

    }
    public void SoyBully()
    {

    }
    public void SoyPerro()
    {

    }
    public void SoyGentil()
    {

    }
    public void SoyAgresivo()
    {

    }
}
