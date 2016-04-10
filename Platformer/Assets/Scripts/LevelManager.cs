﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour  {

    public LevelData levelData;    
    public GridManager gridManager;

    public void Start()
    {
        Load();
    }

    public void Save() {        

        //zerando as posicoes
        //por enquanto esta porco, pois faco dois laços, mas como no projeto final tera o griditem.cs cuidando de cada posicao nao tera este problema        
        for (int i = 0; i < levelData.m_data.Length; i++)
        {
            levelData.m_data[i] = 0;
        }
        
        foreach (var tile in gridManager.items)
        {
            if(tile != null) {             
                levelData.m_data[
                    Convert.ToInt32(tile.transform.position.x) +
                    Convert.ToInt32(tile.transform.position.y) * levelData.m_width
                ] = Convert.ToInt32(tile.name.Substring(0, 1));
            }
        }

        SaveSerialize();
    }

    public void SaveSerialize()
    {
        LevelDataSerialize levelDataSerialize = new LevelDataSerialize();
        levelDataSerialize.m_height = levelData.m_height;
        levelDataSerialize.m_width = levelData.m_width;
        levelDataSerialize.m_data = levelData.m_data;

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream("SavedLevel", FileMode.Create))
        {
            formatter.Serialize(stream, levelDataSerialize);
        } 
    }

    public void LoadSerialize()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists("SavedLevel"))
        {
            using (FileStream stream = new FileStream("SavedLevel", FileMode.Open))
            {
                var levelDataSerialize = formatter.Deserialize(stream) as LevelDataSerialize;
                levelData.m_height = levelDataSerialize.m_height;
                levelData.m_width = levelDataSerialize.m_width;
                levelData.m_data = levelDataSerialize.m_data;
            }
        }        
    }   

    public void Load()
    {
        //limpar para carregar corretamente
        DestroyLevel();
        gridManager.InitializeItems();

        //Carregando dados do arquivo salvo em disco
        LoadSerialize();

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