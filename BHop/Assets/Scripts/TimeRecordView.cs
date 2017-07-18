using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRecordView : MonoBehaviour {

    public Text positionText, timeText;
    public Image positionBG, timeBG;

    public Color newRecordColor = Color.black;

    private Color originalBGColor;

    void Awake()
    {
        originalBGColor = positionBG.color;
    }

    public void setRecord(TimeRecord record)
    {
        setPosition(record.position);
        setTime(record.time);

        setBGColor(originalBGColor);
    }
    
    void setPosition(int position)
    {
        positionText.text = position.ToString();
    }

    void setTime(float time)
    {
        DateTime dateTime = (new DateTime(1970, 1, 1)).AddSeconds(time);
        timeText.text = dateTime.ToString("mm:ss:fff");
    }

    public void setAsNewRecord()
    {
        setBGColor(newRecordColor);
    }

    private void setBGColor(Color bGColor)
    { 
        bGColor.a = originalBGColor.a;

        positionBG.color = bGColor;
        timeBG.color = bGColor;
    }
}
