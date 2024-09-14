using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Map
{
   public List<TileMapDetail> lstTileMapDetail { get; set; }

    public Map()
    {
       
    }

    public Map(List<TileMapDetail> lstTileMapDetail)
    { this.lstTileMapDetail = lstTileMapDetail; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int GetLength()
    {
        return lstTileMapDetail.Count;
    }
}
