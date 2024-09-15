using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNightManager : MonoBehaviour
{
    public Text textTimeInGame;
    public float dayDuaration = 5f;

    private void Update()
    {
        DateTime realtime = DateTime.Now;
        float realSecondInDay = (realtime.Hour * 3600) + (realtime.Minute * 60) + realtime.Second;
        float gameTimeSeconds = (realSecondInDay/ (24*3600)) * (dayDuaration * 60);
        int gameHour = Mathf.FloorToInt(gameTimeSeconds / (dayDuaration * 60) * 24);
        int gameMinutes = Mathf.FloorToInt((gameTimeSeconds / (dayDuaration * 60) * 24 * 60) % 60);
        string timeformatted = string.Format("{0:00}:{ 1:00}", gameHour, gameMinutes);
        textTimeInGame.text = timeformatted;
    }

}
