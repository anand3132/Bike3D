﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeController : MonoBehaviour {
	// Use this for initialization
	public int thrust=20;
	private Rigidbody rb;
	private Vector3 maxVelocity =new Vector3(150,0,0);
	private Vector3 driveVelocity =new Vector3(20,0,0);
	private Vector3 rotationAxis =new Vector3(1,1,0);


	public Text gameStatusText;
	public Text scoreText;
	public Text speedText;
	public Text FuelText;
	public Slider FuelSlider;



	private Quaternion tiltRotationAngle=new Quaternion(0,0,0,0);
	//private Quaternion targetRotationAngle=new Quaternion(0,0,0,0);

	private float rotationSpeed = 1.0f;

	void Start() {
		gameStatusText.text = " ";
		scoreText.text =" ";
		speedText.text = " ";
		FuelSlider.value = 100;
		FuelText.text = " ";
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		int BikeSpeed = (int)rb.velocity.x;
		speedText.text=BikeSpeed.ToString();
		if (Input.GetKey (KeyCode.UpArrow)) {
			FuelSlider.value -= .1f;
			if (rb.velocity.x < maxVelocity.x) {
				rb.AddForce (transform.forward * thrust);
				//Debug.Log("Forward");
			}
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			//Debug.Log("keyup");

			speedText.text=rb.velocity.x.ToString();
			rb.AddForce(-transform.forward * thrust);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.x > 3f) {
				rb.AddForce (-transform.forward * thrust);
			}
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (rb.velocity.x > driveVelocity.x) {
				int zAngle = (int)transform.eulerAngles.z;
				if (zAngle <= 15 && zAngle >= 0 ||
				   zAngle <= 360 && zAngle >= 345) {
					tiltRotationAngle = Quaternion.AngleAxis (15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
					if (transform.eulerAngles.z > 15.0f && transform.eulerAngles.z < 345.0f) {
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 15);
					}
				}
			}
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (rb.velocity.x > driveVelocity.x) {
				int zAngle = (int)transform.eulerAngles.z;
				if (zAngle <= 15 && zAngle >= 0 ||
				   zAngle <= 360 && zAngle >= 345) {
					tiltRotationAngle = Quaternion.AngleAxis (-15f, rotationAxis) * transform.rotation;
					rb.transform.rotation = Quaternion.Lerp (transform.rotation, tiltRotationAngle, rotationSpeed * Time.deltaTime);
					if (transform.eulerAngles.z < 345.0f && transform.eulerAngles.z > 15.0f) {
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 345);
					}
				}
			}
		}
		if (FuelSlider.value < 10) {
			FuelText.text = "Fuel Low!!";
		}
		if (FuelSlider.value < 1) {
			gameStatusText.text = "GameOver!!!";
			Time.timeScale -=0.5f;
		}
		Debug.Log (FuelSlider.value);
	}//FixedUpdate

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Obstacle") {
			Debug.Log (collision.transform.tag);
			gameStatusText.text = "GameOver!!!";
			if(Time.timeScale!=0)
				Time.timeScale-=0.5f;
		}
		if (collision.transform.tag == "Fuel") {
			collision.gameObject.SetActive(false);
			FuelSlider.value += 20;
			Debug.Log (collision.transform.tag);
		}
	}
	
} //MonoBehaviour