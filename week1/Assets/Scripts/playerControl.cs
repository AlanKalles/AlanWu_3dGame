using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class playerControl : MonoBehaviour
{
    Animator anim;
    bool movingState;

    Vector3 movementDirection;

    public float speed;
    public float rotationFactorPerFrame;

    CharacterController controller;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        movingState = false;
    }

    private void FixedUpdate()
    {
        movementAnimation();
        movement();

    }
    private void movement()
    {
        float lrV = Input.GetAxis("Horizontal");
        float fbV = Input.GetAxis("Vertical");
        movementDirection = new Vector3 (lrV, 0.0f, fbV);
        controller.Move(movementDirection * speed * Time.deltaTime);
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
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
