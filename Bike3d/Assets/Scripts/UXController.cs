using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIscreens{
	None
	,HomeScreenUI
	,PopUpScreenUI
	,HUDScreenUI
}

public class UXController : MonoBehaviour {

	public HUDUI hudInstance;
	public PopUpUI popUpUI;
	public HomeScreen homeScreen;
	// Use this for initialization

	public void isSwitchScreenPossible(UIscreens screenID){

		switch(screenID){
		case UIscreens.HomeScreenUI:
			homeScreen.gameObject.SetActive(true);
			hudInstance.gameObject.SetActive(false);
			popUpUI.gameObject.SetActive(false);
			break;
		case UIscreens.HUDScreenUI:
			hudInstance.gameObject.SetActive(true);
			homeScreen.gameObject.SetActive(false);
			popUpUI.gameObject.SetActive(false);
			break;
		case UIscreens.PopUpScreenUI:
			hudInstance.gameObject.SetActive(false);
			homeScreen.gameObject.SetActive(false);
			popUpUI.gameObject.SetActive(true);

			break;
		}
	}

}
