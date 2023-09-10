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

    void startTimer() {
        Destroy(gameObject, secondsToDestroy);
    }

    

    void Start()
    {
       rb =  gameObject.GetComponent<Rigidbody>();
       Debug.Log(rb.velocity.magnitude);
       startTimer();
    }

    void Update()
    {
       
            
            Vector3 magnusDirection = Vector3.Cross(rb.velocity, transform.right).normalized;

            Vector3 magnusForce = Mathf.Sqrt(rb.velocity.magnitude) * magnusDirection * backspin/100 * Time.fixedDeltaTime;
            rb.AddForce(magnusForce);
            Debug.Log("BB speed = " + rb.velocity.magnitude);
            
        

        
        
        
        // rb.AddForce(magnusForce );
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Ball : MonoBehaviour
// {
//     Rigidbody rb;
//     // Start is called before the first frame update
//     public float backspin = .02f;  // Taxa de rotação da BB. Ajuste conforme necessário.

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
        
//     }

//     void FixedUpdate()
//     {

//         Debug.DrawRay(transform.position, rb.velocity.normalized, Color.green, 100, false);
        
//         Debug.Log(rb.velocity.magnitude);
//         Vector3 magnusDirection = Vector3.Cross(rb.velocity, transform.right).normalized;
        
//         Vector3 magnusForce = Mathf.Sqrt(rb.velocity.magnitude) * magnusDirection * backspin * Time.fixedDeltaTime;
        
//         Debug.DrawRay(transform.position, magnusForce * 1000, Color.red, Mathf.Infinity);
//         rb.AddForce(magnusForce);
//     }
// }