using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer mainAudioMixer;

    public void setVolume(float volume){
        mainAudioMixer.SetFloat("volume", volume);
    }
}

