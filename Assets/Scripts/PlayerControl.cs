using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public enum MoveType
    {
        CameraForward,
        Stop
    };

    [SerializeField] public MoveType movetype = MoveType.CameraForward;
    [SerializeField] private Camera cam;

    public float speed = 0f;
    public float jumpSpeed = 0f;
    public float gravityFactor = 1f;

    private CharacterController character;
    private Animator animator;

    private bool jumpInput;
    private bool jumping;
    private bool previouslyGrounded;
    private CollisionFlags collisionFlags;
    private Vector3 move = Vector3.zero;

    void Start () {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        jumpInput = Input.GetButtonDown("Jump");

        if (!previouslyGrounded && character.isGrounded)
        {
            move.y = 0f;
            jumping = false;
        }

        previouslyGrounded = character.isGrounded;


    }


    void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.zero;
        if (movetype == MoveType.CameraForward)
        {
            dir = cam.transform.forward * v + cam.transform.right * h;
        }
        else if(movetype == MoveType.Stop)
        {
            dir = Vector3.zero;
        }

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, character.radius/3f, Vector3.down, out hitInfo,
                           character.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        dir = Vector3.ProjectOnPlane(dir, hitInfo.normal);
        dir.y = 0;

        if (dir.magnitude > 1)
            dir = dir.normalized;

        move.x = dir.x * speed;
        move.z = dir.z * speed;

        if (character.isGrounded&&!jumping)
        {
            move.y = Physics.gravity.y;

            if (dir.magnitude != 0)
                transform.rotation = Quaternion.LookRotation(dir);

            if (jumpInput && !jumping)
            {
                move.y = jumpSpeed;
                jumping = true;
            }
        }
        else
        {
            move += Physics.gravity * gravityFactor * Time.fixedDeltaTime;
        }

        collisionFlags = character.Move(move * Time.fixedDeltaTime);
        Debug.Log(dir.magnitude);

        animator.SetFloat("Speed", dir.magnitude*speed/20f);
        animator.SetBool("Jumping", jumping);
        animator.SetBool("isGround", character.isGrounded);

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(character.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

}
