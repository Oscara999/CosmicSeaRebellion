using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDetector : MonoBehaviour
{
    public MachineStateChicken chickenBrain;
    private void Start()
    {
        chickenBrain = FindObjectOfType<MachineStateChicken>();
    }
    private void OnTriggerEnter(Collider other)
    {
        chickenBrain.inRange = true;
    }
}
