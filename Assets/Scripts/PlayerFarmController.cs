using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFarmController : MonoBehaviour
{
    [Header("Tile Map")]
    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;

    [Header("Tile Base")]
    public TileBase tb_Ground;
    public TileBase tb_Grass;
    public TileBase tb_Forest;

    private RecyclableInventory recyclableInventory;
    
    public TileMapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        recyclableInventory = FindObjectOfType<RecyclableInventory>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandleFarmACtion();
    }

    public void HandleFarmACtion()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            Debug.Log("Cell Pos" + cellPos);

            TileBase crrTileBase = tm_Grass.GetTile(cellPos);

            if(crrTileBase == tb_Grass)
            {
                Debug.Log("Dao");
                tm_Grass.SetTile(cellPos, null);
                mapManager.SetStateForTileMapDetail(cellPos.x, cellPos.y, TileMapState.Ground);
            }    
        }    
        if(Input.GetKeyDown(KeyCode.V))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            Debug.Log("Cell Pos" + cellPos);

            TileBase crrTileBase = tm_Grass.GetTile(cellPos);
            if(crrTileBase == null)
            {
                tm_Forest.SetTile(cellPos, tb_Forest);
                mapManager.SetStateForTileMapDetail(cellPos.x, cellPos.y, TileMapState.Forest);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            Debug.Log("Cell Pos" + cellPos);

            TileBase crrTileBase = tm_Forest.GetTile(cellPos);
            if(crrTileBase != null)
            {
                tm_Grass.SetTile(cellPos, tb_Grass);
                tm_Forest.SetTile(cellPos, null);

                InvenItems itemFlower = new InvenItems();
                itemFlower.name = "Hoa 1 gio";
                itemFlower.description = "Hoa trang tri";

                Debug.Log(itemFlower.ToString());
                recyclableInventory.AddInventoryItem(itemFlower);
                mapManager.SetStateForTileMapDetail(cellPos.x, cellPos.y, TileMapState.Grass);
            }    
        }
    }    
}
