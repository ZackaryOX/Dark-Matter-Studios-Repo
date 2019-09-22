using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{

    public GameObject lights;
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
        yield return new WaitForSeconds(timer);
        lights.SetActive(false);
        timer = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(timer);
        StartCoroutine(Flicker());
        
    }
}
