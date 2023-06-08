using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDesappearChicken : StateChicken   
{
    public MisionManager misionManager;
    public string pets = "Pets";
   private void Awake()
    {
        misionManager = FindObjectOfType<MisionManager>();
        base.Awake();
    }
    private void Start()
    {

    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        controladorNavMesh.DetenerNavMeshAgent();
        Anim.SetBool("Run", false);
        Anim.SetBool("Eat", true);
        misionManager.UpdateMissionProgress(pets, 1);
        Invoke("Destroy", 10);
        Debug.Log("Mori");
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
