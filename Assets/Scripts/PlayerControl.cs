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
    [SerializeField] public Camera cam;

    public float speed = 0f;
    public float jumpSpeed = 0f;
    public float gravityFactor = 1f;
    public float maxSpeed = 30f;
    private CharacterController character;
    private Animator animator;

    private bool jumpInput;
    public bool jumping;
    public bool previouslyGrounded;
    private CollisionFlags collisionFlags;
    private Vector3 move = Vector3.zero;


    public FMOD.Studio.EventInstance BGM;

    void Start () {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();

        BGM = FMODUnity.RuntimeManager.CreateInstance("event:/BGM");
        BGM.start();
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

        dir.y = 0;

        if (dir.magnitude > 1)
            dir = dir.normalized;

        move.x = dir.x * speed;
        move.z = dir.z * speed;

        if (character.isGrounded&&!jumping)
        {
            move.y = Physics.gravity.y;

            if (dir.magnitude != 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir),0.1f);

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



        BGM.setParameterValue("Speed", speed);
        animator.SetFloat("Speed", dir.magnitude*speed/maxSpeed);
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
