using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isActive = false;
    bool isOpen = false;
    bool isMoving = false;
    public bool testOpen = false;
    public bool openMode = false;
    float t = 0;
    float original;
    public AudioClip openSound;
    public AudioClip CloseSound;

    void Start()
    {
        original = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (testOpen)
        {
            isMoving = true;
            testOpen = false;
        }
        if (isMoving)
        {
            t += (!isOpen ? Time.deltaTime * 90 : -Time.deltaTime * 90);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, original + (openMode ? t : -t), transform.eulerAngles.z);
            if ((!isOpen && t > 90) || (isOpen && t < 0))
            {
                isOpen = !isOpen;
                isMoving = false;
                if (!isOpen)
                {
                    GetComponent<AudioSource>().clip = CloseSound;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    public void Open()
    {
        if (isActive) {
            isMoving = true;
            if (!isOpen)
            GetComponent<AudioSource>().clip = openSound;
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetActive()
    {
        isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            other.gameObject.SendMessage("GetBack");
        }
    }
}
