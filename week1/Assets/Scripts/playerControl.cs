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
    
    Vector3 movementDirection;
    Rigidbody rb;

    public float rotationSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Debug.Log("Orientation: " + orientation);

    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
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
        myInput();
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        Debug.DrawRay(transform.position, movementDirection, Color.green);

        if (movementDirection.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
        rb.AddForce(movementDirection.normalized * speed * 10f, ForceMode.Force);

    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            limitVel = new Vector3(-limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void movementAnimation()
    {
        bool forward = Input.GetAxisRaw("Vertical") > 0;
        bool backward = Input.GetAxisRaw("Vertical") < 0;
        bool left = Input.GetAxisRaw("Horizontal") < 0;
        bool right = Input.GetAxisRaw("Horizontal") > 0;
        bool moving = forward || backward || left || right;

        anim.SetBool("isWalking", moving);

    }
}
