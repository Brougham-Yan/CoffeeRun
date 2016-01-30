using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float coffeeTemperature;
	public float temperatureDecay;

    public float acceleration;
    public float maxSpeed;
    bool jumping = false;
	public Vector3 velocity;
	Rigidbody body;
	float decayCooldown = 5f;//every 5 seconds, drop temperature by temperatureDecay
	float decayTimer;
    void Start(){
		body = GetComponent<Rigidbody> ();
        body.freezeRotation = true;
		decayTimer = decayCooldown;
    }
		
	void Update () {
		if (decayTimer > 0) {
			decayTimer -= Time.deltaTime;
		} else {
			decayTimer = decayCooldown;
			coffeeTemperature -= temperatureDecay;
		}

	    if(Input.GetKey(KeyCode.LeftArrow) ) {
          //  Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x - (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
			velocity.x = velX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) ) {
           // Rigidbody body = GetComponent<Rigidbody>();
            float velX = body.velocity.x + (acceleration * Time.deltaTime);
            velX = Mathf.Clamp(velX, -maxSpeed, maxSpeed);
            body.velocity = new Vector3(velX, body.velocity.y, body.velocity.z);
			velocity.x = velX;
        }

		if(Input.GetKeyDown(KeyCode.Space) && !jumping) {
            jumping = true;
            //Rigidbody body = GetComponent<Rigidbody>();
            float velY = body.velocity.y + 5f;
            body.velocity = new Vector3(body.velocity.x, velY, body.velocity.z);
			velocity.x = body.velocity.x; // save x velocity when you jump so you will keep it upon landing
        }

		if (jumping) {
			body.velocity = new Vector3(velocity.x, body.velocity.y, body.velocity.z);
		}
	}

	void fixedUpdate()
	{
		if (jumping) {
			body.velocity = new Vector3(velocity.x, body.velocity.y, body.velocity.z);
		}
	}

    void OnTriggerEnter(Collider collider) {
        jumping = false;
		body.velocity = new Vector3(velocity.x, body.velocity.y, body.velocity.z); // when you land, keep x velocity; this mostly fixes the momentum loss (still a slight visual bug)
    }
		
}
