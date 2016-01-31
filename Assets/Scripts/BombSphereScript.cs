using UnityEngine;
using System.Collections;

public class BombSphereScript : MonoBehaviour {
	public int xPush = 200000;
	public int yPush;
	public int zPush;
	public bool isHarmful = true;

	// Use this for initialization
	void Start () {
		rigidbody.AddRelativeForce(xPush,yPush,zPush);
	}
	
	// Update is called once per frame
	void Update () {
		checkIfHarmful ();
	}

	/*
	 * Checks if the Sphere bomb is harmful to touch
	 * If the Cube is moving its harmful
	 * If it is not moving than its safe to touch
	*/
	public bool checkIfHarmful(){
		if (rigidbody.velocity.magnitude < 0.3) {
			//not moving, not harmful to player
			gameObject.renderer.material.color = Color.white;
			return false;
		}//end if
		gameObject.renderer.material.color = Color.red;
		return true;
	}//end method
}
