using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(NavMeshAgent))]
public class MotionController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private float moveBuffer = 2.0f;

    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent meshAgent;

    //Animator Variables
    private readonly int MovementXHash = Animator.StringToHash("MovementX");
    private readonly int MovementYHash = Animator.StringToHash("MovementY");
    private readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    //Movement
    public bool isRunning;
    public bool isJumping;
    private Vector3 nextPositionCheck;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 InputVector;

    //Get all required components on initiliazed
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        meshAgent = GetComponent<NavMeshAgent>();
    }
    public void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();

        animator.SetFloat(MovementXHash, InputVector.x);
        animator.SetFloat(MovementYHash, InputVector.y);
    }

    public void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
        animator.SetBool(IsRunningHash, value.isPressed);
    }

    //public void OnJump(InputValue value)
    //{
    //    //REMOVE WHEN FIXED
    //    return;
    //    //BROKEN
    //    if (isJumping) return;

    //    isJumping = value.isPressed;
    //    animator.SetBool(IsJumpingHash, value.isPressed);
    //    rb.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
    //}

    // Update is called once per frame
    void Update()
    {
        //no direction change in air
        if (isJumping) return;

        moveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        //check if path is valid
        nextPositionCheck = transform.position + moveDirection * moveBuffer;
        if (NavMesh.SamplePosition(nextPositionCheck, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            transform.position += movementDirection;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground") || !isJumping) return;

        isJumping = false;
        animator.SetBool(IsJumpingHash, false);
    }
}
