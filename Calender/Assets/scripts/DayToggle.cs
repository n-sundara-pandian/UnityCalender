using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DayToggle : Toggle {


    public DateTime? dateTime;
    public class OnDateTimeSelectedEvent : UnityEvent<DateTime?> { }
    public OnDateTimeSelectedEvent onDateSelected = new OnDateTimeSelectedEvent();
    bool m_clicked = false;
    public Color m_todayColor = Color.green;
    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(onToggleValueChanged);
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    // User trigger because ValueCHanged is triggered when toggle is set inactive
    private void OnPointerDownDelegate(PointerEventData data)
    {
        if (isActiveAndEnabled && IsInteractable())
        {
            m_clicked = true;
            onDateSelected.Invoke(dateTime);
        }
    }

    public void onToggleValueChanged(bool value)
    {
        AllowSwitchOffHack(value);
    }
    /// <summary>
    /// A hack to replace AllowSwitchOff of Toggle Group since it doesnt work well when the GameObject it is set inactive
    /// </summary>
    /// <param name="value"></param>
    void AllowSwitchOffHack(bool value)
    {
        if (m_clicked && !value)
        {
            m_clicked = false;
            isOn = true;
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
