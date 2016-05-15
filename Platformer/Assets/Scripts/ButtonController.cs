using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Opens the selected level
	public void OpenGame(){
		Application.LoadLevel("Maker");
	}
}
