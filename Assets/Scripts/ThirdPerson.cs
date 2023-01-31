using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float playerSpeed = 3;
    [SerializeField]
    private float jumpForce = .75f;
    [SerializeField]
    private float gravity = -9.81f;
    private Vector3 movement;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

        //check if grounded
        bool grounded = controller.isGrounded;
        
        //get input
        Vector3 cameraRight = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;
        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        movement = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraForward;

        //check if jumping and add gravity to player velocity vector
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            velocity.y = jumpForce * -gravity;
        } else {
            velocity.y += gravity * Time.deltaTime;

        }

        //set jump/falling animation if needed
        if (grounded) {
            animator.SetFloat("Vertical", 0);
        } else {
            animator.SetFloat("Vertical", velocity.y);
        }

        //rotate character in movement direction if input detected
        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(movement);
            animator.SetFloat("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
        }

        //slide
        if (Input.GetKey(KeyCode.LeftShift) && grounded) {
            animator.SetBool("Sliding", true);
        } else {
            animator.SetBool("Sliding", false);
        }

        //move character
        controller.Move(playerSpeed * Time.deltaTime * movement);

        //add gravity/jump
        controller.Move(velocity * Time.deltaTime);
    }
}