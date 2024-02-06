using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float playerSpeed = 3;    
    [SerializeField]
    private float jumpForce = 200.0f;
    [SerializeField]
    private float gravity = -9.81f;
    private Vector3 movement;
    private Vector3 velocity;
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        cameraPosition = Camera.main.transform.position;
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
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            velocity.y = jumpForce * -gravity * Time.deltaTime; ;
        } else {
            velocity.y += gravity * Time.deltaTime;
        }

        //set jump/falling animation if needed
        if (grounded) {
            animator.SetFloat("Vertical", 0);
        } else {
            animator.SetFloat("Vertical", velocity.y);
        }

        //slide
        if (Input.GetKey(KeyCode.LeftShift) && grounded) {
            animator.SetBool("Sliding", true);
            Camera.main.transform.localPosition = cameraPosition - new Vector3(0, 1.0f, 0);
        }
        else {
            animator.SetBool("Sliding", false);
            Camera.main.transform.localPosition = cameraPosition;
        }

        //set forward animation if movement
        if (movement != Vector3.zero) {
            animator.SetFloat("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
        }

        //move character
        controller.Move(playerSpeed * Time.deltaTime * movement);

        //add gravity/jump
        controller.Move(velocity * Time.deltaTime);
    }
}
