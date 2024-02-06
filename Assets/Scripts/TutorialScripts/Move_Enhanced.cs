using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move_Enhanced : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 3;
    private Vector3 movement = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    public float jumpForce = 500.0f;
    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //check if jumping and add gravity to the player
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = jumpForce * -gravity * Time.deltaTime;
        } else 
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //rotate character in movement direction if input detected
        if(movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        //move character
        controller.Move(playerSpeed * Time.deltaTime * movement);

        // add gravity or jump
        controller.Move(velocity * Time.deltaTime);
    }
}
