using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerControl : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float moveSpeed;

    private Animator animator;

    CharacterController controller;
    private float gravity = 9.81f;
    private Vector2 moveInput;
    private float betweenIdleWalk = 0;

    public float interactionDistance = 2f;
    public KeyCode interactKey = KeyCode.E;

    private CarController carInteractionController;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (carInteractionController != null && Input.GetKeyDown(interactKey))
        {
            carInteractionController.OnPlayerInteraction();
        }

    }

    private void Move()
    {
        Vector3 moveVelocity = (cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y + Vector3.down * gravity) * Time.deltaTime * moveSpeed;
        controller.Move(moveVelocity);
        moveVelocity.y = 0;
        Rotate(moveVelocity);
        animator.SetFloat("Speed", moveVelocity.magnitude);
        if (moveVelocity.magnitude < 0.01)
        {
            betweenIdleWalk += 1;
        }
        else {
            betweenIdleWalk = 0;
        }
        if (betweenIdleWalk > 2)
        {
            animator.SetTrigger("Stop");
        }
      
    }

    private void Rotate(Vector3 target)
    {
        transform.LookAt(transform.position + target);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Check if the player is within range of a car
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            // If the object is a car and the player is not already interacting with it, set the carInteractionController
            CarController newController = hit.collider.GetComponent<CarController>();
            if (newController != null && newController != carInteractionController)
            {
                carInteractionController = newController;
            }
        }
        else
        {
            // If the player is no longer within range of the car, reset the carInteractionController
            carInteractionController = null;
        }
    }


}
