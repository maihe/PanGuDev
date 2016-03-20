using UnityEngine;
using System.Collections;

public class teste : MonoBehaviour {   

    public Transform tilePrefab;
    public float scale = 1f;
    private GameObject[,] items;
    public int xSize, ySize;

    void Start()
    {
        items = new GameObject[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                items[x, y] = new GameObject();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            InstantiateTile();
        }
        else if (Input.GetMouseButton(1))
        {            
            Destroy(ClickSelect());

            //teste para armazenar os objetos para futuramente salvar
            foreach (var item in items)
            {
                Destroy(item);
            }
        }
    }

    //This method returns the game object that was clicked using Raycast 2D
    GameObject ClickSelect()
    {
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit)
        {
            Debug.Log(hit.transform.name);
            return hit.transform.gameObject;
        }
        else return null;    
    }

    protected void InstantiateTile()
    {
        if (ClickSelect() != null) return;

        var tile = Instantiate(tilePrefab);
        var tileTransform = tile.GetComponent<Transform>();

        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position = new Vector3(
            Mathf.Round(position.x / scale) * scale,
            Mathf.Round(position.y / scale) * scale
        );

        tileTransform.position = position;

        //teste para armazenar os objetos para futuramente salvar
        
        var x = (int)position.x;
        var y = (int)position.y;

        print("X: " + x);
        print("Y: " + y);
        items[(int)position.x, (int)position.y] = tile.gameObject;
        
    }
}
