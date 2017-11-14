using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeScreen : MonoBehaviour {
	public Button startButton;
	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener(StartButtonOnClick);
	}

	public void StartButtonOnClick(){
		GameManager.instance.SwitchState(GameState.Ongame);
	}

	void OnDestroy(){
		startButton.onClick.RemoveListener(StartButtonOnClick);
	}
}
