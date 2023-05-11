using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A very simplified controller script;
//This script is an example of a very simple walker controller that covers only the basics of character movement;
public class LocomotionCharacter : MonoBehaviour
{
    [Header("Movement Components")]
    public Mover mover;
    
    SmoothRotation rotation;
    Transform cameraTransform;
    Transform tr;
    Vector3 lastVelocity = Vector3.zero;
    Vector3 _direction = Vector3.zero;

    [Header("Movement Stats")]
    [SerializeField] bool isGrounded;
    [SerializeField] bool isCrouched;
    [SerializeField] bool isCombat;
    [SerializeField] bool isInteracting;
    [SerializeField] bool canDoCombo;
    [SerializeField] bool isCallJump;
    [SerializeField] bool isCallCrouched;
    [SerializeField] bool isCallRoll;
    [SerializeField] bool isCallLightAttack;
    [SerializeField] bool isCallHeavyAtack;

    [Header("Movement Settings")]
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float normalSpeed = 4.5f;
    [SerializeField] float crunchSpeed = 1.2f;
    [SerializeField] float normalTurnSpeed = 20;
    [SerializeField] float crunchTurnSpeed = 10;
    float movementSpeed;
    float gravity = 30;
    public float currentVerticalSpeed = 0f;

    public bool comboFlag;

    // Use this for initialization
    void Awake()
    {
        tr = transform;
        rotation = GetComponentInChildren<SmoothRotation>();
        cameraTransform = Camera.main.transform;
    }

    public void CallInput()
    {
        if(Player.Instance.input != null)
        {
            if (isGrounded && !isCrouched && !isInteracting && Player.Instance.input.IsJumpKeyPressed())
            {
                isCallJump = true;
            }

            if (isGrounded && !isInteracting && Player.Instance.input.IsCrouchKeyPressed())
            {
                isCallCrouched = true;
            }

            if (isGrounded && !isCrouched && !isInteracting && Player.Instance.input.IsRollKeyPressed())
            {
                isCallRoll = true;
            }

            if (isGrounded && !isCrouched && Player.Instance.input.IsAttackLigthKeyPressed())
            {
                isCallLightAttack = true;
            }

            if (isGrounded && !isCrouched && Player.Instance.input.IsAttackHeavyKeyPressed())
            {
                isCallHeavyAtack = true;
            }

            if (Player.Instance.input.TestInput())
            {
                Player.Instance.PlayerStats.health.TakeDamage(25);
            }

            isInteracting = Player.Instance.animationPlayer.anim.GetBool("IsInteracting");
            canDoCombo = Player.Instance.animationPlayer.anim.GetBool("CanDoCombo");

            CalculateMovementInput();

            HandleAttack();
            HandleRoll();
            HandleCrouching();

            GearShift();
        }
    }

    public void FixedUpdateController()
    {
        //Run initial mover ground check;
        mover.CheckForGround();

        //Handle gravity;
        HandleGravity();

        //Check whether the character is grounded and store result;
        isGrounded = mover.IsGrounded();


        if (!isInteracting)
        {
            Vector3 _velocity = Vector3.zero;

            //Add player movement to velocity;
            _velocity += CalculateMovementDirection() * movementSpeed;

            HandleJumping();
            
            //Add vertical velocity;
            _velocity += tr.up * currentVerticalSpeed;

            //Save current velocity for next frame;
            lastVelocity = _velocity;

            mover.SetExtendSensorRange(isGrounded);
            mover.SetVelocity(_velocity);
        }
    }

    public void HandleGravity()
    {
        if (!isGrounded)
        {
            currentVerticalSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            if (currentVerticalSpeed <= 0f)
                currentVerticalSpeed = 0f;
        }
    }

    void GearShift()
    {
        if (!isCrouched)
        {
            movementSpeed = normalSpeed;
            rotation.smoothSpeed = normalTurnSpeed;
        }
        else
        {
            movementSpeed = crunchSpeed;
            rotation.smoothSpeed = crunchTurnSpeed;
        }
    }

    //Handle Jump
    void HandleJumping()
    {
        if (isCallJump)
        {
            currentVerticalSpeed = jumpSpeed;
            isGrounded = false;
            isCallJump = false;
        }
    }

    //Handle Crouch
    void HandleCrouching()
    {
        if (isCallCrouched)
        {
            isCrouched = !isCrouched;
            //mover.ChangeCollisionDimensions(isCrouched);
            isCallCrouched = false;
        }
    }

    //Handle Attack
    void HandleAttack()
    {
        if (isCallLightAttack)
        {

            if (canDoCombo)
            {
                Player.Instance.animationPlayer.SetAnimationBool("IsInteracting", true);
                comboFlag = true;
                Player.Instance.attack.HandleWeaponCombo(Player.Instance.inventoryPlayer.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if (isInteracting)
                    return;

                if (canDoCombo)
                    return;
                Player.Instance.animationPlayer.SetAnimationBool("IsInteracting", true);

                Player.Instance.attack.HandleLightAttack(Player.Instance.inventoryPlayer.rightWeapon);
            }

            isCallLightAttack = false;
        }

        if (isCallHeavyAtack)
        {
            if (isInteracting)
                return;

            if (canDoCombo)
                return;

            Player.Instance.animationPlayer.SetAnimationBool("IsInteracting", true);
            Player.Instance.attack.HandleHeavytAttack(Player.Instance.inventoryPlayer.rightWeapon);
            isCallHeavyAtack = false;
        }
    }

    //Handle Roll
    void HandleRoll()
    {
        if (isCallRoll)
        {
            Player.Instance.animationPlayer.SetAnimationBool("IsInteracting", true);
            Player.Instance.animationPlayer.PlayTargetAnimationRootMotion("IsRolling", true);
            mover.ChangeCollisionDimensions(true);
            isCallRoll = false;
        }
    }

    //Handle Move
    void CalculateMovementInput()
    {
        _direction = Vector3.zero;

        //If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
        if (cameraTransform == null)
        {
            _direction += tr.right * Player.Instance.input.GetHorizontalMovementInput();
            _direction += tr.forward * Player.Instance.input.GetVerticalMovementInput();
        }
        else
        {
            //If a camera transform has been assigned, use the assigned transform's axes for movement direction;
            //Project movement direction so movement stays parallel to the ground;
            _direction += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized * Player.Instance.input.GetHorizontalMovementInput();
            _direction += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized * Player.Instance.input.GetVerticalMovementInput();
        }

        //If necessary, clamp movement vector to magnitude of 1f;
        if (_direction.magnitude > 1f)
            _direction.Normalize();
    }


    Vector3 CalculateMovementDirection()
    {
        //If no character input script is attached to this object, return no input;
		if(Player.Instance.input == null)
			return Vector3.zero;

        return _direction;
    }

    //Return the current velocity of the character;
    public Vector3 GetVelocity()
    {
        return lastVelocity.normalized;
    }

    //Return only the current movement velocity (without any vertical velocity);
    public Vector3 GetMovementVelocity()
    {
        return lastVelocity;
    }

    //Return whether the character is currently grounded;
    public bool IsGrounded()
    {
        return isGrounded;
    }

    //Return whether the character is Crounched;
    public bool IsCrounched()
    {
        return isCrouched;
    }

    //Return whether the character is mode Combat;
    public bool IsCombat()
    {
        return isCombat;
    }

    //Return whether the character is interacting;
    public bool IsInteracting()
    {
        return isInteracting;
    }
}


