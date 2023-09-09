using System;
using System.Collections;
using System.Collections.Generic;
using GunNamespace;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public GameObject gun;
    
    public new BoxCollider collider;
    public Transform player, gunContainer;



    public bool isEquipped;
    public static bool isFull;

    public float pickUpRange;
    


    
    // Start is called before the first frame update
    void Start()
    {
        
        if(!isEquipped) {
            gun.GetComponent<Rigidbody>().isKinematic = false;
            collider.isTrigger = false;

            gun.GetComponent<GunScript>().enabled = false;
        }

        if(isEquipped) {
            gun.GetComponent<Rigidbody>().isKinematic = true;
            collider.isTrigger = true;

            gun.GetComponent<GunScript>().enabled = true;
            isFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if(!isEquipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKey(KeyCode.E) && !isFull){
            
                PickUpGun();
            
        }
        

        if(isEquipped && Input.GetKey(KeyCode.G)){
            DropGun();
        }
    }

    private void DropGun()
    {
        isEquipped = false;
        isFull = false;

        gun.GetComponent<Rigidbody>().isKinematic = false;
        collider.isTrigger = false;

        gun.GetComponent<GunScript>().enabled = false;

        transform.SetParent(null);
        
    }

    private void PickUpGun()
    {
        isEquipped = true;
        isFull = true;

        gun.GetComponent<Rigidbody>().isKinematic = true;
        collider.isTrigger = true;

        gun.GetComponent<GunScript>().enabled = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

    }

    private void PickUpAmmo() {

    }
}
