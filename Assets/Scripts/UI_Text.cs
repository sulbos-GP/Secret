using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text : MonoBehaviour
{
    public GameObject Evident, Key;
    bool KeyActive = false, EvidentActive = false;
    string KeyText, EvidentText;
    float et, kt;
    // Start is called before the first frame update
    void Start()
    {
        Key.GetComponent<Text>().color = new Color(1, 1, 0, 1);
        Evident.GetComponent<Text>().color = new Color(0.8f, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Evident.GetComponent<Text>().text = EvidentText;
        Key.GetComponent<Text>().text = KeyText;
        if (EvidentActive)
        {
            et -= Time.deltaTime;
            if (et < 0)
            {
                EvidentActive = false;
                EvidentText = "";
            }
            if (et < 1)
                Evident.GetComponent<Text>().color -= new Color(0, 0, 0, Time.deltaTime);
        }
        if (KeyActive)
        {
            kt -= Time.deltaTime;
            if (kt < 0)
            {
                KeyActive = false;
                KeyText = "";
            }
            if (kt < 1)
                Key.GetComponent<Text>().color -= new Color(0, 0, 0, Time.deltaTime);
        }
    }

    public void KeyMessage(int i)
    {
        switch (i)
        {
            case 1:
                KeyText = "열쇠를 얻었습니다. \n 안으로 들어갈 수 있는 열쇠 같습니다...";
                break;
            case 2:
                KeyText = "열쇠를 얻었습니다. \n 거실 쪽으로 갈 수 있는 열쇠 같습니다...";
                break;
            case 3:
                KeyText = "열쇠를 얻었습니다. \n 계단 쪽으로 가는 미닫이문을 열 수 있을 것 같습니다...";
                break;
            case 4:
                KeyText = "열쇠를 얻었습니다. \n 안쪽 다락으로 갈 수 있는 열쇠 같습니다...";
                break;
            case 5:
                KeyText = "열쇠를 얻었습니다. \n 화장실 같은 문을 열 수 있는 것 같습니다!";
                break;
        }
        kt = 5;
        Key.GetComponent<Text>().color = new Color(1, 1, 0, 1);
        KeyActive = true;
    }

    public void EvidentMessage(int i)
    {
        switch (i)
        {
            case 1:
                EvidentText = "클립보드에 이렇게 적혀 있습니다.\n\n<b><i>“왜 세상은 권력을 가진 자에게\n오히려 평등하지 못한 것일까”</b></i>\n\n다른 열쇠를 찾아 클립보드를 더 찾아봅시다...";
                break;
            case 2:
                EvidentText = "클립보드에 이렇게 적혀 있습니다.\n\n<b><i>“권력으로 인해 나는\n삶의 터전도 꿈도 미래도 모두 한순간에 잃어버렸다.\n죽음 밖에 선택지가 없다” </b></i>\n\n다른 열쇠를 찾아 클립보드를 더 찾아봅시다...";
                break;
            case 3:
                EvidentText = "클립보드에 이렇게 적혀 있습니다.\n\n<b><i>“법은 능력있고 권력있는 자에게 오히려 고개를 숙였고,\n나는 피해자가 아닌 피의자가 되어버렸다.”</b></i>\n\n다른 열쇠를 찾아 클립보드를 더 찾아봅시다...";
                break;
            case 4:
                EvidentText = "클립보드에 이렇게 적혀 있습니다.\n\n<b><i>“그는 소중한 것을 얻었지만, 나는 모든 걸 잃어버렸다.\n오늘 그가 가장 소중히 여기는 다른 것을\n오늘 없애버릴 것이다.” </b></i>\n\n다른 열쇠를 찾아봅시다...";
                break;
        }
        et = 10;
        Evident.GetComponent<Text>().color = new Color(0.8f, 1, 1, 1);
        EvidentActive = true;
    }
}
