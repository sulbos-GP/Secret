using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlide : MonoBehaviour
{
    bool isActive = false;
    bool isOpen = false;
    bool isMoving = false;
    public bool testOpen = false;
    float t = 0;
    float original;
    public AudioClip openSound;
    public AudioClip CloseSound;

    void Start()
    {
        original = transform.position.x;
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
            t += (isOpen ? Time.deltaTime * 1.3f : -Time.deltaTime * 1.3f);
            transform.position = new Vector3(original + t, transform.position.y, transform.position.z);
            if ((!isOpen && t < -1.3) || (isOpen && t > 0))
            {
                isOpen = !isOpen;
                isMoving = false;
            }
        }
    }

    public void Open()
    {
        if (isActive)
        {
            isMoving = true;
            GetComponent<AudioSource>().clip = (isOpen ? CloseSound : openSound);
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
