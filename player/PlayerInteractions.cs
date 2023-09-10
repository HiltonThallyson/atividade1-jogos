using System;
using GunNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInteractions : MonoBehaviour {

    private Grabbable heldObject;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private Transform raycastOriginPoint;
    [SerializeField] private LayerMask grabLayerMask;

    private GunScript gunScript;
    private AmmoScript ammoScript;


    public void Grab() {
        if(Physics.Raycast(raycastOriginPoint.position, raycastOriginPoint.forward, out RaycastHit hit, interactionDistance, grabLayerMask, QueryTriggerInteraction.Ignore)) {
            if(hit.collider.TryGetComponent(out Grabbable grabbable) && heldObject == null) {
                Debug.Log("Type = " + grabbable.GetType());
                if(grabbable is IGun) {
                    grabbable.Grab();
                    heldObject = grabbable;
                    gunScript = heldObject.GetComponent<GunScript>();
                }else if(grabbable is IAmmo) {
                    grabbable.GrabAmmo();
                    ammoScript = grabbable.GetComponent<AmmoScript>();
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
            // gunScript.Shoot();
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
            gunScript = null;
        }
            
        
    }

    public void RapidFire() {

    }
    public void GrabAmmo() {
        Debug.Log("Got ammo...");
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

    public void Shoot() {
        if(heldObject != null && heldObject is IGun) {
            GunScript gunScript = heldObject.GetComponent<GunScript>();
            gunScript.Shoot();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOriginPoint.position, raycastOriginPoint.forward * interactionDistance);
    }

    
}