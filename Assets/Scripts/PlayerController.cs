using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    bool jumping = false;
	
    void Start(){
        Rigidbody body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }
	void Update () {
	    if(Input.GetKey(KeyCode.LeftArrow) && !jumping) {
            Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x - (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !jumping) {
            Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x + (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            jumping = true;
            Rigidbody body = GetComponent<Rigidbody>();
            float velY = body.velocity.y + 5f;
            body.velocity = new Vector3(body.velocity.x, velY, body.velocity.z);
        }
	}

    void OnTriggerEnter(Collider collider) {
        jumping = false;
    }
}
