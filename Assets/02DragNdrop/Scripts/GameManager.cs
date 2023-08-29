using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DragNDrop
{
    public class GameManager : MonoBehaviour
    {
        public List<DataJson> dataStudents;
        private GetFromJson getFromJson = new GetFromJson();
        [Tooltip("es verdadero parent")]
        public Transform dragparent;
        [Tooltip("es el dummy parent")]
        public Transform itemDraggerParent;
        
        public GameObject prefabDrag;
        public GameObject prefabSlot;
        public Transform aprobadosPanel;
        public Transform reprobadosPanel;

        public GameObject panelItsOk;
        public GameObject panelBadRepeat;
        private void Start()
        {
            dataStudents = getFromJson.studentsList();
            CreateDragItems();
            CrateSlotItems();
        }

        private void CreateDragItems()
        {
            foreach (var students in dataStudents)
            {
                var x = Instantiate(prefabDrag);
                x.transform.SetParent(dragparent);
                x.GetComponent<DragHandlerDragandDrop>().trasformparent = dragparent;
                x.GetComponent<DragHandlerDragandDrop>().itemDraggerParent = itemDraggerParent;
                x.GetComponent<DragHandlerDragandDrop>().id = students.aprobado.ToLower();
                //x.GetComponent<TextMeshProUGUI>().text = students.nombre + " " + students.apellido;
                x.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = students.nombre + " " + students.apellido;
            }
        }

        private void CrateSlotItems()
        {
            foreach (var student in dataStudents)
            {
                var s = Instantiate(prefabSlot);
                if (student.aprobado.ToLower() == "aprobado")
                {
                    s.transform.SetParent(aprobadosPanel);
                    s.GetComponent<DropSlotDragandDrop>().id = "aprobado";
                }
                else
                {
                    s.transform.SetParent(reprobadosPanel);
                    s.GetComponent<DropSlotDragandDrop>().id = "reprobado";
                }
            }
        }

        public void CheckAnswers()
        {
            var dropsUsed = FindObjectsOfType<DropSlotDragandDrop>();
            var i = 0;
            
            foreach (var x in dropsUsed)
            {
                if (x.GetComponentInChildren<DragHandlerDragandDrop>() != null)
                {
                   i++;
                }
            }

            if (i == dropsUsed.Length)
            {
                int badAnswer = 0;
                foreach (var z in dropsUsed)
                {
                    if (z.IsCorrect == false)
                    {
                        z.GetComponentInChildren<DragHandlerDragandDrop>().StartPosition();
                        badAnswer++;
                    }
                }

                if (badAnswer>0)
                {
                    panelBadRepeat.SetActive(true);
                }
                else
                {
                    panelItsOk.SetActive(true);
                }
            }
            
        }

        public void sceneLoad(string namescene)
        {
            SceneManager.LoadScene(namescene);
        }
    }
}
