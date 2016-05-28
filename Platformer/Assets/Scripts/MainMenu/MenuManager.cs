using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private GameObject[] buttons;
	public GameObject btnPrefab;
	public RectTransform panel;
	float offset;


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


			//Button tmpButton = objButton.GetComponent<Button> ();
			//int tmp = i;
			//tmpButton.onClick.AddListener (() => ButtonClicked (tmp));
		}

	}

	private FileInfo[] findSavedLevel(){
		string path;
		//path = Application.dataPath;
		//Debug.Log (Application.streamingAssetsPath);
		path = "C:\\repos\\PanGuDev\\Platformer";
		//Debug.Log (path);

		DirectoryInfo di = new DirectoryInfo (path);
		FileInfo[] fi = di.GetFiles("SavedLevel*.*");

		Debug.Log ("Found "+ fi.Length +" saved files");
		return fi;

	}

	void ButtonClicked(int btnN){
		Debug.Log ("Button clicked = " + btnN);
	}


	// Update is called once per frame
	void Update () {
	
	}


}
