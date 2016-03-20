using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Draws grid lines on the terrain.
/// </summary>
[AddComponentMenu("Level Editor/Drawers/Grid Lines")]
public class GridLines : MonoBehaviour, IDisposable
{
    /// <summary>The line material.</summary>
    public Material lineMaterial;

    /// <summary>The main color.</summary>
    public Color mainColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    /// <summary>The scale of grid.</summary>
    public float scale = 1;

    /// <summary>The grid lines.</summary>    
    public Vector2 gridLines = Vector2.zero;

    /// <summary>The offset the grid.</summary>
    public float offset = 0.5f;

    /// <summary>When disposing the grid, the delay on each step of the fade out effect.</summary>
    public float fadeOutDelay = 0.05f;

    /// <summary>When disposing the grid, the step of the fade out effect.</summary>
    public float fadeOutStep = 0.1f;

    public void OnPostRender()
    {        
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        //GL.LoadOrtho();//se comentar esta linha nao fica fixo na camera, fica no mundo real                
        GL.Begin(GL.LINES);
        GL.Color(mainColor);

        //Lines on X axis.
        for (int index = 0; index < Mathf.FloorToInt(gridLines.x); index++)
        {
            var positionX = index * scale - offset;
            GL.Vertex3(positionX, 0 - offset, 0);
            GL.Vertex3(positionX, Screen.height, 0);                        
        }

        //Lines on Y axis.
        for (int index = 0; index < Mathf.FloorToInt(gridLines.y); index++)
        {
            var positionY = index * scale - offset;
            GL.Vertex3(0 - offset, positionY, 0);
            GL.Vertex3(Screen.width, positionY, 0);
        }

        GL.End();
        GL.PopMatrix();

    }

    /// <summary>
    /// Releases all resource used by the this object.
    /// </summary>
    public void Dispose()
    {
        this.StartCoroutine(this.DisposeGrid());
    }

    /// <summary>
    /// Disposes the grid.
    /// </summary>
    protected IEnumerator DisposeGrid()
    {
        while (this.mainColor.a > 0)
        {
            this.mainColor.a -= this.fadeOutStep;

            yield return new WaitForSeconds(this.fadeOutDelay);
        }

        Destroy(this);
    }
}
 