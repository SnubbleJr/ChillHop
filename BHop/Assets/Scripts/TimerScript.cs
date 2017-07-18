using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public
class TimerScript : MonoBehaviour
{
    public delegate void TimeDelegate(int val);
    public static event TimeDelegate currTime;

    private float currentTime = 0;

    private Text text;
    private Image image;

    private bool running = false;

    private Color originalBGColor;
    
    void OnEnable()
    {
        StartPointBehaviour.runStarted += resetTimer;
        EndPointBehaviour.runEnded += stopTimer;
    }

    void OnDisable()
    {
        StartPointBehaviour.runStarted -= resetTimer;
        EndPointBehaviour.runEnded -= stopTimer;
    }

    // Use this for initialization
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        originalBGColor = image.color;
        running = true;
        setTime();
    }

    void resetTimer(float t)
    {
        currentTime = 0;
        running = true;
        setBGColor(originalBGColor);
    }

    void stopTimer(float t)
    {
        running = false;
        setBGColor(Color.grey);
    }
    
    private void setBGColor(Color bGColor)
    {
        bGColor.a = originalBGColor.a;

        image.color = bGColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
            return;

        currentTime += Time.deltaTime;

        setTime();
    }

    public void StartT()
    {
        running = true;
    }

    public void EndT()
    {
        setTime();
        running = false;
    }

    void setTime()
    {
        string str = "";

        int minComp = (int)(currentTime / 60);
        str += minComp.ToString() + ":";
        int secComp = (int)(currentTime - (60 * minComp));
        if (secComp < 10)
            str += "0";
        str += secComp.ToString();

        text.text = str;
    }
}
