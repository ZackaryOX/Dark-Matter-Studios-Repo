using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager ThisManager;

    [FMODUnity.EventRef]
    public string audioEventName;
    FMOD.Studio.EventInstance AudioEventInstance;


    // Start is called before the first frame update
    void Start()
    {
        if(AudioManager.ThisManager == null)
        {
            AudioManager.ThisManager = this;
        }
        else if(AudioManager.ThisManager != this)
        {
            Destroy(AudioManager.ThisManager.gameObject);
            AudioManager.ThisManager = this;
        }
        DontDestroyOnLoad(gameObject);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(AudioEventInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
