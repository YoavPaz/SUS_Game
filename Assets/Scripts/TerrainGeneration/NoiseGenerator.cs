using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[Serializable]
public class PropTile
{
    public TileBase tileBase;
    public int chance;
}

public class NoiseGenerator : MonoBehaviour
{
    [Header("Box Settings")]
    public int widthPerBox = 32;
    public int heightPerBox = 32;
    public float scale = 1f;
    public int BoxAmount = 4;
    
    private int XOffset;
    private int YOffset;
    
    [Header("Tile Maps")]
    public Tilemap tilemap;
    public Tilemap PropTileMap;

    public TileBase waterTile;
    public TileBase grassTile;
    
    public List<PropTile> PropTiles;
    
    private Texture2D map;

    private int width;
    private int height;

    private void Start()
    {
        width = widthPerBox * BoxAmount;
        height = heightPerBox * BoxAmount;
        
        tilemap.ClearAllTiles();
        PropTileMap.ClearAllTiles();
        tilemap.transform.position = new Vector2(-width / 2, -height / 2);
        PropTileMap.transform.position = tilemap.transform.position;
        
        generateRandomOffset();
        Generate2DTexure();
    }

    void generateRandomOffset()
    {
        XOffset = UnityEngine.Random.Range(0, 1000000);
        YOffset = UnityEngine.Random.Range(0, 1000000);
    }

    void Generate2DTexure()
    {
        map = new Texture2D(width, height);
        map.filterMode = FilterMode.Point;
        map.wrapMode = TextureWrapMode.Clamp;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + XOffset;
                float yCoord = (float)y / height * scale + YOffset;

                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                TileBase chosenTile = null; // the chosen one, to defeat the noise lord.
                
                if (noise < 0.6f) chosenTile = grassTile;
                else if (noise >= 0.6f) chosenTile = waterTile;
                
                Color color = new Color(noise, noise, noise);
                map.SetPixel(x, y, color);
                
                if (chosenTile == null)
                {
                    Debug.LogError($"[Map Gen] could not find a tile to generate at x:{x} y:{y}");
                    continue;
                }
                
                tilemap.SetTile(new Vector3Int(x, y, 0), chosenTile);

                foreach (PropTile propTile in PropTiles)
                {
                    int chance = UnityEngine.Random.Range(0, 1000);
                    if (chance > propTile.chance || noise >= 0.6f) continue;
                    
                    PropTileMap.SetTile(new Vector3Int(x, y, 0), propTile.tileBase);
                    break;
                }
            }
        }

        map.Apply();

        GetComponent<SpriteRenderer>().sprite =
            Sprite.Create(map, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1f);
    }
}
