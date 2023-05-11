using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    public void PlayTargetAnimationRootMotion(string nameTrigger, bool rootMotion)
    {
        anim.applyRootMotion = rootMotion;
        anim.SetTrigger(nameTrigger);
    }

    public void SetAnimationLayer(float weight, string nameLayer)
    {
        anim.SetLayerWeight(anim.GetLayerIndex(nameLayer), weight);
    }

    public void SetAnimationTrigger(string nameTrigger)
    {
        anim.SetTrigger(nameTrigger);
    }

    public void SetAnimationBool(string nameBool, bool state)
    {
        anim.SetBool(nameBool,state);
    }

    public void SetAnimationFloat(string nameFloat, float value)
    {
        anim.SetFloat(nameFloat, value, 0.1f, Time.deltaTime);
    }
}
