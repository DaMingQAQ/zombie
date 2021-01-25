using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public static float volume = 1;
    public Text text;
    public void SetVolume(Slider s)
    {
        volume = s.value;
        text.text = $"Volume: {(int)(s.value*100)}%";
        GetComponentInParent<AudioSource>().volume = volume;
    }
}
