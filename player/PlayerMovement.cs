
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   

    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 10f;
    
    void Start() {
        controller = GetComponent<CharacterController>();
    }   


    public void Move(Vector2 input) {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.fixedDeltaTime);
    }
    
}
