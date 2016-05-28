using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;


[CustomEditor(typeof(LevelData)), CanEditMultipleObjects]
public class LevelDataEditor : Editor
{

  // Editing data
  public override void OnInspectorGUI()
  {
    if (Event.current.type == EventType.Layout)
    {
      return;
    }
    
    Rect position = new Rect(0,                             
                             Screen.height - 300, // Accounts for Header //todo - alterei aqui e funcionou antes era 50
                             Screen.width, 
                             Screen.height - 32);
    
    foreach (var item in targets)
    {
      LevelData level = item as LevelData;
      Rect usedRect = InspectExplodeShape(position, level);    
      position.y += usedRect.height;
    }        
  }

  // Editing Single Explosion
  public static Rect InspectExplodeShape(Rect position, LevelData level)
  {
    GUI.changed = false;
    Rect saveOrig = position;    

    // Size
    int newWidth = EditorGUI.IntField(new Rect(position.x, 
                                               50, //todo - alterei aqui antes era position.y
                                               position.width * 0.5f, 
                                               EditorGUIUtility.singleLineHeight), 
                                        "Width", 
                                        level.m_width);
    
    int newHeight = EditorGUI.IntField(new Rect(position.x + position.width * 0.5f,
                                                50, //todo - alterei aqui antes era position.y
                                                position.width * 0.5f,
                                                EditorGUIUtility.singleLineHeight), 
                                         "Height", 
                                         level.m_height);    
    position.y += EditorGUIUtility.singleLineHeight;

    // Resize
    if ((newWidth != level.m_width) || (newHeight != level.m_height))
    {
      int[] newData = new int[newWidth * newHeight];

      for (int x = 0; (x < newWidth) && (x < level.m_width); x++)
        for (int y = 0; (y < newHeight) && (y < level.m_height); y++)
          newData [x + y * newWidth] = level.m_data [x + y * level.m_width];

      level.m_width = newWidth;
      level.m_height = newHeight;
      level.m_data = newData;
    }

    // Setup Block Size and Font    
    float xWidth = Mathf.Max(position.width / Mathf.Max(1, level.m_width),
                             position.height / Mathf.Max(1, level.m_width));
    GUIStyle myFontStyle = new GUIStyle(EditorStyles.textField);
    myFontStyle.fontSize = Mathf.FloorToInt(xWidth * 0.7f);
    
    float offY = 1.2f; //TODO by ???- eu que criei        

    // Edit Blocks
    for (int x = 0; x < level.m_width; x++)
    {
      for (int y = 0; y < level.m_height; y++)
      {
        level.m_data [x + y * level.m_width] = 
          EditorGUI.IntField(new Rect(position.x + xWidth * x,
                                      offY * position.y - xWidth * y, //TODO by ??? - alterei aqui e funcionou
                                      xWidth,
                                      xWidth), 
                             level.m_data [x + y * level.m_width], myFontStyle);
        
      }
    }

    if (GUI.changed)
      EditorUtility.SetDirty(level);
    
    return new Rect(saveOrig.x, saveOrig.y, saveOrig.width, EditorGUIUtility.singleLineHeight + (level.m_height * xWidth));
  }

  // Preview Explosion
  public override bool HasPreviewGUI()
  {
    return true;
  }
  
  public override void OnPreviewGUI(Rect tarRect, GUIStyle background)
  {
    LevelData level = target as LevelData;

    // Get Size
    float blockSize = Mathf.Min(tarRect.width / level.m_width, tarRect.height / level.m_height);
    float offX = (tarRect.width - blockSize * level.m_width) / 2 + tarRect.x;
    float offY = (tarRect.height + blockSize * level.m_height) / 2 + tarRect.y; //todo - alterei aqui e funcionou            

    // Get Max
    int maxExplode = Mathf.Max(level.m_data);
    float maxExDiv = 1.0f / (float)maxExplode;

    // Draw Blocks
    for (int x = 0; x < level.m_width; ++x)
      for (int y = 0; y < level.m_height; ++y)
        if (level.m_data [x + y * level.m_width] > 0)
          EditorGUI.DrawRect(new Rect(offX + x * blockSize + 1, 
                                      offY - y * blockSize + 1, //todo - alterei aqui e funcionou
                                      blockSize - 2, 
                                      blockSize - 2), 
                             new Color(0, 0, 0, level.m_data [x + y * level.m_width] * maxExDiv));      

    }

  // For Static Thumbnails
  public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int TexWidth, int TexHeight)
  {
    Debug.Log("New Static Preview"); //- Use this to see how often it gets called

    LevelData level = target as LevelData;    
    Texture2D staticPreview = new Texture2D(TexWidth, TexHeight);

    // Get Size
    int blockSize = Mathf.FloorToInt(Mathf.Min(TexWidth / level.m_width, TexHeight / level.m_height));
    int offX = (TexWidth - blockSize * level.m_width) / 2;
    int offY = (TexHeight - blockSize * level.m_height) / 2;

    // Get Max
    int maxExplode = Mathf.Max(level.m_data);
    float maxExDiv = 1.0f / (float)maxExplode;
    
    // Blank Slate
    Color blankCol = new Color(0, 0, 0, 0);
    Color[] colBlock = new Color[TexWidth * TexHeight];
    for (int i = 0; i < colBlock.Length; ++i)
      colBlock [i] = blankCol;
    staticPreview.SetPixels(0, 0, TexWidth, TexHeight, colBlock);

    // Draw Blocks
    for (int x = 0; x < level.m_width; ++x)
    {
      for (int y = 0; y < level.m_height; ++y)
      {
        if (level.m_data [x + y * level.m_width] > 0)
        {
          int subX = offX + x * blockSize;
          int subY = TexHeight - (offY + y * blockSize) - blockSize;
          Color blockColour = new Color(0, 0, 0, level.m_data [x + y * level.m_width] * maxExDiv);
          for (int px = 0; px < blockSize; ++px)
            for (int py = 0; py < blockSize; ++py)
              staticPreview.SetPixel(subX + px, subY + py, blockColour);
        }
      }
    }
    
    staticPreview.Apply();
    return staticPreview;
  }
}