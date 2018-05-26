using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/ChangeMouseCursor")]
public class ChangeMouseCursor : OnClickLogic 
{
    public List<Texture2D> CursorImages = new List<Texture2D>();

    public override void RunClickLogic()
    {
        Cursor.SetCursor(CursorImages[Random.Range(0, CursorImages.Count - 1)], Vector2.zero, CursorMode.Auto);
    }
}
