using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject background;

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
}
