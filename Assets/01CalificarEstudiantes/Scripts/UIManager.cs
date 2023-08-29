using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public GameObject prefabData;
    public Transform parentDataUI;
    public GameObject panelAlert;
    public GameObject finishPanel;
    public GameObject noyetPanel;
    public GameObject checkAgainBtn;
    private GameManager gameManager;
    private List<float> note;

    void Start()
    {
        note = new List<float>();
        gameManager = GameManager.FindObjectOfType<GameManager>();
        SetDataTable(gameManager.dataStudents);
    }

    private void SetDataTable(List<DataJson> dataJsons)
    {
        foreach (var p in dataJsons)
        {
            var x = Instantiate(prefabData);
            x.transform.SetParent(parentDataUI);
            
            x.transform.Find("Codigo").gameObject.GetComponent<TextMeshProUGUI>().text = p.codigo.ToString();
            x.transform.Find("Nombre").gameObject.GetComponent<TextMeshProUGUI>().text = p.nombre;
            x.transform.Find("Apellido").gameObject.GetComponent<TextMeshProUGUI>().text = p.apellido;
            x.transform.Find("Correo").gameObject.GetComponent<TextMeshProUGUI>().text = p.correo;
            x.transform.Find("Nota").gameObject.GetComponent<TextMeshProUGUI>().text = p.nota.ToString();
            var dropdwonBox =x.transform.Find("Aprobado").gameObject.GetComponent<TMP_Dropdown>();
            GetSetDropDwon(dropdwonBox,p.aprobado.ToUpper(),p.calificado);
            
            note.Add(p.nota);
        }
    }

    private void GetSetDropDwon(TMP_Dropdown dropdown, string data, bool calificado)
    {
        if (!calificado)
        {
            List<string> opciones = new List<string>();
            opciones.Add("Sin Aprobar");
            opciones.Add("Aprobado");
            opciones.Add("Reprobado");
            
            dropdown.options.Clear();
            dropdown.AddOptions(opciones);
            dropdown.RefreshShownValue();
        }
        else
        {
            dropdown.options.Clear();
            dropdown.options.Add(new TMP_Dropdown.OptionData(data,null));
            dropdown.RefreshShownValue();
            dropdown.interactable = false;
        }
    }

    public void ReloadData()
    {
        gameManager.ReloadDataFromJson();
        //borrar toda la data anterior
        foreach (Transform child in parentDataUI)
        {
            Destroy(child.gameObject);
        }
        SetDataTable(gameManager.dataStudents);
    }
    
    public void ChangeFormatNote()
    {
        List<Transform> uiComponentNota = new List<Transform>();
        foreach (Transform child in parentDataUI)
        {
            uiComponentNota.Add(child.Find("Nota"));
        }

        for (int i = 0; i < note.Count; i++)
        {
            var newNote = note[i] * 20;
            uiComponentNota[i].gameObject.GetComponent<TextMeshProUGUI>().text = newNote.ToString();
        }
    }

    public void ReactiveDropDown()
    {
        var drops = FindObjectsOfType<TMP_Dropdown>();
        //checkAgainBtn.SetActive(true);
        foreach (var z in drops)
        {
            
            var actualOption = z.options[z.value].text;
            z.options.Clear();
            List<string> opciones = new List<string>();
            opciones.Add("Sin Aprobar");
            opciones.Add("Aprobado");
            opciones.Add("Reprobado");
            
            z.AddOptions(opciones);
            if (actualOption.ToLower()=="aprobado")
            {
                z.value = 1;
            }
            else if (actualOption.ToLower() == "reprobado")
            {
                z.value = 2;
            }
            else
            {
                z.value = 0;
            }
            z.RefreshShownValue();
            z.interactable = true;
        }
    }
    public void ActivePanelAlert()
    {
        panelAlert.SetActive(true);
    }

    public void ActiveFinishPanel()
    {
        finishPanel.SetActive(true);
    }
    
    public void ActiveNoYetPanel()
    {
        noyetPanel.SetActive(true);
    }
}
