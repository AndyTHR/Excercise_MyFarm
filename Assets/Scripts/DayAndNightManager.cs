using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayAndNightManager : MonoBehaviour
{
    public Text textTimeInGame;
    public float dayMultiplier = 20;
    public Light2D Light2D;
    public Gradient gradient;

    private void Update()
    {
        DateTime realtime = DateTime.Now;
        float realSecondInDay = (realtime.Hour * 3600) + (realtime.Minute * 60) + realtime.Second;
        realSecondInDay = (realSecondInDay * dayMultiplier) % 86400;
        //float gameTimeSeconds = (realSecondInDay/ (24*3600)) * (dayDuaration * 60);
        int gameHour = Mathf.FloorToInt(realSecondInDay / 3600);
        int gameMinutes = Mathf.FloorToInt((realSecondInDay * 60)/ 60);
        string timeformatted = string.Format($"{0:00}:{ 1:00}", gameHour, gameMinutes);
        textTimeInGame.text = timeformatted;

        ChangeColorByTime(realSecondInDay);
    }

    public void ChangeColorByTime(float seconds)
    {
        Light2D.color = gradient.Evaluate(seconds / 86400);
    }

}
