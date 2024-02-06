using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleRigidBody : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private CapsuleCollider _collider;
    [SerializeField]
    private Transform _groundchecker;
    [SerializeField]
    private float playerSpeed = 10f;
    [SerializeField]
    private float jumpPower = 3f;
    private bool grounded;
    private bool sliding;
    private bool jumping;
    private Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _collider = gameObject.GetComponent<CapsuleCollider>();
        _groundchecker = transform.Find("GroundChecker");
        Cursor.lockState = CursorLockMode.Locked;        
    }

    // Update is called once per frame
    void Update()
    {        
        //falling animation
        if (grounded) {
            _animator.SetFloat("Vertical", 0);
        } else {
            _animator.SetFloat("Vertical", _rigidbody.velocity.y);
        }       

        //sliding check        
        if (Input.GetKey(KeyCode.LeftShift))
        {           
            _collider.height = 0.8f;
            _collider.center = new Vector3(0, .4f, 0);
            _animator.SetBool("Sliding", true);
            sliding = true;
        }
        else
        {            
            _collider.height = 1.8f;
            _collider.center = new Vector3(0, .9f, 0);
            _animator.SetBool("Sliding", false);
            sliding = false;
        }

        //jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jumping = true;            
        }              

        //get input
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);            
            _animator.SetFloat("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
        }
    }

    void FixedUpdate() {
               
        grounded = Physics.Raycast(_groundchecker.position, Vector3.down, .20f);        
        if (!sliding) {            
            _rigidbody.AddForce(movement * playerSpeed, ForceMode.Force);
        }

        if (jumping)
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);            
            jumping = false;
        }
    }
}
