using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerVelocity = 10f;
    public CharacterController controller;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection =  transform.right * x + transform.forward * z;

        controller.Move(moveDirection * playerVelocity * Time.deltaTime);

        
    }

    
}
