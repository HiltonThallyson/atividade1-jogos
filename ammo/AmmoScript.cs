using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GunNamespace
{

    
        public class AmmoScript : Grabbable, IAmmo
        {

       [SerializeField] private int ammoCapacity;
        private int ammoLeft;
        [SerializeField] private AmmoType ammoType;
        // private BbScript bbScript;
        [SerializeField] private GameObject bb;
        private void Start() {
            ammoLeft = ammoCapacity;
        }

        private void Awake() {
        }

        public GameObject GetBB() {
            return bb;
        } 
        
        public int GetAmmoCapacity()
        {
            return ammoCapacity;
        }

        public int GetAmmoLeft()
        {
            return ammoLeft;
        }

        public AmmoType GetAmmoType()
        {
            return ammoType;
        }

        
        public void DropAmmo()
        {
            Destroy(gameObject);
        }

        public void DecreaseAmmo()
        {
            ammoLeft--;
        }
    }
    
}
