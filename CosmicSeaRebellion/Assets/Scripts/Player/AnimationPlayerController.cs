using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the character's animation by passing velocity values and other information ('isGrounded') to an animator component;
public class AnimationPlayerController : AnimatorController {

	Transform animatorTransform;
	Transform tr;
	Vector3 oldMovementVelocity = Vector3.zero;
    float smoothingFactor = 0.8f;
    public float speed;

    //Setup;
    void Start()
    {
		anim = GetComponent<Animator>();
		animatorTransform = anim.transform;
		tr = transform;
    }

    public void SetAnimations(Vector3 _velocity)
    {
        //Split up velocity;
        Vector3 _horizontalVelocity = VectorMath.RemoveDotVector(_velocity, tr.up);
        Vector3 _verticalVelocity = _velocity - _horizontalVelocity;

        //Smooth horizontal velocity for fluid animation;
        _horizontalVelocity = Vector3.Lerp(oldMovementVelocity, _horizontalVelocity, smoothingFactor);
        oldMovementVelocity = _horizontalVelocity;

        //Pass values to animator;
        SetAnimationFloat("VerticalSpeed", _verticalVelocity.magnitude * VectorMath.GetDotProduct(_verticalVelocity.normalized, tr.up));
        SetAnimationFloat("HorizontalSpeed", _horizontalVelocity.magnitude);

        SetAnimationBool("IsGrounded", Player.Instance.walkerController.IsGrounded());
        SetAnimationBool("IsCrounched", Player.Instance.walkerController.IsCrounched());
        SetAnimationBool("IsCombat", Player.Instance.walkerController.IsCombat());
        //anim.SetBool("IsInteracting", SimpleWalkerController.Instance.IsInteracting());
        //anim.SetBool("HaveArrow", SimpleWalkerController.Instance.);
        //anim.SetBool("HaveSword", SimpleWalkerController.Instance.);
    }

    public void EnableCombo()
    {
        SetAnimationBool("CanDoCombo", !anim.GetBool("CanDoCombo"));
        Debug.Log(anim.GetBool("CanDoCombo"));
    }

    public void OnAnimatorMove()
    {
        if (!Player.Instance.walkerController.IsInteracting())
            return;

        float delta = Time.deltaTime;
        Vector3 _velocity;
        _velocity = Vector3.zero;
        Player.Instance.walkerController.mover.rig.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        _velocity = (deltaPosition / delta) * speed;
        _velocity += tr.up * Player.Instance.walkerController.currentVerticalSpeed;
        Player.Instance.walkerController.mover.SetVelocity(_velocity);
    }
}
