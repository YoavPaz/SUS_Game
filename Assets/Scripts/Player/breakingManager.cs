using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[System.Serializable]
public class resorecePresets
{
    public string name;
    public Sprite oreSprite;
    //public int hardness;
    public string dropedResorce;
    public Sprite droppedResorceSprite;
    public string dropAMT;
}

public class breakingManager : MonoBehaviour
{
    public GameObject cursor;
    public Tilemap tilemap;
    public InventoryManager inventory;

    public List<resorecePresets> tileOptions = new List<resorecePresets>();

    public void breakCurrantTile()
    {
        Vector3Int cursorPosition = tilemap.WorldToCell(cursor.transform.position);
        Sprite sprite = tilemap.GetSprite(cursorPosition);
        Debug.Log($"currant sprite: {sprite}");

        foreach (resorecePresets tile in tileOptions)
        {
            if (tile.oreSprite == sprite)
            {
                Debug.Log($"found match: {tile.name}");
                tilemap.SetTile(cursorPosition, null);
                break;
            }
        }
    }
}
