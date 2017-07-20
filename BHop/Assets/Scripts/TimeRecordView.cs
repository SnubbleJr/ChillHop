using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRecordView : MonoBehaviour {

    public Text PositionText, TimeText;
    public Image PositionBg, TimeBg;

    public Color NewRecordColor = Color.black;

    private Color originalBgColor;

    private void Awake()
    {
        originalBgColor = PositionBg.color;
    }

    public void SetRecord(TimeRecord record)
    {
        SetPosition(record.Position);
        SetTime(record.Time);

        SetBgColor(originalBgColor);
    }
    
    private void SetPosition(int position)
    {
        PositionText.text = position.ToString();
    }

    private void SetTime(float time)
    {
        DateTime dateTime = (new DateTime(1970, 1, 1)).AddSeconds(time);
        TimeText.text = dateTime.ToString("mm:ss:fff");
    }

    public void SetAsNewRecord()
    {
        SetBgColor(NewRecordColor);
    }

    private void SetBgColor(Color bGColor)
    { 
        bGColor.a = originalBgColor.a;

        PositionBg.color = bGColor;
        TimeBg.color = bGColor;
    }
}
