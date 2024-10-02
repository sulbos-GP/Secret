using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject panel;
    public GameObject[] OverTexts;
    public GameObject[] ClearTexts;
    public GameObject SkipText;
    public GameObject Player;
    public AudioClip OverSound;

    Color panelColor;
    bool isPlay = false;
    bool isCleared = false;
    GameObject[] Texts;
    float t = 0, st = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            t += Time.deltaTime;
            if (t < 3)
                panel.GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, t / 3f);
            else if (t < Texts.Length * 4 + 3)
            {
                int i = (int)((t - 3) / 4);
                Texts[i].GetComponent<Text>().color += new Color(0,0,0, Time.deltaTime / 3);
            }

            if (Input.GetAxis("Skip") >= 0.9f || Input.GetKey(KeyCode.Space))
            {
                st += Time.deltaTime;
                SkipText.GetComponent<Text>().text = ((int)(3 - st)).ToString();
                if (st > 3)
                {
                    Application.Quit();
                }
            }
            else
            {
                st = 0;
                SkipText.GetComponent<Text>().text = "오른쪽 트리거를 길게 누르면 게임을 종료합니다.";
            }
        }
    }

    public void Play(bool isClear)
    {
        isCleared = isClear;
        if (isCleared)
        {
            panelColor = new Color(0.9f, 0.9f, 0.9f);
            foreach (GameObject i in ClearTexts)
                i.GetComponent<Text>().color += new Color(1, 1, 1, 0);
            Texts = ClearTexts;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            panelColor = new Color(0.5f, 0.1f, 0.1f);
            foreach (GameObject i in OverTexts)
                i.GetComponent<Text>().color += new Color(0, 0, 0, 0);
            Texts = OverTexts;
            GetComponent<AudioSource>().clip = OverSound;
            GetComponent<AudioSource>().pitch = 0.5f;
            GetComponent<AudioSource>().Play();
        }
        t = 0;
        isPlay = true;
    }
}
