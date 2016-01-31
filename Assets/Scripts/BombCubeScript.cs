using UnityEngine;
using System.Collections;

public class BombCubeScript : MonoBehaviour {

	public int dropBombTime  = 3;
	private float timer = 0;
	public bool isHarmful = true;
	public PlayerScript player;

	// Use this for initialization
	void Start () {
		rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkIfHarmful ();
		//Debug.Log ("Player distance is "+checkPlayerDistance()*1000);
		timer += 1.0F * Time.deltaTime;

		if (timer > dropBombTime) {
			rigidbody.useGravity = true;
		}
	}

	/*
	 * Calcualtes the x/z distance between the player and the bomb
	 * Ignores vertical, y, distance
	*/
	private float checkPlayerDistance(){
		float xDistnace = rigidbody.transform.position.x - player.transform.position.x;
		float zDistnace = rigidbody.transform.position.z - player.transform.position.z;
		xDistnace = Mathf.Pow (xDistnace, 2);
		zDistnace = Mathf.Pow (zDistnace, 2);
		float distance = xDistnace + zDistnace;
		distance = Mathf.Sqrt (distance);
		return distance;
	}//end method

	/*
	 * Checks if the Cube bomb is harmful to touch
	 * If the Cube is moving its harmful
	 * If it is not moving than its safe to touch
	*/
	public bool checkIfHarmful(){
		if (rigidbody.velocity.magnitude < 1) {
			//not moving, not harmful to player
			gameObject.renderer.material.color = Color.white;
			return false;
		}//end if
		gameObject.renderer.material.color = Color.red;
		return true;
	}//end method
}
