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
    }

}
