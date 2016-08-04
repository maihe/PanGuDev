using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour {

	private string levelName;

	//Set up level to be loaded
	public void SetLevel(){
		Debug.Log ("CLick");

		//ApplicationContext.Instance.setLevelToLoad(btnName.Split('_')[1]);
		ApplicationContext.Instance.setLevelToLoad(levelName);
		SceneManager.LoadScene("Maker");

		//Application.LoadLevel("Maker");
	}

	public string getLevelName(){
		return levelName;
	}
	public void setLevelName(string name){
		Debug.Log ("set name "+name);
		this.levelName = name;
	}

}
