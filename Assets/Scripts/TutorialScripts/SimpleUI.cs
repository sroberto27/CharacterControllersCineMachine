using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.U2D;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Text text;
    private float speed;
    // Start is called before the first frame update
    void Start() {        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        speed = controller.velocity.magnitude;
        text.text = "Speed: " + speed.ToString("F2") + "\nGrounded: " + controller.isGrounded;
    }

    private void FixedUpdate() {

    }
}