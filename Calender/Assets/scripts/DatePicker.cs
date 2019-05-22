using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Globalization;

public class DatePicker : MonoBehaviour {
  
    private DayToggle[] DayToggles = new DayToggle[7 * 6];

    public DayToggle DayToggleTemplate;
    public Text DayNameLabelTemplate;
    public GridLayoutGroup DayContainer;
    public Text SelectedDateText;
    public Text CurrentMonth;
    public Text CurrentYear;
    public string DateFormat = "dd-MM-yyyy";
    public string MonthFormat = "MMMMM";
    public bool ForwardPickOnly = false;
    private DateTime m_SelectedDate;

    public DateTime SelectedDate
    {
        get { return m_SelectedDate; }
        private set
        {
            if (m_SelectedDate == value)
                return;
            
            Debug.LogFormat("Date changed from {0} to {1}", m_SelectedDate.ToShortDateString(), value.ToShortDateString());
            m_SelectedDate = value;
            SelectedDateText.text = value.ToString(DateFormat);
            ReferenceDate = new DateTime( value.Year, value.Month, 1);
        }
    }
    DateTime m_ReferenceDate;
    public DateTime ReferenceDate
    {
        get { return m_ReferenceDate; }
        private set
        {
            if ((ForwardPickOnly) 
                && (value.Year < DateTime.Today.Year 
                || (value.Year == DateTime.Today.Year && value.Month < DateTime.Today.Month)))
            {
                return;
            }

            m_ReferenceDate = value;
            CurrentMonth.text = value.ToString(MonthFormat);
            CurrentYear.text = value.Year.ToString();
            DisplayMonthDays();
        }
    }

    public DayOfWeek startDayOfWeek;
    void Start()
    {
        GenerateDaysNames();
        GenerateDaysToggles();
        SelectedDate = DateTime.Today;
        
    }
 
    public string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
    }

    public void GenerateDaysNames(){
        int dayOfWeek = (int)startDayOfWeek;
        for (int d = 1; d <= 7; d++){       
            string day_name = Truncate(Enum.GetName(typeof(DayOfWeek), dayOfWeek), 3);
            var DayNameLabel = Instantiate(DayNameLabelTemplate);
            DayNameLabel.transform.SetParent(DayContainer.transform);
            DayNameLabel.GetComponentInChildren<Text>().text = day_name;
            dayOfWeek++;
            if (dayOfWeek >= 7){
                dayOfWeek = 0;
            }
        }
    }
    public void GenerateDaysToggles(){
        for (int i = 0; i < DayToggles.Length; i++){
            var DayToggle = Instantiate(DayToggleTemplate);
            DayToggle.transform.SetParent(DayContainer.transform);
            DayToggle.GetComponentInChildren<Text>().text = string.Empty;
            DayToggle.onDateSelected.AddListener(OnDaySelected);
            DayToggles[i] = DayToggle;
        }
    }

    private void DisplayMonthDays()
    {
        int monthdays = DateTime.DaysInMonth(ReferenceDate.Year, ReferenceDate.Month);
 
        DateTime day_datetime = new DateTime(ReferenceDate.Year, ReferenceDate.Month, 1);

        int dayOffset = (int)day_datetime.DayOfWeek - (int)startDayOfWeek;
        if ((int)day_datetime.DayOfWeek < (int)startDayOfWeek)
        {
            dayOffset = (7 + dayOffset);
        }
        day_datetime = day_datetime.AddDays(-dayOffset); 
        DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = true;
        for (int i = 0; i < DayToggles.Length; i++)
        {
            SetDayToggle(DayToggles[i], day_datetime);
            day_datetime = day_datetime.AddDays(1);
        }        
        DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = false;
    }

    void SetDayToggleEmpty(DayToggle dayToggle){
        dayToggle.interactable = false;
        dayToggle.isOn = false;
        //dayToggle.dateTime = null;
        //dayToggle.ClearText();
    }

    void SetDayToggle(DayToggle dayToggle, DateTime toggleDate){
        dayToggle.interactable = (!ForwardPickOnly || toggleDate >= DateTime.Today) && toggleDate.Month == ReferenceDate.Month && toggleDate.Year == ReferenceDate.Year;
        dayToggle.SetText(toggleDate.Day.ToString());
        dayToggle.isOn = SelectedDate == toggleDate;
        dayToggle.dateTime = toggleDate;
    }

    public void OnYearInc()
    {
        ReferenceDate = ReferenceDate.AddYears(1);
    }
    public void OnYearDec()
    {
        ReferenceDate = ReferenceDate.AddYears(-1);
        
    }
    public void OnMonthInc()
    {
        ReferenceDate = ReferenceDate.AddMonths(1);
    }
    public void OnMonthDec()
    {
        ReferenceDate = ReferenceDate.AddMonths(-1);
    }
    public void OnDaySelected(DateTime? date)
    {
        Debug.Log("OnDaySelected");
       if (date != null){
        SelectedDate = (DateTime)date;
       } 
        
        
    }
    public void OnToday()
    {
        OnDaySelected(DateTime.Today);
    }

 
}
