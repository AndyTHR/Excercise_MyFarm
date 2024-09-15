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
    public List<TileBase> lstb_RiceTree;
    public List<TileBase> lstb_CanHarvest;
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
                //tm_Forest.SetTile(cellPos, tb_Forest);
                StartCoroutine(GrowPlant(cellPos, tm_Forest, lstb_RiceTree));
                mapManager.SetStateForTileMapDetail(cellPos.x, cellPos.y, TileMapState.Forest);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            Debug.Log("Cell Pos" + cellPos);

            TileBase crrTileBase = tm_Forest.GetTile(cellPos);
            if(crrTileBase == lstb_RiceTree[4])
            {
                tm_Grass.SetTile(cellPos, tb_Grass);
                tm_Forest.SetTile(cellPos, null);

                InvenItems Item_Ricetree = new InvenItems();
                Item_Ricetree.name = "Bi do";
                Item_Ricetree.description = "Bi do an rat ngon";

                recyclableInventory.AddInventoryItem(Item_Ricetree);
                mapManager.SetStateForTileMapDetail(cellPos.x, cellPos.y, TileMapState.Pumpkin);
            }    
        }
    }    
    public IEnumerator GrowPlant(Vector3Int cellpos, Tilemap tilemap, List<TileBase> lsttilebase)
    {
        int crrstage = 0;
        while (crrstage < lsttilebase.Count)
        {
            tilemap.SetTile(cellpos, lsttilebase[crrstage]);
            yield return new WaitForSeconds(5);
            crrstage++;
        }    
    }    
}
