using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{

    public GameObject lights;
    public GameObject audio;
    public GameObject spooky;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }


    // Update is called once per frame
    IEnumerator Flicker()
    {
        timer = Random.Range(0.1f, 1f);
        lights.SetActive(true);
        audio.GetComponent<AudioSource>().mute = false;
        spooky.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(timer);
        lights.SetActive(false);
        spooky.GetComponent<Renderer>().enabled = false;
        audio.GetComponent<AudioSource>().mute = true;
        timer = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(timer);
        StartCoroutine(Flicker());
        
    }
}
