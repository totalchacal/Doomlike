﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartVolume : MonoBehaviour
{
    public Slider volumeSlider;
    float volume;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SliderVolumeLevel"))
        {
            volume = PlayerPrefs.GetFloat("SliderVolumeLevel");
            volumeSlider.value = volume;
        }
    }
}
