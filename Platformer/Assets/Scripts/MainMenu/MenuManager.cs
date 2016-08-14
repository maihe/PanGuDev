using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public bool hidePanel = false;
	public GameObject btnPrefab;
	public RectTransform panelLevelsDinamico;
	public RectTransform panelLevelFixo;
	private float offset;
	private GameObject[] buttons;

	// Runs once and before Starts
	void Awake(){
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		
		//fillDinamicPanel ();
		fillFixedPanel ();
	}


	private void fillDinamicPanel(){
		FileInfo[] files = findSavedLevel();
		int total = 0;
		foreach (FileInfo f in files) {
			total++;
			Debug.Log (f.FullName);

			GameObject objButton = (GameObject)Instantiate (btnPrefab);
			objButton.transform.SetParent (panelLevelsDinamico, false);
			objButton.transform.localScale = new Vector3 (1, 1, 1);
			offset = total * objButton.GetComponent<RectTransform>().rect.height;

			objButton.transform.position -= new Vector3(0,offset,0);
			//not sure if it will be used yet
			//buttons [i] = objButton;
			objButton.GetComponentInChildren<Text> ().text = f.Name+ " " + f.LastWriteTime;
			objButton.GetComponentInChildren<ButtonController> ().setLevelName (f.Name);
		}
	}


	private void fillFixedPanel(){
		FileInfo[] files = findSavedLevel();
		int total = 0;
		Button[] btns = panelLevelFixo.GetComponentsInChildren<Button> ();
		foreach (Button b in btns) {
			if (total < files.Length) {
				b.GetComponentInChildren<Text> ().text = files[total].Name+ " " + files[total].LastWriteTime;
			} else {
				b.GetComponentInChildren<Text> ().text = "Empty";
			}
			total++;
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

}
