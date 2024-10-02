using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class InOutro : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject panel;
    public GameObject[] Texts;
    public GameObject SkipText;
    public Color panelColor;
    bool isPlay = true;
    bool isSkipPressed = false;
    float t = 0, st = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Texts.Length; i++)
            Texts[i].GetComponent<Text>().color -= new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            t += Time.deltaTime;
            if (t < 2)
                panel.GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, t / 2f);
            else if (t < Texts.Length * 4 + 2)
            {
                int i = (int)((t - 2) / 4);
                Texts[i].GetComponent<Text>().color += new Color(0, 0, 0, Time.deltaTime / 3);
            }

            if (Input.GetAxis("Skip") >= 0.9f || Input.GetKey(KeyCode.Space))
            {
                st += Time.deltaTime;
                SkipText.GetComponent<Text>().text = ((int)(3 - st)).ToString();
                if (st > 3)
                {
                    gameManager.SendMessage("Begin");
                    isPlay = false;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                st = 0;
                SkipText.GetComponent<Text>().text = "오른쪽 트리거를 누르고 있으면 스킵됩니다.";
            }
        }
    }

    public void Play()
    {
        isPlay = true;
        t = 0;
        GetComponent<AudioSource>().Play();
    }
}
