using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

[RequireComponent(typeof(Dropdown))]
public class TimeOptionData : MonoBehaviour
{
    private Dropdown m_dropDownComponent = null;
    private Dropdown dropDownComponent{
        get{
            if(m_dropDownComponent == null){
                m_dropDownComponent = this.GetComponent<Dropdown>();
            }
            return m_dropDownComponent;
        }
    }

    public void PopulateOptionData(){
        dropDownComponent.ClearOptions();
        var lst60 = new List<string>();
        for(int i = 0; i < 60; i++){
            lst60.Add(i.ToString("D2"));            
        }
        dropDownComponent.AddOptions(lst60);
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
