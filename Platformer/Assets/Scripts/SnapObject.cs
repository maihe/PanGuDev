using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapObject : MonoBehaviour
{
    public Vector3 lookAtPoint = Vector3.zero;
    public Vector2 roomSize = Vector2.zero;
    public Quaternion rot = Quaternion.identity;
    public float scale = 1f;
    void Update()
    {
        roomSize = new Vector2(Mathf.FloorToInt(roomSize.x), Mathf.FloorToInt(roomSize.y));
        foreach (Transform t in transform)
        {
            if (t.tag == "LevelTile")
            {
                t.name = "GridElement";
                Vector3 position = t.localPosition;
                position = new Vector3(
                    //Mathf.Clamp (Mathf.Round(position.x/scale)*scale, 0, roomSize.x),
                    Mathf.Round(position.x / scale) * scale,
                    Mathf.Round(position.y / scale) * scale,
                    //Mathf.Clamp (Mathf.Round(position.y/scale)*scale, 0, roomSize.y),
                    position.z
                    );
                t.localPosition = position;
                t.localScale = new Vector3(scale, scale, scale);
                //t.localScale = Vector3.one*scale;
               //if (t.GetComponent<GridElement>()) t.GetComponent<GridElement>().SetObject();
            }
        }
    }

    public void NewGridElement()
    {
        GameObject gridEl = (GameObject)Resources.Load("GridElement");
        GameObject instanceElement = (GameObject)Instantiate(gridEl, transform.position, Quaternion.identity);
        instanceElement.transform.parent = gameObject.transform;
        instanceElement.name = "GridElement";
        instanceElement.transform.localPosition = Vector3.zero;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int h = 0; h < Mathf.FloorToInt(roomSize.x); h++)
        {
            for (int w = 0; w < Mathf.FloorToInt(roomSize.y); w++)
            {
                Vector3 nextCube = new Vector3(transform.position.x + (w * scale), transform.position.y + (h * scale), transform.position.z);
                Gizmos.DrawWireCube(nextCube, Vector3.one * scale);                
            }
        }
    }
}