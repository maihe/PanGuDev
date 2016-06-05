using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour  {

    public LevelData levelData;    
    public GridManager gridManager;
	public String dataFileName = "SavedLevel";

	public void Awake(){
		dataFileName = ApplicationContext.Instance.getLevelToLoad ();
		Debug.Log ("Save loaded " + dataFileName);
	}

    public void Start()
    {
        Load();
    }

    public void Save() {        

        //TODO by maiara - abrir popup, listar - reutilizar panel do menu principal
		cleanSerializableObject ();

		saveLevelData ();

        SaveSerialize();
    }

	private void cleanSerializableObject(){
		try{
			//zerando as posicoes
			//por enquanto esta porco, pois faco dois laços, 
			//mas como no projeto final tera o griditem.cs cuidando de cada posicao nao tera este problema
			//for do item level sendo salvo em objeto "Serializavel"
			for (int i = 0; i < levelData.m_data.Length; i++)
			{
				levelData.m_data[i] = 0;
			}

		}catch(Exception e){
			Debug.Log ("[LevelManager] " + e.ToString());
		}
	}

	private void saveLevelData(){
		//for do array de digitos
		foreach (var tile in gridManager.items)
		{
			if(tile != null) {             
				levelData.m_data[
					Convert.ToInt32(tile.transform.position.x) + 
					(Convert.ToInt32(tile.transform.position.y) * levelData.m_width)
				] = Convert.ToInt32(tile.name.Substring(0, 1));
			} //e se for?
		}
	}
    public void SaveSerialize()
    {
        LevelDataSerialize levelDataSerialize = new LevelDataSerialize();
        levelDataSerialize.m_height = levelData.m_height;
        levelDataSerialize.m_width = levelData.m_width;
        levelDataSerialize.m_data = levelData.m_data;

        BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream stream = new FileStream(dataFileName, FileMode.Create))
        {
            formatter.Serialize(stream, levelDataSerialize);
        } 
    }

	//Load from Disc
    public void LoadSerialize()
    {
        BinaryFormatter formatter = new BinaryFormatter();
		if (File.Exists (dataFileName)) {
			using (FileStream stream = new FileStream (dataFileName, FileMode.Open)) {
				var levelDataSerialize = formatter.Deserialize (stream) as LevelDataSerialize;
				levelData.m_height = levelDataSerialize.m_height;
				levelData.m_width = levelDataSerialize.m_width;
				levelData.m_data = levelDataSerialize.m_data;
			}
		} else {
			Debug.Log ("Saved File not found");
		}
    }   

    public void Load()
    {
        //limpar para carregar corretamente
        DestroyLevel();
		gridManager.InitializeItems(levelData.m_width, levelData.m_height);

        //Carregando dados do arquivo salvo em disco
        LoadSerialize();

		//Poe o serializado no GrigManager
        for (int x = 0; x < levelData.m_width; x++)
        {
            for (int y = 0; y < levelData.m_height; y++)
            {
                var numberPrefab = levelData.m_data[x + y * levelData.m_width].ToString();
                if (numberPrefab != "0")
                {   
                    var tile = (GameObject)Instantiate(Resources.Load<GameObject>("Tiles/" + numberPrefab), new Vector2(x, y), Quaternion.identity);
                    gridManager.items[x, y] = tile;
                }
            }
        }        
    }


	//Destroi tile por tile da Cena
    public void DestroyLevel()
    {
        foreach (var tile in gridManager.items)
        {
            if (tile != null)
                Destroy(tile);
        }
    }

    public void FecharGame()
    {        
        Application.Quit();
    }

}
