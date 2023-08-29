using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<DataJson> dataStudents;
    private GetFromJson getFromJson = new GetFromJson();
    private void Start()
    {
        dataStudents = getFromJson.studentsList();
        CheckAgain();
    }
    public string CheckSelection(int index)
    {
        if (dataStudents[index].nota >2.95)
        {
            return "aprobado";
        }
        else
        {
            return "reprobado";
        }
    }
    public void ChangeState(int index,string aprobado)
    {
        dataStudents[index].aprobado = aprobado;
        dataStudents[index].calificado = true;
        getFromJson.SaveDataJsonFile(dataStudents);
        
    }
    public void ReloadDataFromJson()
    {
        dataStudents.Clear();
        dataStudents = getFromJson.studentsList();
    }

    public void VerifyAllChecks()
    {
        int noCheck = 0; 
        var uiManager = GameObject.FindObjectOfType<UIManager>();
        foreach (var x in dataStudents)
        {
            if (x.calificado == false)
            {
                noCheck++;
            }
        }

        if (noCheck==0)
        {
            uiManager.ActiveFinishPanel();
        }
        else
        {
            uiManager.ActiveNoYetPanel();
        }
    }

    public void CheckAgain()
    {
        int calificados = 0;
        var uiManager = GameObject.FindObjectOfType<UIManager>();
        foreach (var x in dataStudents)
        {
            if (x.calificado)
            {
                calificados++;
            }
        }

        if (calificados == dataStudents.Count)
        {
            uiManager.checkAgainBtn.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
