using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeQuality : MonoBehaviour
{
    public AudioSource sound;
    public TMP_Dropdown qualityDropdown;

    private void Awake() {
        qualityDropdown.value = QualitySettings.GetQualityLevel();        
    }

    public void ChangeQualityLevel(TMP_Dropdown dropdown){
        QualitySettings.SetQualityLevel(dropdown.value);
    }

    public void MuteAudio(Toggle toggle){
        sound.mute = toggle.isOn;
    }
}
