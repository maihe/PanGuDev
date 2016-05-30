using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject btnPrefab;
	public RectTransform panel;
	private float offset;
	private GameObject[] buttons;
	private LevelProperties levelProperties;

	// Runs once and before Starts
	void Awake(){
		DontDestroyOnLoad (levelProperties);
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		FileInfo[] files = findSavedLevel();
		int total = 0;
		foreach (FileInfo f in files) {
			total++;
			Debug.Log (f.FullName);
		
			GameObject objButton = (GameObject)Instantiate (btnPrefab);
			objButton.transform.SetParent (panel, false);
			objButton.transform.localScale = new Vector3 (1, 1, 1);
			offset = total * objButton.GetComponent<RectTransform>().rect.height;

			objButton.transform.position -= new Vector3(0,offset,0);
			//not sure if it will be used yet
			//buttons [i] = objButton;
			objButton.GetComponentInChildren<Text> ().text = f.Name+ " " + f.LastWriteTime;
		}

	}

	private FileInfo[] findSavedLevel(){
		string path;
		//path = Application.dataPath;
		//Debug.Log (Application.streamingAssetsPath);
		path = "C:\\repos\\PanGuDev\\Platformer";


		DirectoryInfo di = new DirectoryInfo (path);
		FileInfo[] fi = di.GetFiles(Constants.SAVED_FILE_PATTERN);

		Debug.Log ("Found "+ fi.Length +" saved files");
		return fi;

	}

	public void openMaker(string levelName){
		Application.LoadLevel ("Maker");
	}

}
