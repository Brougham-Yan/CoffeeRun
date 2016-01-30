using UnityEngine;
using System.Collections;

public class soundtrigger : MonoBehaviour {

	public AudioClip clip;
	public float range;
	public float playChance;
	public float distance;
	Transform target;
	bool inRange;

	// Use this for initialization
	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RangeCheck ();
	}

	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, target.transform.position);

		if (distance < range && !inRange) {
			playSound ();
			inRange = true;
		} else if (distance > range && inRange) {
			inRange = false;
		}
	}

	void playSound()
	{
		if (Random.value < playChance) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
		}
	}
		
}
