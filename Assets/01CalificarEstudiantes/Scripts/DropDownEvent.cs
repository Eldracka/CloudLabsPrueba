using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownEvent : MonoBehaviour
{
    public void EventVerify()
    {
        var gm = GameObject.FindObjectOfType<GameManager>();
        int index = this.gameObject.transform.parent.GetSiblingIndex(); 
        
        var dropdown = GetComponent<TMP_Dropdown>();
        string selected = dropdown.options[dropdown.value].text;
        if (gm.CheckSelection(index).ToLower() == selected.ToLower() )
        {
            dropdown.interactable = false;
            gm.ChangeState(index,selected);
        }
        else
        {
            //debe de sacar un mensaje de advertencia de que el profe ha cometido un error
            UIManager ui = UIManager.FindObjectOfType<UIManager>();
            dropdown.value = 0;
            ui.ActivePanelAlert();
            Debug.Log("<color=magenta> el calificador ha cometido un error </color>");
        }
    }
}
