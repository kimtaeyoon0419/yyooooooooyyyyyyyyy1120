using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour
{
    public Image Panel;
    float time = 0;
    float F_time = 1f;

    private void Awake()
    {
        Fade();
    }


    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;

        //∆‰¿ÃµÂ æ∆øÙ
        //while(alpha.a < 1f)
        //{
        //    time += Time.deltaTime / F_time;
        //    alpha.a = Mathf.Lerp(0,1, time);
        //    Panel.color = alpha;
        //    yield return null;
        //}

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);


        yield return null;
    }
}
