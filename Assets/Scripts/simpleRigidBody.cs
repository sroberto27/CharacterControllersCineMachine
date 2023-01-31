using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleRigidBody : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rb;
    private float playerSpeed = 3;
    private Vector3 movement;
    Vector3 m_EulerAngleVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate() {
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rb.MovePosition(transform.position + m_Input * Time.deltaTime * playerSpeed);

        if(m_Input != Vector3.zero) {
            //rb.gameObject.transform.rotation = Quaternion.SetLookRotation(m_Input);
        }
        
    }
}
