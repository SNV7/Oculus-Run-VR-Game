using UnityEngine;
using System.Collections;

public class BounceWallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		foreach (ContactPoint contact in col.contacts) {
			if(contact.thisCollider == collider){
				//this is the paddles contact point
				float english = contact.point.z - transform.position.z;
				contact.otherCollider.rigidbody.AddForce(0,0,500f * english);
			}
		}
	}
}
