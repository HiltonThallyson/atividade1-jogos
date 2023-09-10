using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour {
    
    private PlayerInputs playerInput;
    private InputAction onMovement;   
    private InputAction onGrab;   
    private InputAction onDrop;   
    private PlayerMovement playerMovement;
    private PlayerInteractions playerInteractions;

    private void Awake() {
        playerInput = new PlayerInputs();
        onMovement = playerInput.Play.Move;
        onGrab = playerInput.Play.Grab;
        onDrop = playerInput.Play.Drop;
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();

        onGrab.performed += ctx => playerInteractions.Grab();
        onDrop.performed += ctx => playerInteractions.Drop();
    }
    private void FixedUpdate() {
        playerMovement.Move(onMovement.ReadValue<Vector2>());
    }

    private void OnEnable() {
        onMovement.Enable();
        onGrab.Enable();
        onDrop.Enable();
    }

    private void OnDisable() {
        onMovement.Disable();
        onGrab.Disable();
        onDrop.Disable();
    }

    
}