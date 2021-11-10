using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] Inputs inputs;
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float gravity;
    [SerializeField] float turnSmoothing;
    [SerializeField] float sprintMultiplier;
    [SerializeField] float timeToSprint;
    public Vector3 desiredMoveDirection;
    Vector3 velocityY;
    private Vector3 forward;
    private Vector3 right;
    bool canMove = true;
    float canMoveTimer;

    [Header("Grounding")]
    [SerializeField] bool isGrounded;
    [SerializeField] float yOffset;
    [SerializeField] float groundcheckRadius;
    [SerializeField] LayerMask floorMask;
    bool isGroundedLastFrame;

    [Header("Jump Settings")]
    [SerializeField] private float jumpDelay = 0.7f;
    [SerializeField] bool CanDoubleJump;
    [SerializeField] private float jumpLandDelay = 0.7f;
    private float secondJumpMultiplier;

    [Header("Animation")]
    [SerializeField] Animator animator;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check sphere at characters legs to see if we're on the floor
        isGrounded = Physics.CheckSphere(controller.transform.position - new Vector3(0, yOffset, 0), groundcheckRadius, floorMask);
        animator.SetBool("isGround", isGrounded);

        if (isGroundedLastFrame == false && isGrounded == true)
        {
            StartCoroutine(LandRoutine()); 
        }

        isGroundedLastFrame = isGrounded;
        if (!canMove) return;
        Gravity();
        Movement();


    }

    private void Movement()
    {

        //camera front and right  vectors
        forward = cam.transform.forward;
        right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        if (!Input.GetKey(KeyCode.LeftControl))
        {
            //build the direction vector
            desiredMoveDirection = (forward * inputs.verticalRaw + right * inputs.horizontalRaw).normalized * moveSpeed;
            t = 0;

            animator.SetFloat("MoveSpeed", desiredMoveDirection.normalized.magnitude);

            //if there is some input we turn towards the vector
            if (inputs.horizontalRaw != 0 || inputs.verticalRaw != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), turnSmoothing * Time.deltaTime);
            }
        }
        else
        //sprint
        {
            //build the direction vector
            desiredMoveDirection = (forward * inputs.vertical + right * inputs.horizontalRaw).normalized * moveSpeed * Mathf.Lerp(1, sprintMultiplier, t);
            //if there is some input we turn towards the vector
            t += Time.deltaTime / timeToSprint;

            if (inputs.horizontalRaw != 0 || inputs.vertical != 0)
            {
                animator.SetFloat("MoveSpeed", Mathf.Lerp(1, 5, t));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), turnSmoothing * Time.deltaTime);
            }
        }

        Jump();

        //add y veclocity to our movement
        desiredMoveDirection += velocityY;

        controller.Move(desiredMoveDirection * Time.deltaTime);
    }


    private void Jump()
    {

        if (isGrounded)
        {
            //first jummp
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump");
                StartCoroutine(JumpRoutine());

            }
        }
        //second jump
        if (Input.GetKeyDown(KeyCode.Space) && CanDoubleJump && !isGrounded)
        {
            CanDoubleJump = false;
            velocityY = new Vector3(0, JumpForce * secondJumpMultiplier, 0);
        }
    }


    private void Gravity()
    {
        //if we're touching the ground, add a bit of gravity so we're always on the floor
        if (isGrounded && velocityY.y < 0)
        {
            velocityY = new Vector3(0, -2 * Time.deltaTime, 0);
            CanDoubleJump = false;


        }
        else
        {
            //if we're in the air, add some gravity every frame
            velocityY += new Vector3(0, gravity * Time.deltaTime, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, yOffset, 0), groundcheckRadius);
    }

    IEnumerator JumpRoutine()
    {
        animator.SetTrigger("Jump");
        canMove = false;
        yield return new WaitForSeconds(jumpDelay);
        canMove = true;
        velocityY = new Vector3(0, JumpForce, 0);
        CanDoubleJump = true;
    }
    IEnumerator LandRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(jumpLandDelay);
        canMove = true;
    }
}


