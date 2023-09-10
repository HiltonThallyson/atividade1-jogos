using System;
using GunNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInteractions : MonoBehaviour {

    private Grabbable heldObject;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private Transform raycastOriginPoint;
    [SerializeField] private LayerMask grabLayerMask;

    private GunScript gunScript;
    // private AmmoScript ammoScript;


    public void Grab() {
        if(Physics.Raycast(raycastOriginPoint.position, raycastOriginPoint.forward, out RaycastHit hit, interactionDistance, grabLayerMask, QueryTriggerInteraction.Ignore)) {
            if(hit.collider.TryGetComponent(out Grabbable grabbable) ) {
                Debug.Log("Type = " + grabbable.GetType());
                if(grabbable is IGun && heldObject == null) {
                    grabbable.Grab();
                    heldObject = grabbable;
                    gunScript = heldObject.GetComponent<GunScript>();
                    gunScript.GrabGun();
                }else if(grabbable is AmmoScript) {
                    grabbable.GrabAmmo();
                    Debug.Log(grabbable);
                    // ammoScript = grabbable.GetComponent<AmmoScript>();
                }else {
                    Debug.Log("Something went wrong");
                }
            }
        };
    }

    Coroutine fireCoroutine;

    public void StartFiring() {
        if(heldObject != null && heldObject is IGun) {
            GunScript gunScript = heldObject.GetComponent<GunScript>();
            fireCoroutine = StartCoroutine(gunScript.RapidFire());
        }
        
    }


    public void StopFiring() {
        if(fireCoroutine != null) {
            StopCoroutine(fireCoroutine);
        }
    }

    public void Drop() {
        if(heldObject != null) {
            heldObject.Drop();
            heldObject = null;
            gunScript.DropGun();
            gunScript = null;
        }
            
        
    }

    public void AdjustHopUp(InputAction.CallbackContext context) {
        if(heldObject != null) {
            gunScript.AdjustHopUP(context.action.ReadValue<float>()/1000);
        }
    }

    public void Reload()
    {
        if(heldObject != null && heldObject is IGun) {
            gunScript.ReloadGun();
            Debug.Log("Reloading...");
        }
    }

    public void SwitchFireMode()
    {
        if(heldObject != null && heldObject is IGun) {
            gunScript.SwitchFireMode();
            Debug.Log("Switching fire mode...");
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOriginPoint.position, raycastOriginPoint.forward * interactionDistance);
    }

    
}