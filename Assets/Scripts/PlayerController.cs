using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
	
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }
	void Update () {
	    if(Input.GetKey(KeyCode.LeftArrow)) {
            Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x - (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x + (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
        }
	}
}
