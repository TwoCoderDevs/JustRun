using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSound : MonoBehaviour
{
    public static SingletonSound instance;

    private void Awake() {
        if(instance == null) instance = this;
        if(instance != this) Destroy(gameObject);
    }
}
