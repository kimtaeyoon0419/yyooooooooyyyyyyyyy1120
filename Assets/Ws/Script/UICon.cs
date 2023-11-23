using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICon : MonoBehaviour
{
    public Slider _musicSlider;

    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_musicSlider.value);
    }
}
