using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStateBar : MonoBehaviour
{
    public Image greenHPimage;
    public Image redHPImage;

    [Header("延迟比率")] 
    public float delay = 1.0f;
    private void Update()
    {
        if (greenHPimage.fillAmount < redHPImage.fillAmount)
        {
            redHPImage.fillAmount -= Time.deltaTime*delay;
        }
    }

    public void OnHealthChange(float perc)
    {
        greenHPimage.fillAmount = perc;
    }
}
