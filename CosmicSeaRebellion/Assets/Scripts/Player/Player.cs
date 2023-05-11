using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Player Components")]
    public PlayerStats PlayerStats;
    public LocomotionCharacter walkerController;
    public CharacterInput input;
    public CinemachineControllerCamera cinmachineCamera;
    public AnimationPlayerController animationPlayer;
    public AttackPlayer attack;
    public PlayerInventory inventoryPlayer;
    public InventoryObject inventoryObject;
    public GameObject panelButtons;

    [Header("Player Flags")]
    public bool IsActivate;
    public string playerName;

    void Start()
    {
       // walkerController = GetComponent<LocomotionCharacter>();
       // input = GetComponent<CharacterInput>();
       // cinmachineCamera = GetComponent<CinemachineControllerCamera>();
       // animationPlayer = GetComponentInChildren<AnimationPlayerController>();
       // StartCoroutine(LoadDataPlayer());
    }

    public void SavedDataPlayer()
    {
        inventoryObject.Save();
    }

    public IEnumerator LoadDataPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (!ManagerBaseData.Instance.Load(inventoryObject.savePath, inventoryPlayer))
        {
            SavedDataPlayer();
        }
    }

    public bool State()
    {
        return IsActivate = !IsActivate;
    }

    public void DeathPlayer()
    {
        animationPlayer.PlayTargetAnimationRootMotion("Death", true);
        animationPlayer.SetAnimationBool("IsInteracting", true);
        State();
        // sonidos
    }

    public void DamagePlayer()
    {
        animationPlayer.SetAnimationBool("IsInteracting", true);
        animationPlayer.SetAnimationTrigger("Damage");
        // sonidos
    }

    void FixedUpdate()
    {
        if (IsActivate)
        {
            walkerController.FixedUpdateController();
            cinmachineCamera.FixedUpdateCameraController();
            panelButtons.SetActive(true);
        }
        else
        {
            PlayerDisable();
        }
    }

    void Update()
    {
        if (IsActivate)
        {
            animationPlayer.SetAnimations(walkerController.GetVelocity());
            walkerController.CallInput();
            input.GetInput();
        }
    }
    
    void PlayerDisable()
    {
        walkerController.mover.SetVelocity(Vector3.zero);
        input.Desactivate();
        panelButtons.SetActive(false);
    }
}
