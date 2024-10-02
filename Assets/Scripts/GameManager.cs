using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject sightCam;
    public GameObject player;
    bool[] hasKey = new bool[5] { false, false, false, false, false };
    bool[] hasEvident = new bool[4] { false, false, false, false };
    int battery;
    public GameObject TextUI;
    public GameObject IconUI;
    public GameObject[] Doors;
    public GameObject IntroUI, OutroUI;
    bool isBegin = true;

    void Start()
    {
        Intro();
    }

    void Update()
    {

    }

    public void Intro()
    {
        IntroUI.SendMessage("Play");
    }

    public void Begin()
    {
        isBegin = true;
        player.SendMessage("Gogogo");
    }

    public void GameOver()
    {
        player.SendMessage("End");
        OutroUI.SetActive(true);
        OutroUI.SendMessage("Play", false);
    }

    public void Clear()
    {
        player.SendMessage("End");
        OutroUI.SetActive(true);
        OutroUI.SendMessage("Play", true);
    }

    public void GetKey(string name)
    {
        int i = System.Convert.ToInt32(name[3].ToString());
        hasKey[i - 1] = true;
        TextUI.SendMessage("KeyMessage", i);
        IconUI.SendMessage("KeyUpdate", hasKey);
        Doors[i - 1].SendMessage("SetActive");
    }
    public void GetEvident(string name)
    {
        int i = System.Convert.ToInt32(name[7].ToString());
        hasEvident[i - 1] = true;
        TextUI.SendMessage("EvidentMessage", i);
        IconUI.SendMessage("EvidentUpdate", hasEvident);
    }

    public void BatterySet(int time)
    {
        IconUI.SendMessage("BatteryUpdate", time);
    }
}
