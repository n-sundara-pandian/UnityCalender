using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePicker : MonoBehaviour
{
    public HourOptionData HourPicker;
    public MinuteSecondOptionData MinutePicker;
    public MinuteSecondOptionData SecondPicker;
    // Start is called before the first frame update

    public DateTime SelectedTime(){
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            HourPicker.SelectedValue, MinutePicker.SelectedValue, SecondPicker.SelectedValue);
    }

    public void SetSelectedTime(int hour, int minute, int second){
       HourPicker.SelectedValue = hour; 
       MinutePicker.SelectedValue = minute;
       SecondPicker.SelectedValue = second;
    }
    public void SetSelectedTime(DateTime value){
        SetSelectedTime( value.Hour, value.Minute, value.Second);
    }


    public void Now_onClick(){
       SetSelectedTime(DateTime.Now);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
