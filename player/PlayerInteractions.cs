using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInteractions : MonoBehaviour {

    private Grabbable heldObject;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private Transform raycastOriginPoint;
    [SerializeField] private LayerMask grabLayerMask;


    public void Grab() {
        if(Physics.Raycast(raycastOriginPoint.position, raycastOriginPoint.forward, out RaycastHit hit, interactionDistance, grabLayerMask, QueryTriggerInteraction.Ignore)) {
            if(hit.collider.TryGetComponent(out Grabbable grabbable) && heldObject == null) {
                grabbable.Grab();
                heldObject = grabbable;
            }
        };
    }

    public void Drop() {
        if(heldObject != null) {
            heldObject.Drop();
            heldObject = null;
        }
            
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOriginPoint.position, raycastOriginPoint.forward * interactionDistance);
    }
}