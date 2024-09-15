using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public enum TileMapState
{
    Ground,
    Grass,
    Forest,
    Pumpkin,
    Grapefruit
}    
public class TileMapDetail
{
    public int x { get; set; }
    public int y { get; set; } 

    public TileMapState tileMapState { get; set; }
    public DateTime GrowTime { get; set; }
    public TileMapDetail()
    {

    }

    public TileMapDetail(int x, int y, TileMapState tileMapState, DateTime GrowTime)
    {
        this.x = x;
        this.y = y;
        this.tileMapState = tileMapState;
        this.GrowTime = GrowTime;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
