using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour {
	
	public Text timer;
	private float startTime;

	public Text gameTimeText;
	public Text speedText;
	public Text fuelText;
	public Slider fuelSlider;
	public Text gameStatusText;
	public BikeController bikeInstance;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.getCurrentState()==GameState.Ongame){
			fuelText.text=bikeInstance.FuelText;
			speedText.text=bikeInstance.BikeSpeed.ToString();
			gameTimeText.text="Time : "+GameManager.instance.getGameTimer().ToString("f2");
			if(GameManager.instance.getGameTimer()>25)
			{
//				GameManager.instance.SwitchState(GameState.GameLose);
			}
		}
	}

}
