using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;
    [Header("Sound")]
    public GameObject soundEnabled;
    public GameObject soundDisabled;
    private bool soundToggle = true;

    [Header("Vibration")]
    public GameObject vibrationEnabled;
    public GameObject vibrationDisabled;
    private bool vibrationToggle = true;

    [Header("Language")]
    public GameObject enEnabled;
    public GameObject enDisabled;
    public GameObject viEnabled;
    public GameObject viDisabled;
    public GameObject chEnabled;
    public GameObject chDisabled;
    public GameObject koEnabled;
    public GameObject koDisabled;
    public GameObject jaEnabled;
    public GameObject jaDisabled;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        gameObject.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();

        Image r = background.GetComponent<Image>();
        if (r.color.a <= 0)
        {
            LeanTween.value(background, 0, 1, .25f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
        }
    }

    #region Toggle Sound & Vibration
    /// <summary>
    /// Return if Sound is On or Off
    /// </summary>
    /// <returns>True is On, False is Off</returns>
    public bool getSoundToggle()
    {
        return soundToggle;
    }

    /// <summary>
    /// Return if Sound is On or Off
    /// </summary>
    /// <returns>True is On, False is Off</returns>
    public bool getVibrationToggle()
    {
        return vibrationToggle;
    }

    public void OnClick_Sound()
    {
        if (soundEnabled.activeSelf)
        {
            UnSelected(soundEnabled, soundDisabled);
            soundToggle = false;
        }
        else
        {
            Selected(soundEnabled, soundDisabled);
            soundToggle = true;
        }
    }

    public void OnClick_Vibration()
    {
        if (vibrationEnabled.activeSelf)
        {
            UnSelected(vibrationEnabled, vibrationDisabled);
            vibrationToggle = false;
        }
        else
        {
            Selected(vibrationEnabled, vibrationDisabled);
            vibrationToggle = true;
        }
    }
    #endregion

    #region Toggle Language
    //English
    //
    public void OnClick_en()
    {
        if (!enEnabled.activeSelf)
        {
            Selected(enEnabled, enDisabled);
            UnSelected(viEnabled, viDisabled);
            UnSelected(chEnabled, chDisabled);
            UnSelected(koEnabled, koDisabled);
            UnSelected(jaEnabled, jaDisabled);
        }
    }

    //Vietnamese
    //
    public void OnClick_vi()
    {
        if (!viEnabled.activeSelf)
        {
            Selected(viEnabled, viDisabled);
            UnSelected(enEnabled, enDisabled);
            UnSelected(chEnabled, chDisabled);
            UnSelected(koEnabled, koDisabled);
            UnSelected(jaEnabled, jaDisabled);
        }
    }

    //Chinese
    //
    public void OnClick_ch()
    {
        if (!chEnabled.activeSelf)
        {
            Selected(chEnabled, chDisabled);
            UnSelected(enEnabled, enDisabled);
            UnSelected(viEnabled, viDisabled);
            UnSelected(koEnabled, koDisabled);
            UnSelected(jaEnabled, jaDisabled);
        }
    }

    //Korean
    //
    public void OnClick_ko()
    {
        if (!koEnabled.activeSelf)
        {
            Selected(koEnabled, koDisabled);
            UnSelected(enEnabled, enDisabled);
            UnSelected(viEnabled, viDisabled);
            UnSelected(chEnabled, chDisabled);
            UnSelected(jaEnabled, jaDisabled);
        }
    }

    //Japanese
    //
    public void OnClick_ja()
    {
        if (!jaEnabled.activeSelf)
        {
            Selected(jaEnabled, jaDisabled);
            UnSelected(enEnabled, enDisabled);
            UnSelected(viEnabled, viDisabled);
            UnSelected(chEnabled, chDisabled);
            UnSelected(koEnabled, koDisabled);
        }
    }
    #endregion

    #region Other Methods
    public void UnSelected(GameObject enableGameobj, GameObject disableGameobj)
    {
        enableGameobj.SetActive(false);
        disableGameobj.SetActive(true);
    }

    public void Selected(GameObject enableGameobj, GameObject disableGameobj)
    {
        enableGameobj.SetActive(true);
        disableGameobj.SetActive(false);
    }
    #endregion
}
