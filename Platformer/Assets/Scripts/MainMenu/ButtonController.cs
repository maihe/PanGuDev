using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour {

	string btnName;

	// Use this for initialization
	void Start () {
		btnName = gameObject.GetComponent<Text> ().text;
	}

	//Set up level to be loaded
	public void SetLevel(){
		GameObject.FindObjectOfType<LevelProperties>().setLevelName(btnName.Split('_')[1]);
		GameObject.FindObjectOfType<MenuManager> ().openMaker (btnName.Split('_')[1]);
		//Application.LoadLevel("Maker");
	}
}
