using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TimerScript : MonoBehaviour
{
    private float currentTime = 0f;

    public Text text;
    public Image image;

    private bool running = false;

    private Color originalBGColor;
    
    void OnEnable()
    {
        StartPointBehaviour.runStarted += StartTimer;
        EndPointBehaviour.runEnded += StopTimer;
    }

    void OnDisable()
    {
        StartPointBehaviour.runStarted -= StartTimer;
        EndPointBehaviour.runEnded -= StopTimer;
    }

    // Use this for initialization
    void Awake()
    {
        originalBGColor = image.color;
    }
    
    private void StartTimer(float t)
    {
        currentTime = 0;
        running = true;
        StopCoroutine(UpdateTime());
        StartCoroutine(UpdateTime());
        SetBGColor(originalBGColor);
    }

    private void StopTimer(float t)
    {
        running = false;
        SetTime();
        SetBGColor(Color.grey);
    }
    
    private void SetBGColor(Color bGColor)
    {
        bGColor.a = originalBGColor.a;

        image.color = bGColor;
    }

    private IEnumerator UpdateTime()
    {
        float waitTime = Time.timeScale;
        WaitForSeconds timeToWait = new WaitForSeconds(waitTime);
        while (running)
        {
            SetTime();
            currentTime += waitTime;
            yield return timeToWait;
        }
    }

    private void SetTime()
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
