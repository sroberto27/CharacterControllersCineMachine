using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.05f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.05f;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - .25f, transform.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + .25f, transform.eulerAngles.z);
        }
    }
}
