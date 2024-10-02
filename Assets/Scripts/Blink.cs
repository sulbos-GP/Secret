using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject Player;
    float t = 0;
    bool isGraped = false;
    public bool test = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isGraped)
        {
            t += Time.deltaTime;
            GetComponent<Light>().intensity = 0;
            if (t > 0.8f)
                Destroy(gameObject);
        }
        else
        {
            GetComponent<Light>().intensity = 5 - Vector3.Distance(transform.position, Player.transform.position);
            if (test)
                Gotten();
        }
    }
    public void Gotten()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            isGraped = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
