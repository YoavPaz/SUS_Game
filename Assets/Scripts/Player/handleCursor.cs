using UnityEngine;
using UnityEngine.Tilemaps;

public class handleCursor : MonoBehaviour
{
    public int range = 3;
    public Sprite validCur;
    public Sprite inValidCur;

    private SpriteRenderer cursorSprite;

    public Tilemap tilemap;
    public GameObject cursor;

    void Start()
    {
        cursorSprite = cursor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    void LateUpdate()
    {
        Vector3Int cursorPosition = tilemap.WorldToCell(cursor.transform.position);
        Vector3Int playerPosition = tilemap.WorldToCell(transform.position);

        int distance = Mathf.Abs(cursorPosition.x - playerPosition.x) + Mathf.Abs(cursorPosition.y - playerPosition.y);
        Vector3 snappedPosition = tilemap.GetCellCenterWorld(cursorPosition);

        if (distance <= range) cursorSprite.sprite = validCur;
        else                   cursorSprite.sprite = inValidCur;
        cursor.transform.position = snappedPosition;
    }
}
