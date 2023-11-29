using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Settingpanel;
    [SerializeField]
    bool IsSetting = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsSetting)
            {
                SettingpanelOpen();
            }
            else if (IsSetting)
            {
                SettingpanelClose();
            }
        }
    }

    void SettingpanelOpen()
    {
        Settingpanel.SetActive(true);
        IsSetting = true;
        Time.timeScale = 0f;
    }
    void SettingpanelClose()
    {
        Settingpanel.SetActive(false);
        IsSetting = false;
        Time.timeScale = 1f;
    }

}
