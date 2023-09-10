using GunNamespace;
using UnityEngine;

public class Grabbable : MonoBehaviour {
    
    [SerializeField] Transform gunContainer;
    public void Grab() {
        gameObject.transform.SetParent(gunContainer);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gameObject.transform.localScale = Vector3.one;
        Debug.Log("Grabbed " + gameObject);
    }

    public void Drop() {
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Dropped " + gameObject);
    }

    public void GrabAmmo() {
        // gameObject.SetActive(false);
        GunScript gunScript =  gunContainer.GetComponentInChildren<GunScript>();
        gunScript?.SetUpCartridge(gameObject.GetComponent<IAmmo>());
    }

}