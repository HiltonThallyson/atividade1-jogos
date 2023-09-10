using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GunNamespace
{

    public class GunScript : Grabbable, IGun
    {
        [SerializeField]
        private GameObject cartridgePrefab;
        public Transform location;
        [SerializeField]
        private int numberOfBullets;
        public GunTypes gunType;
        private AmmoScript ammoScript;
        [SerializeField]
        private float forcaDaMola = 1.2f;
        private float motorRotation;
        [SerializeField]
        private float backSpin = 0.02f;
        private FireMode currentFireMode; 
        public enum FireMode
        {
            Manual,  
            SemiAuto,
            Auto  
        }


        private Dictionary<FireMode, float> motorRotations = new Dictionary<FireMode, float>()
        {
            { FireMode.Manual, 0.01f },
            { FireMode.SemiAuto, 8f},
            { FireMode.Auto, 15f }
        };

        WaitForSeconds rapidFireWait;
        private bool rapidFire;

        public IEnumerator RapidFire() {
            if(rapidFire) {
                while(true) {
                    Shoot();
                    yield return rapidFireWait;
                }
            }else {
                Shoot();
                yield return null;
            }
        }

        public void SetUpGun() {
            currentFireMode = GetFireMode();
            motorRotation = motorRotations[currentFireMode];
            numberOfBullets = 0; 
            rapidFireWait = new WaitForSeconds(1/motorRotation);
        }

        private FireMode GetFireMode() {
            if(gunType is GunTypes.Pistol or GunTypes.Shotgun) {
                rapidFire = false;
                return FireMode.Manual;
            }else {
                rapidFire = true;
                return FireMode.SemiAuto;
            }
        }
    
        public void Shoot()
        {
            if(numberOfBullets > 0) {
                numberOfBullets --;
                Vector3 pontoInstanciacao = location.position;
                GameObject instantiatedBB = Instantiate(cartridgePrefab, pontoInstanciacao, location.rotation);
                Rigidbody bbRigidBody = instantiatedBB.GetComponent<Rigidbody>();

                instantiatedBB.GetComponent<BbScript>().setBackSpin(backSpin);

                if (bbRigidBody != null)
                {
                    Vector3 gunDirection = location.forward;
                    bbRigidBody.AddForce(gunDirection * forcaDaMola);
                }
            }
        }

        
        public void SwitchFireMode() 
        {
            if(gunType == GunTypes.Assault ) {
                if(currentFireMode.Equals(FireMode.SemiAuto)){
                    currentFireMode = FireMode.Auto;
                    rapidFire = true;
                
                }else if(currentFireMode.Equals(FireMode.Auto)) {
                    currentFireMode = FireMode.Manual;
                    rapidFire = false;
                }else {
                    currentFireMode = FireMode.SemiAuto;
                    rapidFire = true;
                }
            }
            motorRotation = motorRotations[currentFireMode];
            rapidFireWait = new WaitForSeconds(1/motorRotation);
        }

        public void ReloadGun() {
            numberOfBullets = 60; 
        }

        void Awake()
        {   
            SetUpGun();
        }
    }
}
