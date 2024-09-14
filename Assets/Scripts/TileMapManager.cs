using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;
    public TileBase tb_pumpkin;
    public TileBase tb_forest;



    private FirebaseDatabaseManager databaseManager;

    private DatabaseReference Reference;
    
    void Start()
    {
        //WriteAllTileMapToFirebase();       
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        if (LoadDataManager.userInGame.MapInGame.lstTileMapDetail == null)
        {
            WriteAllTileMapToFirebase();
        }
        Reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp app = FirebaseApp.DefaultInstance;

        LoadMapForUser();
    }

    // Update is called once per frame
    public void WriteAllTileMapToFirebase()
    {
        List<TileMapDetail> tileMaps = new List<TileMapDetail> ();
        for(int x = tm_Ground.cellBounds.min.x; x < tm_Ground.cellBounds.max.x; x++)
        {
            for(int y = tm_Ground.cellBounds.min.y; y < tm_Ground.cellBounds.max.y; y++)
            {
                TileMapDetail tm_detail = new TileMapDetail(x, y, TileMapState.Grass);
                tileMaps.Add(tm_detail);
            } 
                
        }  
        LoadDataManager.userInGame.MapInGame = new Map(tileMaps);
        databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId,LoadDataManager.userInGame.ToString());
    }    

    public void LoadMapForUser()
    {
        MapToUI(LoadDataManager.userInGame.MapInGame);
    }    

    public void TileMapDetailToTileBase(TileMapDetail tileMapDetail)
    {
        Vector3Int cellPos = new Vector3Int(tileMapDetail.x, tileMapDetail.y, 0);
        if(tileMapDetail.tileMapState == TileMapState.Ground)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Forest.SetTile(cellPos, null);
        }    
        else if (tileMapDetail.tileMapState == TileMapState.Grass)
        {
            tm_Forest.SetTile(cellPos, null);
        }    
        else if (tileMapDetail.tileMapState == TileMapState.Forest)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Forest.SetTile(cellPos, tb_forest);
        }   
        else if (tileMapDetail.tileMapState == TileMapState.Pumpkin)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Forest.SetTile(cellPos, tb_pumpkin);
        }    
    }    


    public void MapToUI( Map map )
    {
        for(int i = 0; i < map.GetLength(); i ++)
        {
            Debug.Log(i);
            TileMapDetailToTileBase(map.lstTileMapDetail[i]);
        }    
    }    

    public void SetStateForTileMapDetail(int x, int y, TileMapState state)
    {
        for(int i = 0; i < LoadDataManager.userInGame.MapInGame.GetLength();i ++)
        {
           if (LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].x == x && LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].y == y)
            {
                LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].tileMapState = state;
                databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());

            }    

        }    
    }    
}
 