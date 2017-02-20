using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DatePicker : MonoBehaviour {

    public DayScript[] DayList;
    public Text SelectedDateText;
    public Text CurrentMonth;
    public Text CurrentYear;
    public string DateFormat = "dd-MM-yyyy";
    public bool ForwardPickOnly = false;
    DateTime SelectedDate = DateTime.Today;
    DateTime ReferenceDate = DateTime.Today;

    void Start()
    {
        DayList = gameObject.GetComponentsInChildren<DayScript>();
        SetupVariables();
    }
    string GetMonth(int index)
    {
        string result = "January";
        switch(index)
        {
            case 1:
                result = "January";
                break;
            case 2:
                result = "February";
                break;
            case 3:
                result = "March";
                break;
            case 4:
                result = "April";
                break;
            case 5:
                result = "May";
                break;
            case 6:
                result = "Jun";
                break;
            case 7:
                result = "July";
                break;
            case 8:
                result = "August";
                break;
            case 9:
                result = "September";
                break;
            case 10:
                result = "October";
                break;
            case 11:
                result = "November";
                break;
            case 12:
                result = "December";
                break;
            default:
                Debug.Log(" Error : Improbable month");
                break;
        }
        return result;
    }
    void SetupVariables()
    {
        SelectedDateText.text = SelectedDate.ToString(DateFormat);
        CurrentMonth.text = GetMonth(ReferenceDate.Month);
        CurrentYear.text = ReferenceDate.Year.ToString();
        Generate();
    }
    public void Generate()
    {
        int month = ReferenceDate.Month; 
        int year = ReferenceDate.Year;
        DateTime dateTime = new DateTime(year, month, 1);
        int day = (int)dateTime.DayOfWeek;
        int no_of_days_in_month = DateTime.DaysInMonth(year, month);
        for (int i = 0; i <  DayList.Length; i++)
        {
            if (i < day || i >= (day + no_of_days_in_month))
            {
                DayList[i].gameObject.SetActive(false);
                continue;
            }
            DateTime date = new DateTime(year, month, (i - day) + 1);
            if (ForwardPickOnly && date < DateTime.Today)
            {
                DayList[i].gameObject.SetActive(false);
                continue;
            }
            DayList[i].gameObject.SetActive(true);
            DayList[i].Setup(date, SelectedDate == date);
            Button btn = DayList[i].GetComponentInChildren<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                OnDaySelected(date);
            });
        }        
    }

    bool ValidateForwardPickOnly(DateTime date)
    {
        if (!ForwardPickOnly)
            return true;
        if (date < DateTime.Today)
            OnToday();
        return true;
    }
    public void OnYearInc()
    {
        if (!ValidateForwardPickOnly(ReferenceDate.AddYears(1)))
            return;
        ReferenceDate = ReferenceDate.AddYears(1);
        SetupVariables();
    }
    public void OnYearDec()
    {
        if (!ValidateForwardPickOnly(ReferenceDate.AddYears(-1)))
            return;
        ReferenceDate = ReferenceDate.AddYears(-1);
        SetupVariables();
    }
    public void OnMonthInc()
    {
        if (!ValidateForwardPickOnly(ReferenceDate.AddMonths(1)))
            return;
        ReferenceDate = ReferenceDate.AddMonths(1);
        SetupVariables();

    }
    public void OnMonthDec()
    {
        if (!ValidateForwardPickOnly(ReferenceDate.AddMonths(-1)))
            return;
        ReferenceDate = ReferenceDate.AddMonths(-1);
        SetupVariables();

    }
    public void OnDaySelected(DateTime date)
    {
        SelectedDate = date;
        ReferenceDate = date;
        SetupVariables(); 
    }
    public void OnToday()
    {
        ReferenceDate = DateTime.Today;
        SetupVariables();
    }
    public void OnCurrentSelectedDay()
    {
        ReferenceDate = SelectedDate;
        SetupVariables();
    }
    public DateTime GetSelectedDate()
    {
        return SelectedDate;
    }
}
