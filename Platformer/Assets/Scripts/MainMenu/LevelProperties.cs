using UnityEngine;
using System.Collections;

public class LevelProperties : MonoBehaviour{

	private string levelName;

	public string getLevelName(){
		return levelName;
	}

	public void setLevelName(string levelName){
		this.levelName = levelName;
	}

}
