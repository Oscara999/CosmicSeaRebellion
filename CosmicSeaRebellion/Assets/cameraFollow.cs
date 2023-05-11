using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.Instance.transform.position;
    }
}
