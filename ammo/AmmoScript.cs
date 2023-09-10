using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GunNamespace
{

    
        public class AmmoScript : Grabbable, IAmmo
        {   
            // [SerializeField]
            // private GameObject bb;
            // private int numberOfBullets;
        
            // [SerializeField] private int bulletsCapacity;
            // private GunTypes ammoType;
        
            // private void setUpAmmo()
            // {
            //     bulletsCapacity = 20;
            //     ammoType = GunTypes.Assault;
            //     numberOfBullets = bulletsCapacity;
            // }

            // public int getAmmoLeft() {
            //     return numberOfBullets;
            // }

            // public void reduceAmmo() {
            //     if(numberOfBullets > 0) {
            //         numberOfBullets--;
            //     }
            // }

        

            // public void rechargeAmmo() {
            //     Debug.Log("Rechargindg...");
            //     numberOfBullets = bulletsCapacity;
            // }
            // // Start is called before the first frame update
            // void Start()
            // {
            //     setUpAmmo();
            // }

    

            // // Update is called once per frame
            // void Update()
            // {
        
            // }

            public void PrintAmmo()
            {
                Debug.Log("Printing ammo");
            }
        }
    
}
