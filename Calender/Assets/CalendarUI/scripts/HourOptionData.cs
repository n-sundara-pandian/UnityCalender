using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

[RequireComponent(typeof(Dropdown))]
public class HourOptionData : MonoBehaviour
{
    public bool is24Hour = false;
    private Dropdown m_dropDownComponent = null;
    private Dropdown dropDownComponent{
        get{
            if(m_dropDownComponent == null){
                m_dropDownComponent = this.GetComponent<Dropdown>();
            }
            return m_dropDownComponent;
        }
    }

   public int SelectedValue{
       get{
           return dropDownComponent.value;
       }
       set{
           dropDownComponent.value = value;
       }
   } 

    public void PopulateOptionData(){
        dropDownComponent.ClearOptions();
        var lst24 = new List<string>();
        int h = 0;
        string tm = "am";
        string strHour = string.Empty;
        for(int i = 0; i < 24; i++){
            h = i; 
            if (!is24Hour && h > 11){
                if (h > 12){
                    h = h - 12;
                }
                tm = "pm";
            } else if (!is24Hour && h == 0){
                h = 12;
            }
            if (!is24Hour){
                strHour = string.Format("{0} {1}", h.ToString() , tm);
            }else{
                strHour = h.ToString("D2");
            }
            lst24.Add(strHour);            
        }
        dropDownComponent.AddOptions(lst24);
    }
    // Start is called before the first frame update
    void Start()
    {
        PopulateOptionData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
