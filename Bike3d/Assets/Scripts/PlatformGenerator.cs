using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
	// Use this for initialization
	public BikeController bikeInstance = null;
	private GameObject middleTile = null;
	public Vector3 prevPos;
	public List<Transform> tiles = new List<Transform> ();
	public Material checkPointMaterial;
	public Material roadMaterial;
	public int tileCount=0;
	void Start () {
		if (!bikeInstance) {
			Debug.LogError ("FollowObject not set");
			return;
		}
	
		// cache the previous position.
		prevPos = bikeInstance.transform.position;

		// add the tiles to our list.
		for (int x = 0; x< transform.childCount; x++) {
			tiles.Add(transform.GetChild(x));
		}

		// calculate the mid tile.
		int midPoint = tiles.Count / 2;
		middleTile = tiles.ElementAt (midPoint).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!bikeInstance) {
			Debug.LogError ("FollowObject not set");
			return;
		}

		DoMoveForward ();
		Debug.Log(tileCount);
	}//Update

	private void DoMoveForward() {
		Vector3 velocity = bikeInstance.transform.position - prevPos;
		bool movingForward = (velocity.x > 0);
		if(GameManager.instance.getGameTimer()>25)
		{
			
		}
		// check if our bike crossed the mid tile while moving forward.
		if (bikeInstance.transform.position.x > middleTile.transform.position.x && movingForward) {
			// Move the tile towards the last of our list and re-calculate the mid tile.
			Transform lastTile = tiles.ElementAt(0);
			Transform firstTile = tiles.ElementAt (tiles.Count - 1);
			float sizeX = firstTile.transform.GetComponent<BoxCollider> ().bounds.size.x;
			lastTile.SetPositionAndRotation (firstTile.transform.position + new Vector3 (sizeX, 0, 0),  lastTile.transform.rotation);
			tiles.Remove (lastTile);
			tiles.Add (lastTile);
			int midPoint = tiles.Count / 2;
			middleTile = tiles.ElementAt (midPoint).gameObject;
			if(tileCount>8){
				lastTile.GetComponent<Renderer>().material=checkPointMaterial;
				lastTile.tag="end";
			}
			Debug.Log(lastTile.tag);
			// random Obstacle & Fuel Generation
			DoRandomObstacles (lastTile);
			tileCount++;
		}
	}

	private void DoRandomObstacles(Transform lastTile) {
		int randomNumber = (int)Random.Range (0f, 100.0f);
		Transform obstacle = lastTile.Find ("Obstacle");
		Transform FuelObject = lastTile.Find ("Fuel");
		if ((randomNumber % 3) == 0) {
			if (FuelObject != null) {
				FuelObject.gameObject.SetActive (true);
			}
			if (obstacle != null) {
				obstacle.gameObject.SetActive (true);
			}
		} else {
			if (FuelObject != null) {
				FuelObject.gameObject.SetActive (false);

			}
			if (obstacle != null) {
				obstacle.gameObject.SetActive (false);
			}
		}
	}

	public void ResetGame() {
		bikeInstance.bikeHit = false;
		bikeInstance.score = 0;
		bikeInstance.gameStatusText.text = " ";
		bikeInstance.speedText.text = "0";
		bikeInstance.FuelSlider.value = 100.0f;
		bikeInstance.FuelText = "Fuel";

		Transform secondTile = tiles.ElementAt(1);
		foreach(Transform item in tiles)
		{
			item.tag="";
			item.GetComponent<Renderer>().material=roadMaterial;
		}
		Vector3 secondTilePos = secondTile.transform.position;
		bikeInstance.transform.SetPositionAndRotation(new Vector3(secondTilePos.x, 2.0f, secondTilePos.z), bikeInstance.bikeRotation);
		bikeInstance.rb.velocity = Vector3.zero;
		GameManager.instance.SwitchState(GameState.GameStart);
	}
}//MonoBehaviour
