using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    public GameObject background;
    public GameObject playButton;
    private void OnEnable()
    {
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
}
