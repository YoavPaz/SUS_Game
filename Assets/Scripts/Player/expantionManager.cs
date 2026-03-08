using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class expantionManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Tilemap tilemap;
    public TileBase tile;

    [Header("Square size")]
    public int width = 10;
    public int height = 10;
    public int startingBoxWidth = 10;
    public int startingBoxHeight = 10;

    void Start()
    {
        if (tilemap == null || tile == null)
        {
            Debug.LogError("Tilemap or Tile not assigned!");
            return;
        }

        GenerateTileSquare();
    }

    void GenerateTileSquare()
    {
        for (int x = width / -2; x < width / 2; x++)
        {
            for (int y = height / -2; y < height / 2; y++)
            {
                if (x >= startingBoxWidth / -2 && x < startingBoxWidth / 2 && y >= startingBoxHeight / -2 && y < startingBoxHeight / 2)
                    continue;
                Vector3Int pos = new Vector3Int(x, y, 0);
                tilemap.SetTile(pos, tile);
            }
        }
    }
}