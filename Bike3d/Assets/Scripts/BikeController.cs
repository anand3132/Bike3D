using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeController : MonoBehaviour {
	// Use this for initialization
	public int thrust = 20;
	public float rotationSpeed = 2.5f;
	private Rigidbody rb;

	private Vector3 maxVelocity =new Vector3(150, 0, 0);
	private Vector3 driveVelocity =new Vector3(20, 0, 0);
	public Vector3 rotationAxis =new Vector3(1, -1, 0);

	public Text gameStatusText;
	public Text scoreText;
	public Text speedText;
	public Text FuelText;
	public Slider FuelSlider;


	void Start() {
		gameStatusText.text = " ";
		scoreText.text =" ";
		speedText.text = " ";
		FuelSlider.value = 100.0f;
		FuelText.text = " ";
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		int BikeSpeed = (int)rb.velocity.x;
		speedText.text=BikeSpeed.ToString();
		if (Input.GetKey (KeyCode.UpArrow)) {
			FuelSlider.value -= 0.05f;
			if (rb.velocity.x < maxVelocity.x) {
				rb.AddForce (transform.forward * thrust);
				//Debug.Log("Forward");
			}
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			//Debug.Log("keyup");

			speedText.text = rb.velocity.magnitude.ToString();
			rb.AddForce(-transform.forward * thrust);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.x > 3f) {
				rb.AddForce (-transform.forward * thrust);
			}
		}

		// Applying lateral force to left
		bool lateralForceApplied = false;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			
			if (rb.velocity.x > driveVelocity.x) {
				int zAngle = (int)transform.eulerAngles.z;
				//Debug.Log ("Angle " + zAngle);
				if (zAngle <= 15 && zAngle >= 0 ||
				   zAngle <= 360 && zAngle >= 345) {
					//Debug.Log ("Entering L");
					lateralForceApplied = true;
					Quaternion tiltRotationAngle = Quaternion.AngleAxis (15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
					if (transform.eulerAngles.z > 15.0f && transform.eulerAngles.z < 345.0f) {
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 14.5f);
					}
				}
			}
		}

		// Applying lateral force to right
		if (Input.GetKey (KeyCode.RightArrow)) {
			
			if (rb.velocity.x > driveVelocity.x) {
				int zAngle = (int)transform.eulerAngles.z;
				if (zAngle <= 15 && zAngle >= 0 ||
				   zAngle <= 360 && zAngle >= 345) {
					// Debug.Log ("Entering R");
					lateralForceApplied = true;
					Quaternion tiltRotationAngle = Quaternion.AngleAxis (-15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
					if (transform.eulerAngles.z < 345.0f && transform.eulerAngles.z > 15.0f) {
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 345.5f);
					}
				}
			}
		}

		// To return to normal angle if no lateral force applied.
		if (!lateralForceApplied) {
			if (rb.velocity.x < driveVelocity.x) {
				return;
			}
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

		if (FuelSlider.value < 10) {
			FuelText.text = "Fuel Low!!";
		}
		if (FuelSlider.value < 1) {
			gameStatusText.text = "GameOver!!!";
			Time.timeScale -=0.5f;
		}
		// Debug.Log (FuelSlider.value);
	}//FixedUpdate

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Obstacle") {
			gameStatusText.text = "GameOver!!!";
			if(Time.timeScale!=0)
				Time.timeScale-=0.5f;
		}

	}
	void OnTriggerEnter(Collider other) {
	
		if (other.transform.tag == "Fuel") {
			Debug.Log ("Fuel");
			other.gameObject.SetActive(false);
			FuelSlider.value += 20;
		}
	}
} //MonoBehaviour
