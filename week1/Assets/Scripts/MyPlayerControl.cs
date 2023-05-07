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

    public Animator animator;

    CharacterController controller;
    private float gravity = 9.81f;
    private Vector2 moveInput;
    private float betweenIdleWalk = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
}
