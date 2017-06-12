using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
	// Use this for initialization
	public GameObject tobeFollowed=null;
	public GameObject middleTile = null;
	public Vector3 prevPos;
	public List<Transform> tiles = new List<Transform> ();

	void Start () {
		if (!tobeFollowed) {
			Debug.LogError ("FollowObject not set");
			return;
		}

		prevPos = tobeFollowed.transform.position;
		for (int x = 0; x< transform.childCount; x++) {
			tiles.Add(transform.GetChild(x));
		}

		int midPoint = tiles.Count / 2;
		middleTile = tiles.ElementAt (midPoint).gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (!tobeFollowed) {
			Debug.LogError ("FollowObject not set");
			return;
		}

		Vector3 velocity = tobeFollowed.transform.position - prevPos;
		bool movingForward = (velocity.x > 0);

		if (tobeFollowed.transform.position.x > middleTile.transform.position.x && movingForward) {
			Transform lastTile = tiles.ElementAt(0);
			Transform firstTile = tiles.ElementAt (tiles.Count - 1);
			float sizeX = firstTile.transform.GetComponent<BoxCollider> ().bounds.size.x;
			lastTile.SetPositionAndRotation (firstTile.transform.position + new Vector3 (sizeX, 0, 0),  lastTile.transform.rotation);
			tiles.Remove (lastTile);
			tiles.Add (lastTile);
			int midPoint = tiles.Count / 2;
			middleTile = tiles.ElementAt (midPoint).gameObject;

			// do obstacle
			int randomNumber= (int)Random.Range (0f, 100.0f);
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
				//Transform obstacle = lastTile.Find ("Obstacle");
					if (FuelObject != null) {
					FuelObject.gameObject.SetActive (false);

					}
				if (obstacle != null) {
					obstacle.gameObject.SetActive (false);
				}
			}
		}
	}

}
