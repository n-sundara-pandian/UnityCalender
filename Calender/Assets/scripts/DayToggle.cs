using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DayToggle : Toggle {

    public DateTime? dateTime;
    public class OnDateTimeSelectedEvent : UnityEvent<DateTime?> { }
    public OnDateTimeSelectedEvent onDateSelected = new OnDateTimeSelectedEvent();

    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(onToggleValueChanged);
    }

    void onToggleValueChanged(bool value)
    {
        if (value)
        {
            onDateSelected.Invoke(dateTime);
            Debug.Log("Toggled");
        }
    }
 
    public void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void ClearText()
    {
        SetText(string.Empty);
    }
}
