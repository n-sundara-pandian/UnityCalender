using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayScript : MonoBehaviour {

    Text DayText;
    Image Bg;
    public Color SelectedColor;
    public Color NormalColor;
    bool Selected;

    public void Setup(DateTime text, bool selected)
    {
        DayText = gameObject.GetComponentInChildren<Text>();
        Bg = gameObject.GetComponent<Image>();
        DayText.text = text.Day.ToString();
        SetSelected(selected);
    }

    public void SetSelected(bool sel)
    {
        Selected = sel;
        if (Selected)
            Bg.color = SelectedColor;
        else
            Bg.color = NormalColor;
    }
}
