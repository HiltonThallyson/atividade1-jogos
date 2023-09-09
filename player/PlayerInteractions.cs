using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private Grabbable heldObject;
    private float interactionDistance = 5f;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask grabLayerMask;

    

    private void Update() {
        
        if(Input.GetKey(KeyCode.E)) {
            Grab();
            
        }
        
    }

    
    public void Grab() {
        if(Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, interactionDistance, grabLayerMask, QueryTriggerInteraction.Ignore)) {
                if(hit.collider.TryGetComponent(out Grabbable grabbable)) {
                    grabbable.Grab();
                }
            };
    }

}