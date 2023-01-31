using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//simple controller to get you started
public class SimpleController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float playerSpeed = 7.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        
    }

    void Update()
    {        
        //grounded check 
        if(controller.isGrounded && playerVelocity.y < 0){
            playerVelocity.y = 0f;
        }
        if(!groundedPlayer && !controller.isGrounded){
            animator.SetFloat ("Vertical", playerVelocity.y);
        }else if(!groundedPlayer && controller.isGrounded){
            animator.SetFloat ("Vertical", 0);
            groundedPlayer = true;
        }        

        //movement
        Vector3 cameraRight = Vector3.ProjectOnPlane (Camera.main.transform.right, Vector3.up).normalized;
        Vector3 cameraForward = Vector3.ProjectOnPlane (Camera.main.transform.forward, Vector3.up).normalized;        
        Vector3 move = Input.GetAxis ("Horizontal") * cameraRight + Input.GetAxis ("Vertical") * cameraForward;
        move.y = 0;     
        controller.Move(move * Time.deltaTime * playerSpeed);    
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetFloat ("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
        }        

        //jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            groundedPlayer = false;
            animator.SetFloat ("Vertical", playerVelocity.y);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(Input.GetKey (KeyCode.LeftShift) && groundedPlayer){
            animator.SetBool ("Sliding", true);
        }else{
            animator.SetBool ("Sliding", false);
        }
    }
}
