using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Icon : MonoBehaviour
{
    public GameObject[] KeyIcon;
    public GameObject[] EvidentIcon;
    public GameObject BatteryGuage;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
            KeyIcon[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            EvidentIcon[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyUpdate(bool[] status)
    {
        for (int i = 0; i < 5; i++)
            KeyIcon[i].SetActive(status[i]);
    }
    public void EvidentUpdate(bool[] status)
    {
        for (int i = 0; i < 4; i++)
            EvidentIcon[i].SetActive(status[i]);
    }
    public void BatteryUpdate(int num)
    {
        BatteryGuage.GetComponent<Slider>().value = num;
    }
}
