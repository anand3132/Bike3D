using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BikeController : MonoBehaviour {
	public Rigidbody rb;
	public PlatformGenerator platformInstance;

	// initial bike transforms
	public  Vector3 bikePosition;
	public	Quaternion bikeRotation;

	private int thrust = 50;
	private float rotationSpeed = 2.5f;
	private bool lateralForceApplied = false;
	public float score = 0.0f;
	private int bonus = 10;
	public bool bikeHit = false;

	private Vector3 maxVelocity =new Vector3(200.0f, 0.0f, 0.0f);
	private Vector3 driveVelocity =new Vector3(20.0f, 0.0f, 0.0f);
	public Vector3 rotationAxis =new Vector3(1.0f, -1.0f, 0.0f);

	// gui components
	public Text gameStatusText;
	public Text scoreText;
	public Text speedText;
	public String FuelText;
	public Slider FuelSlider;

	//keyboard events
	public bool upArrow=false;
	public bool downArrow=false;
	public bool leftArrow=false;
	public bool rightArrow=false;

	public int BikeSpeed=0;

	void Start() {
		// cache the initial transfroms
		bikePosition = gameObject.transform.position;
		bikeRotation = gameObject.transform.rotation;
		InitGUI ();
	}

	void FixedUpdate() {
		UpdateGUI ();
		ProcessKeyBoard ();
	}

	void InitGUI() {
		// init gui components
		gameStatusText.text = " ";
		scoreText.text ="Score :"+ score;
		speedText.text = "0";
		FuelSlider.value = 100.0f;
		FuelText = "Fuel";
	}

	void ProcessKeyBoard() {
		// Applying drive force to move forward
		if(bikeHit==false){
			if (Input.GetKey (KeyCode.UpArrow)||upArrow) {
				FuelSlider.value -= 0.05f;
				score += .1f;
				if (rb.velocity.x < maxVelocity.x) {
					rb.AddForce (transform.forward * thrust);
				}
			}
//			if (Input.GetKeyUp (KeyCode.UpArrow)||upArrow) {
//				speedText.text = rb.velocity.magnitude.ToString();
//				rb.AddForce(-transform.forward * thrust);
//			}
			//Applying negative force to stop 
			if (Input.GetKey (KeyCode.DownArrow)||downArrow) {
				if (rb.velocity.x > 3f) {
					rb.AddForce (-transform.forward * thrust);
				}
			}

			// Applying lateral force to left
			if (Input.GetKey (KeyCode.LeftArrow)||leftArrow ) {
				TiltBike (15f, 14.5f);
			}

			// Applying lateral force to right
			if (Input.GetKey (KeyCode.RightArrow)||rightArrow) {
				TiltBike (-15f, 345.5f);
			}
		}
		//Reset Button!!
		if(Input.GetKey(KeyCode.R)){
			bikeHit = false;
			platformInstance.ResetGame();
		}

//		 To return to normal angle if no lateral force applied.
		if (!lateralForceApplied) {
			if (rb.velocity.x > driveVelocity.x) {
				int zAngle = (int)transform.eulerAngles.z;
				if (zAngle <= 15 && zAngle >= 0) {
					Quaternion tiltRotationAngle = Quaternion.AngleAxis (-15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
				}
				if (zAngle <= 360 && zAngle >= 345) {
					Quaternion tiltRotationAngle = Quaternion.AngleAxis (15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
				}
			}
		}

		ReCorrectAngle ();
	
	}//ProcessKeyBoard

	void UpdateGUI() {
		BikeSpeed = (int)rb.velocity.magnitude;

		//HUD Updates
		scoreText.text="Score :"+(int)score;
		speedText.text=BikeSpeed.ToString();
		FuelText = "Fuel";

		if (FuelSlider.value < 10) {
			FuelText = "Fuel Low!!";
		}
		if (FuelSlider.value < 1) {
			GameManager.instance.SwitchState(GameState.GameLose);
//			gameStatusText.text = "GameOver!!! press R to reset";
			rb.velocity = Vector3.zero;
		}
	}

	void ReCorrectAngle() {
		rb.transform.rotation = Quaternion.Lerp (transform.rotation, bikeRotation, rotationSpeed * 0.25f * Time.deltaTime);
	}

	void TiltBike(float tiltAngle,float limit) {
		if (rb.velocity.x > driveVelocity.x) {
			int zAngle = (int)transform.eulerAngles.z;
			if (zAngle <= 15 && zAngle >= 0 ||
				zAngle <= 360 && zAngle >= 345) {
				lateralForceApplied = true;
				Quaternion tiltRotationAngle = Quaternion.AngleAxis (tiltAngle, rotationAxis) * transform.rotation;
				rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
				if (transform.eulerAngles.z > 15.0f && transform.eulerAngles.z < 345.0f) {
					transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, limit);
				}
			}
		}
	}
		
	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Obstacle") {
			GameManager.instance.SwitchState(GameState.GameLose);
//			gameStatusText.text = "GameOver!!! Press R to reset";
			rb.velocity = Vector3.zero;
			bikeHit = true;
		}
		if (collision.transform.tag == "end") {
			GameManager.instance.SwitchState(GameState.GameWin);
			Debug.Log("Winstate");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.tag == "Fuel") {
			scoreText.text = "Score :" +(int)( score + bonus);
			FuelSlider.value += 20;
			other.gameObject.SetActive(false);
		}

	}
} //MonoBehaviour
