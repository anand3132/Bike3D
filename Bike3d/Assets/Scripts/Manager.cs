//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Manager : MonoBehaviour {
//
//
//	public GameState gameState = GameState.kMaxState;
//	public int ItemSelector;
//	static public Manager instance = null;
//	public enum GameState
//	{
//		kTapToContinue,
//		kGamePlay,
//		kGameLose,
//		kPause,
//		kMaxState
//	}
//	void Awake()
//	{
//		//Check if instance already exist
//		if (instance==null)
//		{
//			//if not, set instance to this
//			instance = this;
//			//instance.respawnEnemies();
//		}
//		//If instance already exists and it's not this:
//		else if (instance!=this)
//		{	
//			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
//			Destroy(gameObject);    
//		}
//		//Sets this to not be destroyed when reloading scene
//		DontDestroyOnLoad(gameObject);
//		SetState(GameState.kTapToContinue);
//	}
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		switch (gameState) {
//		case GameState.kTapToContinue: {
//				if(Input.GetKeyUp(KeyCode.Space)) {
////					LoadGame ();
//					SetState(GameState.kGamePlay);
//				}
//			}
//			break;
//
//		case GameState.kGamePlay:
////			gameStatusText.text = "";
//			CheckForPauseKey ();
//			break;
//
//		case GameState.kGameLose: {
//				if(Input.touchCount==2||Input.GetMouseButtonDown(0)) {
////					DestroyAllGameObject ();
//					//ReduceScore (scoreValue);
//					SetState(GameState.kTapToContinue);
//				}
//			}
//			break;
//		case GameState.kPause: {
//				if(Input.GetKeyUp(KeyCode.Escape)) {
//					SetState(GameState.kGamePlay);
//				}
//			}
//			break;
//		}
//	}//update
//	public bool IsPaused()
//	{
//		return gameState==GameState.kPause;
//	}
//
//	public bool IsGamePlay()
//	{
//		return (gameState==GameState.kGamePlay);
//	}
//
//	public void SetState(GameState state)
//	{
//		if(gameState==state)
//			return;
//
//		gameState = state;
//	}
//
//	void CheckForPauseKey()
//	{
//		if(Input.GetKeyUp(KeyCode.Escape))
//		{
//			SetState(GameState.kPause);
//		}
//	}
//
//	//Enemy regenerator
//	void RespawnObjects()
//	{
//
//		Object _obj;
//		switch(ItemSelector)
//		{
//		case 1:
//			break;
//		case 2:
//			break;
//		default:
//			break;
//
//		}//switch case
//
//
//	}
//}//monodevelop
