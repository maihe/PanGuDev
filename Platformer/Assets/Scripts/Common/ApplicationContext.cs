using UnityEngine;
using System.Collections;

public class ApplicationContext : MonoBehaviour{

	private string levelToLoad;

	public static ApplicationContext Instance;

	void Awake (){
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	public string getLevelToLoad (){
		return levelToLoad;
	}

	public void setLevelToLoad(string levelName){
		this.levelToLoad = levelName;
	}

}
