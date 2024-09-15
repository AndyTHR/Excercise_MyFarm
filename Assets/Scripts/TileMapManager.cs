using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;
    public List<TileBase> lstb_pumpkin;
    public TileBase tb_forest;
    public PlayerFarmController playerFarmController;

    private FirebaseDatabaseManager databaseManager;
    private DatabaseReference Reference;

    void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();

        // Kiểm tra nếu map chưa được khởi tạo, thì tạo mới
        if (LoadDataManager.userInGame.MapInGame == null || LoadDataManager.userInGame.MapInGame.lstTileMapDetail == null)
        {
            WriteAllTileMapToFirebase();
        }

        Reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp app = FirebaseApp.DefaultInstance;

        LoadMapForUser();
    }

    public void WriteAllTileMapToFirebase()
    {
        List<TileMapDetail> tileMaps = new List<TileMapDetail>();
        for (int x = tm_Ground.cellBounds.min.x; x < tm_Ground.cellBounds.max.x; x++)
        {
            for (int y = tm_Ground.cellBounds.min.y; y < tm_Ground.cellBounds.max.y; y++)
            {
                TileMapDetail tm_detail = new TileMapDetail(x, y, TileMapState.Grass, DateTime.Now);
                tileMaps.Add(tm_detail);
            }
        }

        LoadDataManager.userInGame.MapInGame = new Map(tileMaps);
        databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
    }

    public void LoadMapForUser()
    {
        MapToUI(LoadDataManager.userInGame.MapInGame);
    }

    public void TileMapDetailToTileBase(TileMapDetail tileMapDetail)
    {
        Vector3Int cellPos = new Vector3Int(tileMapDetail.x, tileMapDetail.y, 0);

        if (tileMapDetail.tileMapState == TileMapState.Ground)
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
            double elapsedTime = DateTime.Now.Subtract(tileMapDetail.GrowTime).TotalSeconds;
            tm_Grass.SetTile(cellPos, null);

            if (elapsedTime > 20)
            {
                tm_Forest.SetTile(cellPos, lstb_pumpkin[4]);
            }
            else if (elapsedTime > 15)
            {
                tm_Forest.SetTile(cellPos, lstb_pumpkin[3]);
                playerFarmController.StartCoroutine(playerFarmController.GrowPlant(cellPos, tm_Forest, lstb_pumpkin.GetRange(3, 2)));
            }
            else if (elapsedTime > 10)
            {
                tm_Forest.SetTile(cellPos, lstb_pumpkin[2]);
            }
            else if (elapsedTime > 5)
            {
                tm_Forest.SetTile(cellPos, lstb_pumpkin[1]);
            }
            else
            {
                tm_Forest.SetTile(cellPos, lstb_pumpkin[0]);
            }
        }
    }
    public void MapToUI(Map map)
    {
        for (int i = 0; i < map.GetLength(); i++)
        {
            TileMapDetailToTileBase(map.lstTileMapDetail[i]);
        }
    }

    public void SetStateForTileMapDetail(int x, int y, TileMapState state)
    {
        for (int i = 0; i < LoadDataManager.userInGame.MapInGame.GetLength(); i++)
        {
            if (LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].x == x &&
                LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].y == y)
            {
                LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].tileMapState = state;
                LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].GrowTime = DateTime.Now;
                databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
            }
        }
    }
}
