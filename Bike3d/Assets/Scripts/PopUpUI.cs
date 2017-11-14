using UnityEngine;
using UnityEngine.UI;
using AudioRecorder;

public class PopUpUI : MonoBehaviour {
	 
	Recorder recorder;
	AudioSource audioSource;
	public bool autoPlay;
	public Text gameMessage;
	string log = "";

	//UI Elements
	public Button playButton;
	public Button recorderButton;
	public Button saveRecord;
	public Button restartGame;


	void OnEnable() {
		if(GameManager.instance.getCurrentState()==GameState.GameWin) {
			gameMessage.text="Success!!";
		} else if(GameManager.instance.getCurrentState()==GameState.GameLose) {
			gameMessage.text="You Lost!!";
		}
		recorderButton.onClick.AddListener(RecordButtonOnClick);
		playButton.onClick.AddListener(PlayBackButtonOnClick);
		saveRecord.onClick.AddListener(SaveButtonOnclick);
		restartGame.onClick.AddListener(RestartGameOnclick);

		recorder = new Recorder();
		Recorder.onInit += OnInit;
		Recorder.onFinish += OnRecordingFinish;
		Recorder.onError += OnError;
		Recorder.onSaved += OnRecordingSaved;
	}

	void OnDisable() {
		Recorder.onInit -= OnInit;
		Recorder.onFinish -= OnRecordingFinish;
		Recorder.onError -= OnError;
		Recorder.onSaved -= OnRecordingSaved;
		recorderButton.onClick.RemoveListener(RecordButtonOnClick);
		playButton.onClick.RemoveListener(PlayBackButtonOnClick);
		saveRecord.onClick.RemoveListener(SaveButtonOnclick);
		restartGame.onClick.RemoveListener(RestartGameOnclick);

	}

	public void RecordButtonOnClick(){
		if(recorder.IsReady)  {
			if(!recorder.IsRecording){
				recorderButton.GetComponentInChildren<Text>().text="Recording";
				saveRecord.GetComponentInChildren<Text>().text="Save Audio";
				recorderButton.GetComponentInChildren<Text>().color=Color.red;
				recorder.StartRecording(false,60);
				playButton.interactable=false;
				saveRecord.interactable=false;
			}else{
				recorder.StopRecording();
				recorderButton.GetComponentInChildren<Text>().text="Record Audio!!";
				saveRecord.GetComponentInChildren<Text>().text="Save Audio";
				recorderButton.GetComponentInChildren<Text>().color=Color.black;
				playButton.interactable=true;
				saveRecord.interactable=true;

			}

		}
	}
	public void RestartGameOnclick(){
		GameManager.instance.SwitchState(GameState.Reset);
	}
	public void PlayBackButtonOnClick(){

		if(recorder.hasRecorded) {
			recorder.StopRecording();
			recorder.PlayRecorded(audioSource);
		
		}else{
			saveRecord.GetComponentInChildren<Text>().text="NoRecordFound";
		}
	}

	public void SaveButtonOnclick(){
		if(recorder.hasRecorded) {
			saveRecord.GetComponentInChildren<Text>().text="Save Audio";
			recorder.Save(System.IO.Path.Combine(Application.persistentDataPath,"Audio"+Random.Range(0,10000)+".wav"),recorder.Clip);
		} else{
			saveRecord.GetComponentInChildren<Text>().text="NoRecordFound";
		}
	}

 	void Start() {  
		audioSource = GameObject.FindObjectOfType<AudioSource>();
		if(!audioSource)
			audioSource=gameObject.AddComponent<AudioSource>();
		recorder.Init();
	}  
	
	void OnInit(bool success) {
		Debug.Log("Success : "+success);
		log += "\nSuccess";
	}

	void OnError(string errMsg) {
		Debug.Log("Error : "+errMsg);
		log += "\nError " + errMsg;
	}

	void OnRecordingFinish(AudioClip clip) {
		if(autoPlay) {
			recorder.PlayAudio (clip, audioSource);
		}
	}

	void OnRecordingSaved(string path) {
		Debug.Log("File Saved at : "+path);
		log += "\nFile save at : "+path;
		recorder.PlayAudio (path, audioSource);
	}
}
