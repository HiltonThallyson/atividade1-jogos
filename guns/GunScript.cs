using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace GunNamespace
{

    public class GunScript : Grabbable, IGun
    {
        public Transform location;
        [SerializeField]
        private int numberOfBullets;

        [SerializeField] private TMP_Text ammoLeftText;
        [SerializeField] private TMP_Text ammoMassText;

        private GameObject bb;
        public GunTypes gunType;
        private IAmmo ammo;
        [SerializeField] private AmmoType ammoType;

        [SerializeField]
        private float forcaDaMola = 1.2f;
        private float motorRotation;
        [SerializeField]
        private float hopup = 0.02f;
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

        void Awake()
        {   
            SetUpGun();
            ammoLeftText.SetText(numberOfBullets.ToString());
            ammoMassText.SetText("0.0g");
        }

        public void SetUpGun() {
            currentFireMode = GetFireMode();
            ammoType = GetAmmoType();
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

        public void DropGun() {
            ammoLeftText.SetText("0");
            ammoMassText.SetText("0.0g");
        }

        public void GrabGun() {
            if(ammo != null) {
                ammoLeftText.SetText(numberOfBullets.ToString());
                ammoMassText.SetText((ammo.GetBB().GetComponent<Rigidbody>().mass*1000).ToString() + "g");
            }
            
        }

        public void AdjustHopUP(float value) {
            hopup += value;
            Debug.Log("HopUp =" + hopup);
        }
    
        public void Shoot()
        {
            if(numberOfBullets > 0) {
                ammo.DecreaseAmmo();
                numberOfBullets --;
                ammoLeftText.SetText(numberOfBullets.ToString());
                Vector3 pontoInstanciacao = location.position;
                GameObject instantiatedBB = Instantiate(bb, pontoInstanciacao, location.rotation);
                Rigidbody bbRigidBody = instantiatedBB.GetComponent<Rigidbody>();

                instantiatedBB.GetComponent<BbScript>().setBackSpin(hopup);

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

        public AmmoType GetAmmoType() {
            return gunType switch
            {
                GunTypes.Pistol => AmmoType.Pistol,
                GunTypes.Shotgun => AmmoType.Shotgun,
                GunTypes.Assault => AmmoType.Assault,
                _ => AmmoType.Unknown,
            };
        }

        public void ReloadGun() {
            if(ammo != null) {
                numberOfBullets = ammo.GetAmmoCapacity();
                ammoLeftText.SetText(numberOfBullets.ToString());
                bb = ammo.GetBB();
            }
             
        }

        

        public void SetUpCartridge(IAmmo cartridge)
        {
            if(cartridge.GetAmmoType() == ammoType) {
                ammo = cartridge;
                
                ammoMassText.SetText((ammo.GetBB().GetComponent<Rigidbody>().mass*1000).ToString() + "g");
                ReloadGun();
            }
            
        }

        
    }
}
