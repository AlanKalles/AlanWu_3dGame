using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    Animator anim;
    Transform trans;
    bool movingState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        movingState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        movement();
    }

    private void movement()
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
