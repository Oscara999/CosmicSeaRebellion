using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public Transform PosicionAleatoria;
    // Start is called before the first frame update

    // Update is called once per frame

    private void OnEnable()
    {
        RandomNew();
    }
    public void RandomNew()
    {
        PosicionAleatoria.position = new Vector3 (Random.Range(-10, 10), 0 , Random.Range(-10, 10));
    }

}
