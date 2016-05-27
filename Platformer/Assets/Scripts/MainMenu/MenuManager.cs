using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public LevelData[] levels;
	public LevelData selectedLevel;
	public Button[] Buttons;
	public GameObject btnPrefab;
	public RectTransform panel;
	float offset;


	// Use this for initialization
	void Start () {
		int totalFiles = findSavedLevel();
		//offset = btnPrefab.transform.
		for(int i = 0; i < totalFiles; i++){
			GameObject objButton = (GameObject)Instantiate (btnPrefab);
			objButton.transform.SetParent (panel, false);
			objButton.transform.localScale = new Vector3 (1, 1, 1);
			offset = i * objButton.GetComponent<RectTransform>().rect.height;

			objButton.transform.position -= new Vector3(0,offset,0);
			Button tmpButton = objButton.GetComponent<Button> ();

			int tmp = i;
			tmpButton.onClick.AddListener (() => ButtonClicked (tmp));
		}

	}

	private int findSavedLevel(){
		int total = 0;
		string path;
		//path = Application.dataPath;
		//Debug.Log (Application.streamingAssetsPath);
		path = "C:\\repos\\PanGuDev\\Platformer";
		//Debug.Log (path);

		DirectoryInfo di = new DirectoryInfo (path);
		FileInfo[] fi = di.GetFiles("SavedLevel*.*");
		foreach (FileInfo  f in fi) {
			total++;
			Debug.Log (f.FullName);
		}
		Debug.Log (total);
		return total;

	}

	void ButtonClicked(int btnN){
		Debug.Log ("Button clicked = " + btnN);
	}


	// Update is called once per frame
	void Update () {
	
	}


}
