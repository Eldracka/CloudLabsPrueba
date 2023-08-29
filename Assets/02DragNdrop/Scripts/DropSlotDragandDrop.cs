using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragNDrop
{
    public class DropSlotDragandDrop : MonoBehaviour, IDropHandler
    {
        public GameObject item;
        public string id;
        public bool IsCorrect;
        public int childLimit;
       
        public void OnDrop(PointerEventData eventData)
        {
            if (!item && transform.childCount == ( 2 + childLimit))
            {
                item = DragHandlerDragandDrop.objBeingDraged;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
            }

            if (eventData.pointerDrag.GetComponent<DragHandlerDragandDrop>().id == id)
            {
                IsCorrect = true;
                item.transform.position = transform.position;
            }
            else
            {
                IsCorrect = false;
            }
        }

        private void Update()
        {
            if (item != null && item.transform.parent != transform)
            {
                item = null;
            }
        }


    }

}
