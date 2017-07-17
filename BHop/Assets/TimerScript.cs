using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public
class TimerScript : MonoBehaviour {

public
  delegate void TimeDelegate(int val);
public
  static event TimeDelegate currTime;

private
  float currentTime = 0;

private
  Text text;

private
  bool start = false;

  // Use this for initialization
  void Awake() {
    text = GetComponent<Text>();
    start = true;
    setTime();
  }

  // Update is called once per frame
  void Update() {
    if (!start)
      return;

    currentTime += Time.deltaTime;

    setTime();
  }

public
  void StartT() { start = true; }

public
  void EndT() {
    setTime();
    start = false;
  }

  void setTime() {
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
