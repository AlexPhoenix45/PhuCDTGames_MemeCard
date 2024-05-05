using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    public GameObject background;
    public GameObject playButton;
    public Slider levelSlider;
    public GameObject level1_active;
    public GameObject level1_inActive;
    public TextMeshProUGUI level1_text;
    public GameObject level2_active;
    public GameObject level2_inActive;
    public TextMeshProUGUI level2_text;
    public GameObject level3_active;
    public GameObject level3_inActive;
    public TextMeshProUGUI level3_text;
    public GameObject level4_active;
    public GameObject level4_inActive;
    public TextMeshProUGUI level4_text;
    public GameObject level5_active;
    public GameObject level5_inActive;

    private void OnEnable()
    {
        EventController.LoadLevelSlider += LevelSliderExecuter;

        playButton.SetActive(true);
        gameObject.transform.localScale = Vector3.one;
        playButton.transform.localScale = Vector3.zero;
        Image r = background.GetComponent<Image>();
        if (r.color.a != 0)
        {
            LeanTween.value(gameObject, 1, 0, .25f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
        }

        playButton.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();
    }

    public void OnClick_Play()
    {
        //Playing a Game
        EventController.OnTurnTableCam();
        EventController.OnHideNavButtons();

        playButton.SetActive(false);
        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);
            while (FindObjectOfType<CameraMovement>().isBlending == true)
            {
                yield return null;
            }
            EventController.OnStartGame();
            gameObject.SetActive(false);
        }
        StartCoroutine(wait());
    }

    public void ShowLevelSlider()
    {
        levelSlider.GetComponent<CanvasGroup>().alpha = 0;
        LeanTween.value(0, 1, 1f).setOnUpdate((float value) =>
        {
            levelSlider.GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(() =>
        {
            levelSlider.GetComponent<CanvasGroup>().alpha = 1;
        });
    }

    public void LevelSliderExecuter(PlayerData playerData)
    {
        float currentLevelRange = playerData.currentLvl / 5;
        level1_text.text = (5 * Mathf.Floor(currentLevelRange)).ToString();
        level2_text.text = (5 * Mathf.Floor(currentLevelRange) + 1).ToString();
        level3_text.text = (5 * Mathf.Floor(currentLevelRange) + 2).ToString();
        level4_text.text = (5 * Mathf.Floor(currentLevelRange) + 3).ToString();

        if (playerData.currentLvl == int.Parse(level1_text.text))
        {
            levelSlider.value = 0;
            level1_active.SetActive(true);
            level1_inActive.SetActive(false);

            level2_active.SetActive(false);
            level2_inActive.SetActive(true);
            level3_active.SetActive(false);
            level3_inActive.SetActive(true);
            level4_active.SetActive(false);
            level4_inActive.SetActive(true);
            level5_active.SetActive(false);
            level5_inActive.SetActive(true);
        }
        else if (playerData.currentLvl == int.Parse(level2_text.text))
        {
            levelSlider.value = 0;
            LeanTween.value(0, 25, 1f).setOnUpdate((float value) =>
            {
                levelSlider.value = value;
                if (value >= 0)
                {
                    level1_active.SetActive(true);
                    level1_inActive.SetActive(false);
                }

                if (value == 25)
                {
                    level2_active.SetActive(true);
                    level2_inActive.SetActive(false);
                }
            }).setOnComplete(() =>
            {
                levelSlider.value = 25;

                level3_active.SetActive(false);
                level3_inActive.SetActive(true);
                level4_active.SetActive(false);
                level4_inActive.SetActive(true);
                level5_active.SetActive(false);
                level5_inActive.SetActive(true);
            });
        }
        else if (playerData.currentLvl == int.Parse(level3_text.text))
        {
            levelSlider.value = 0;
            LeanTween.value(0, 50, 1f).setOnUpdate((float value) =>
            {
                levelSlider.value = value;

                if (value >= 0)
                {
                    level1_active.SetActive(true);
                    level1_inActive.SetActive(false);
                }

                if (value >= 25)
                {
                    level2_active.SetActive(true);
                    level2_inActive.SetActive(false);
                }

                if (value == 50)
                {
                    level3_active.SetActive(true);
                    level3_inActive.SetActive(false);
                }
            }).setOnComplete(() =>
            {
                levelSlider.value = 50;

                level4_active.SetActive(false);
                level4_inActive.SetActive(true);
                level5_active.SetActive(false);
                level5_inActive.SetActive(true);
            });
        }
        else if (playerData.currentLvl == int.Parse(level4_text.text))
        {
            levelSlider.value = 0;
            LeanTween.value(0, 75, 1f).setOnUpdate((float value) =>
            {
                levelSlider.value = value;

                if (value >= 0)
                {
                    level1_active.SetActive(true);
                    level1_inActive.SetActive(false);
                }

                if (value >= 25)
                {
                    level2_active.SetActive(true);
                    level2_inActive.SetActive(false);
                }

                if (value >= 50)
                {
                    level3_active.SetActive(true);
                    level3_inActive.SetActive(false);
                }

                if (value == 75)
                {
                    level4_active.SetActive(true);
                    level4_inActive.SetActive(false);
                }
            }).setOnComplete(() =>
            {
                levelSlider.value = 75;

                level5_active.SetActive(false);
                level5_inActive.SetActive(true);
            });
        }
        else if (playerData.currentLvl == 5 * Mathf.Ceil(currentLevelRange))
        {
            levelSlider.value = 0;
            LeanTween.value(0, 100, 1f).setOnUpdate((float value) =>
            {
                levelSlider.value = value;

                if (value >= 0)
                {
                    level1_active.SetActive(true);
                    level1_inActive.SetActive(false);
                }

                if (value >= 25)
                {
                    level2_active.SetActive(true);
                    level2_inActive.SetActive(false);
                }

                if (value >= 50)
                {
                    level3_active.SetActive(true);
                    level3_inActive.SetActive(false);
                }

                if (value >= 75)
                {
                    level4_active.SetActive(true);
                    level4_inActive.SetActive(false);
                }

                if (value == 100)
                {
                    level5_active.SetActive(true);
                    level5_inActive.SetActive(false);
                }
            }).setOnComplete(() =>
            {
                levelSlider.value = 100;
            });
        }
    }

    private void OnDisable()
    {
        EventController.LoadLevelSlider -= LevelSliderExecuter;
    }
}
