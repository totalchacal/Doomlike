using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVolumeValue : MonoBehaviour
{
    public void SaveSliderValue(System.Single volume)
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", volume);
    }
}
