using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Globalization;

public class DatePicker : MonoBehaviour {

    private DayToggle[] DayToggles = new DayToggle[7 * 6];

    public DayToggle DayToggleTemplate;
    public Text DayNameLabelTemplate;
    [SerializeField]
    private GridLayoutGroup DayContainer;
    [SerializeField]
    private Text SelectedDateText;
    [SerializeField]
    private Text CurrentMonth;
    [SerializeField]
    private Text CurrentYear;
    public string DateFormat = "dd-MM-yyyy";
    public string MonthFormat = "MMMMM";
    public bool ForwardPickOnly = false;
    private DateTime m_SelectedDate;

    public DateTime SelectedDate
    {
        get { return m_SelectedDate; }
        private set
        {
            m_SelectedDate = value;
            SelectedDateText.text = m_SelectedDate.ToString(DateFormat);
        }
    }
    DateTime m_ReferenceDate;
    DateTime m_DisplayDate = DateTime.Now;
    public DateTime ReferenceDateTime{
        get{
            return m_ReferenceDate;
        }set{
            m_ReferenceDate = DateTimeHelpers.GetYearMonthStart(value);
            CurrentYear.text = m_ReferenceDate.Year.ToString();
            CurrentMonth.text = m_ReferenceDate.ToString(MonthFormat);
        }
    }

    public DayOfWeek startDayOfWeek;
    void Start()
    {
        GenerateDaysNames();
        GenerateDaysToggles();

        SelectedDate = DateTime.Today;
        ReferenceDateTime = SelectedDate;

        DisplayMonthDays(true);
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
            DayNameLabel.name = String.Format("Day Name Label ({0})", day_name);
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
            DayToggles[i] = DayToggle;
        }
    }
    private void OnDisable()
    {
        //DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = false;
    }

    private void OnEnable()
    {
        //DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = true;
    }

    private void DisplayMonthDays(bool refresh = false)
    {
        Debug.Log("Display Months");
        if (!refresh && m_DisplayDate.IsSameYearMonth(ReferenceDateTime)){
            return;
        }
        m_DisplayDate = ReferenceDateTime.DuplicateDate(ReferenceDateTime);

        int monthdays = ReferenceDateTime.DaysInMonth();
 
        DateTime day_datetime = m_DisplayDate.GetYearMonthStart();

        int dayOffset = (int)day_datetime.DayOfWeek - (int)startDayOfWeek;
        if ((int)day_datetime.DayOfWeek < (int)startDayOfWeek)
        {
            dayOffset = (7 + dayOffset);
        }
        day_datetime = day_datetime.AddDays(-dayOffset); 
        //DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = true;
        for (int i = 0; i < DayToggles.Length; i++)
        {
            SetDayToggle(DayToggles[i], day_datetime);
            day_datetime = day_datetime.AddDays(1);
        }        
        //DayContainer.GetComponent<ToggleGroup>().allowSwitchOff = false;
    }

    void SetDayToggle(DayToggle dayToggle, DateTime toggleDate){
        dayToggle.onDateSelected.RemoveListener(OnDaySelected);

        dayToggle.interactable = ((!ForwardPickOnly || (ForwardPickOnly && !toggleDate.IsPast())) && toggleDate.IsSameYearMonth(m_DisplayDate));
        dayToggle.name = String.Format("Day Toggle ({0} {1})", toggleDate.ToString("MMM"), toggleDate.Day);
        dayToggle.SetText(toggleDate.Day.ToString());
        dayToggle.dateTime = toggleDate;
        dayToggle.isOn = SelectedDate.IsSameDate(toggleDate);

        dayToggle.onDateSelected.AddListener(OnDaySelected);
    }

    public void YearInc_onClick()
    {
        ReferenceDateTime = ReferenceDateTime.AddYears(1);
            DisplayMonthDays(false);
    }
    public void YearDec_onClick()
    {
        if (!ForwardPickOnly ||( !ReferenceDateTime.IsCurrentYearMonth() && !ReferenceDateTime.IsPastYearMonth())){
            ReferenceDateTime = ReferenceDateTime.AddYears(-1);
            DisplayMonthDays(false);
        }
    }
    public void MonthInc_onClick()
    {
        ReferenceDateTime = ReferenceDateTime.AddMonths(1);
            DisplayMonthDays(false);
    }
    public void MonthDec_onClick()
    {
        if (!ForwardPickOnly ||( !ReferenceDateTime.IsCurrentYearMonth() && !ReferenceDateTime.IsPastYearMonth())){
            ReferenceDateTime = ReferenceDateTime.AddMonths(-1);
            DisplayMonthDays(false);
        }
    }

    public void SetSelectedDate(DateTime date){
        SelectedDate = date;
        SwitchToSelectedDate();
    }
    void OnDaySelected(DateTime? date)
    {
        SetSelectedDate((DateTime)date);
    }

    public void SwitchToSelectedDate(){
        ReferenceDateTime = SelectedDate;
        DisplayMonthDays(false);
    }
    public void Today_onClick()
    {
        ReferenceDateTime = DateTime.Today;
        DisplayMonthDays(false);
    }

 
}
