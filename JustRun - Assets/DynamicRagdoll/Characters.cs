using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Characters : ScriptableObject
{
    public bool firstRun;
    public bool firstRunGame;
    public GameObject[] readyCharacters;
    public characterData[] displayCharacters;

    [System.Serializable]
    public struct characterData{
        public Transform character;
        public int price;
        public float stamina, skills, overall;
        public bool unlocked;
        public bool selected;
    }
}
