using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private int health; //Keep track of player health

	public float speed = 6.0F; 
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private string LEVEL = "Level01";
	private float RotateSpeed = 30f;

	// Use this for initialization
	void Start () {
		setHealth (0);
	}//end method

	// Update is called once per frame
	private void Update () {


		if (checkIfFallen ()) {
			Application.LoadLevel(LEVEL);
		}//end if

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);


			//moveDirection = transform.Rotate(0f, Input.GetAxis ("Mouse X") * 30 * Time.deltaTime, 0f);
			//controller.transform.rotation = Quaternion.Euler(0f, Input.GetAxis ("Mouse X") * 30 * Time.deltaTime, 0f);

			moveDirection *= speed;
			if (Input.GetButton("Jump") && controller.isGrounded){
				//transform.position += transform.up * jumpSpeed * Time.deltaTime;
				moveDirection.y = jumpSpeed;
			}
		}//end if


		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

	}//end method

	/*
	 * Called when player has begun touching another rigidbody/collider
	*/
	private void OnCollisionEnter(Collision collision){
		Debug.Log("Collision was "+collision.relativeVelocity.magnitude);
		if (collision.gameObject.tag == "BOMBCUBE" && collision.relativeVelocity.magnitude > 10) { //Player collided with bomb

			//Check if bomb is in harmful state
			GameObject target = collision.gameObject;
			bool harm = target.GetComponent<BombCubeScript>().checkIfHarmful();
			Debug.Log("harm is "+harm);
			if(harm){
				loseHealth();
			}//end if

		}else if(collision.gameObject.tag == "BOMBSPHERE" && collision.relativeVelocity.magnitude > 5){
			//Check if bomb is in harmful state
			GameObject target = collision.gameObject;
			bool harm = target.GetComponent<BombSphereScript>().checkIfHarmful();
			Debug.Log("harm is "+harm);
			if(harm){
				loseHealth();
			}//end if
		}


		if (collision.gameObject.tag == "FINISH") { //Player reaced finish line
			Debug.Log ("You Win!!");
			Application.LoadLevel(LEVEL);
		}//end if
	}//end method



	private void onCollisionExit(){
		Debug.Log ("off ground");
	}


	/*
	 * Check if the player has fallen off the playform
	 * Returns true or false indicating if player fell or not
	 * Player is deemed fallen if Y pos becomes negative
	*/
	private bool checkIfFallen(){
		float currentY = rigidbody.transform.position.y;
		if (currentY < 0) {
			return true;
		}else{
			return false;
		}//end if
	}//end method


	/*
	 * Decrement the players health by 1
	 * If health falls below zero, player dies
	*/
	private void loseHealth(){
		//is in harmful state
		setHealth(getHealth() - 1);
		Debug.Log ("Hit!! Health is now "+getHealth());
		if (getHealth() < 0) {
			Debug.Log ("You Lose helath is "+getHealth());
			Application.LoadLevel(LEVEL);
		}//end if
	}//end method


	/*
	 * Set the health value of the player
	 * Takes an int parameter as the new helath value
	*/
	public void setHealth(int h){
		health = h;
	}//end method

	/*
	 * Returns the current health of the player, as an int value
	*/
	public int getHealth(){
		return health;
	}//end method


}//end class
