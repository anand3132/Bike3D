using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	MaxState
	,Ongame
	,GameStart
	,GamePause
	,GameWin
	,GameLose
	,Resume
	,Reset
	,Exit
}
public class GameManager : MonoBehaviour {
	
	private GameState state=GameState.MaxState;
	public GameObject mainGameObject=null;
	public GameObject uxGameObject=null;
	public int gameScore=0;
	public static GameManager instance=null;
	private float gametime=0;
	private float gameStartTime=0;
	public GameObject defaultCamera;
	//controllers
	public UXController uxInstance;
	public PlatformGenerator platformInstance;
	void Awake(){
		if(instance==null)
			instance=this;
		else if (instance != this)
			Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);
		uxInstance=uxGameObject.GetComponent<UXController>();
	}

	public GameState getCurrentState(){
		return state;
	}
	public void SwitchState(GameState switchState){
		state = switchState;
	}


	void Start () {
		SwitchState(GameState.GameStart);
	}
	void  GameLostRules(){
		if(gametime>25)
		{
			SwitchState(GameState.GameLose);
		}
	}
	public float getGameTimer()
	{
		float t = Time.time - gameStartTime;
		gametime=(t%60);
		return gametime;
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case GameState.GameStart:
			defaultCamera.SetActive(true);
			uxInstance.isSwitchScreenPossible(UIscreens.HomeScreenUI);
			gameStartTime = Time.time;

			break;
		case GameState.Ongame:
			uxInstance.isSwitchScreenPossible(UIscreens.HUDScreenUI);
			mainGameObject.SetActive(true);
			defaultCamera.SetActive(false);
			GameLostRules();
			break;
		case GameState.GameLose:
			uxInstance.isSwitchScreenPossible(UIscreens.PopUpScreenUI);
			defaultCamera.SetActive(true);
			break;
		case GameState.Reset:
			platformInstance.ResetGame();
			break;
		case GameState.GameWin:
			uxInstance.isSwitchScreenPossible(UIscreens.PopUpScreenUI);

			break;
		}
	}
}
