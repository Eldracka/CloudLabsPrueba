using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;




namespace DragNDrop
{
    public class DragHandlerDragandDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static GameObject objBeingDraged;

        private Vector3 startPosition;
        private Transform startParent;
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        public Transform itemDraggerParent;
        public string id;
        public Transform trasformparent;

        private void Start()

        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

            objBeingDraged = gameObject;

            startPosition = transform.position;
            startParent = transform.parent;
            transform.SetParent(itemDraggerParent);

            canvasGroup.blocksRaycasts = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            objBeingDraged = null;

            canvasGroup.blocksRaycasts = true;
            if (transform.parent == itemDraggerParent)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
            }
        }

        public void StartPosition()
        {
            startParent = transform.parent;
            transform.SetParent(trasformparent);
        }
        
    }

}

