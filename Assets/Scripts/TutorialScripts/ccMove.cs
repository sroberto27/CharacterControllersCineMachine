using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccMove : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 3;
    public float rotateSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        controller.Move(transform.forward * playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime);        
    }
}
