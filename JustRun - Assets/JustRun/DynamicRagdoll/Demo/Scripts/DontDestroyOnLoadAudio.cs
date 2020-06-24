using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadAudio : MonoBehaviour
{
    public static DontDestroyOnLoadAudio instance;
    public bool audioS;
    private void Awake() {
        if(audioS){
            if(instance == null) instance = this;
        if(instance != this) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
