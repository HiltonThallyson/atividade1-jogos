using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    float backspin = 0.02f;

    public void setBackSpin(float value) {
        backspin = value;
    }

    public int secondsToDestroy = 5;

    void StartTimer() {
        Destroy(gameObject, secondsToDestroy);
    }

    

    void Start()
    {
       rb =  gameObject.GetComponent<Rigidbody>();
       StartTimer();
    }

    void Update()
    {
            Vector3 magnusDirection = Vector3.Cross(rb.velocity, transform.right).normalized;
            Vector3 magnusForce = Mathf.Sqrt(rb.velocity.magnitude) * magnusDirection * backspin/100 * Time.deltaTime;

            rb.AddForce(magnusForce);
    }
}

