using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar
{
    public Image mask;
    public float minValue;
    public float maxValue;
    public float currentValue;

    float originalBarSize;

    public ProgressBar(Image mask, float current, float max)
    {
        this.mask = mask;
        currentValue = current;
        maxValue = max;
    }

    private void Start()
    {
        originalBarSize = mask.rectTransform.rect.width;
        //reset widths of background bars in case of incorrect sizes due to resolution change
        foreach (var image in mask.gameObject.GetComponentsInChildren<Image>())
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalBarSize);
    }

    private void Update()
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            originalBarSize * currentValue / maxValue);
    }
}
