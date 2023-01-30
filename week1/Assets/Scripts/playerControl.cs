using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class playerControl : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Transform orientation;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    [Header("Animation")]
    public Animator anim;
    
    bool movingState;
    Vector3 movementDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        movingState = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down);
        myInput();
        speedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        movementAnimation();
        movement();
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    private void movement()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(movementDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            flatVel = new Vector3(-limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void movementAnimation()
    {
        bool isWalk = anim.GetBool("isWalking");
        bool forward = Input.GetKey(KeyCode.W);
        bool backward = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        if (forward || backward || left || right)
        {
            movingState = true;
        }
        else
        {
            movingState = false;
        }
        if (!isWalk && movingState)
        {
            anim.SetBool("isWalking", true);
        }
        else if (isWalk && !movingState)
        {
            anim.SetBool("isWalking", false);
        }

    }
}
