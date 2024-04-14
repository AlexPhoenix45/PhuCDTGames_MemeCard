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
        gameObject.transform.localScale = Vector3.one;
        playButton.transform.localScale = Vector3.zero;
        Image r = background.GetComponent<Image>();
        LeanTween.value(gameObject, 1, 0, .25f).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

        playButton.transform.LeanScale(Vector3.one, 1f).setEaseOutElastic();
    }
}
