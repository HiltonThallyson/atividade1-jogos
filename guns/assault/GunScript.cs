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

        private float nextShootTime;

        private bool canManualShoot = true;

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
            { FireMode.Manual, 1f },
            { FireMode.SemiAuto, 0.3f},
            { FireMode.Auto, 0.06f }
        };

        public void setUpGun() {
            currentFireMode = GetFireMode();
            Debug.Log(currentFireMode);
            motorRotation = motorRotations[currentFireMode];
            nextShootTime = motorRotation;
            // ammoScript = cartridgePrefab.GetComponent<AmmoScript>();
            numberOfBullets = 0; 
        }

        private FireMode GetFireMode() {
            if(gunType is GunTypes.Pistol or GunTypes.Shotgun) {
                Debug.Log("Entrou");
                return FireMode.Manual;
            }else {
                return FireMode.SemiAuto;
            }
        }
    
        public void onShootPress()
        {
            // ammoScript.reduceAmmo();
            Vector3 pontoInstanciacao = location.position;
            GameObject instantiatedBB = Instantiate(cartridgePrefab, pontoInstanciacao, location.rotation);
            Rigidbody bbRigidBody = instantiatedBB.GetComponent<Rigidbody>();

            instantiatedBB.GetComponent<BbScript>().setBackSpin(backSpin);

            if (bbRigidBody != null)
            {
                Vector3 gunDirection = location.forward;
                Debug.Log("bbDirection = " + gunDirection);
                Debug.Log("gunDirection = " + transform.forward);


                bbRigidBody.AddForce(gunDirection * forcaDaMola, ForceMode.Impulse);
            }
        }

        public void alternateFireMode() 
        {
            if(gunType == GunTypes.Assault ) {
                if(currentFireMode.Equals(FireMode.SemiAuto)){
                    currentFireMode = FireMode.Auto;
                    canManualShoot = false;
                
                }else if(currentFireMode.Equals(FireMode.Auto)) {
                    currentFireMode = FireMode.Manual;
                    canManualShoot = true;
                }else {
                    currentFireMode = FireMode.SemiAuto;
                    canManualShoot = false;
                }
            }
            
                
           
            motorRotation = motorRotations[currentFireMode];
        }

        public void reloadGun() {
            // ammoScript.rechargeAmmo();
            numberOfBullets = 60; 
            nextShootTime = Time.time;
        }

        // Start is called before the first frame update
        void Start()
        {   
            setUpGun();
        
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKey(KeyCode.Mouse0) && numberOfBullets > 0)
            {
            
                if (Time.time >= nextShootTime && !currentFireMode.Equals(FireMode.Manual))
                {
                    onShootPress();
                    numberOfBullets--;
                    nextShootTime = Time.time + motorRotation;
                }else if(canManualShoot){
                    canManualShoot = false;
                    onShootPress();
                    numberOfBullets--;
                }
            }

            if(Input.GetKeyUp(KeyCode.Mouse0)){
                canManualShoot = true;
            }

            if (Input.GetKey(KeyCode.R))
            {
                reloadGun();
            }

            if(Input.GetKeyDown(KeyCode.T)){
                alternateFireMode();
            }
        }

    
    }
 



}
