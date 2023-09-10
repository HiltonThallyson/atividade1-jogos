using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour {
    
    private PlayerInputs playerInput;
    private InputAction onMovement;   
    private InputAction onGrab;   
    private InputAction onDrop;
    private InputAction onReload;
    private InputAction onSwitchFireMode;
    private InputAction onShoot;
    private PlayerMovement playerMovement;
    private PlayerInteractions playerInteractions;

    

    private void Awake() {
        playerInput = new PlayerInputs();
        onMovement = playerInput.Play.Move;
        onGrab = playerInput.Play.Grab;
        onDrop = playerInput.Play.Drop;
        onReload = playerInput.Play.Reload;
        onSwitchFireMode = playerInput.Play.Mode;
        onShoot = playerInput.Play.Shoot;
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();

        onGrab.performed += ctx => playerInteractions.Grab();
        onDrop.performed += ctx => playerInteractions.Drop();
        onReload.performed += ctx => playerInteractions.Reload();
        onShoot.started += ctx => playerInteractions.StartFiring();
        onShoot.canceled += ctx => playerInteractions.StopFiring();
        onSwitchFireMode.performed += ctx => playerInteractions.SwitchFireMode();
    }
    private void FixedUpdate() {
        playerMovement.Move(onMovement.ReadValue<Vector2>());
    }

    

    private void OnEnable() {
        onMovement.Enable();
        onGrab.Enable();
        onDrop.Enable();
        onReload.Enable();
        onShoot.Enable();
        onSwitchFireMode.Enable();
    }

    private void OnDisable() {
        onMovement.Disable();
        onGrab.Disable();
        onDrop.Disable();
        onReload.Disable();
        onShoot.Disable();
        onSwitchFireMode.Disable();
    }

    
}