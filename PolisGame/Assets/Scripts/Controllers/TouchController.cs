using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TouchController : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Vector2 _touchPosition;
    public Vector2 direction;
    public Vector2 rotation;

     public UnityAction OnPointerDownEvent;
     public UnityAction<Vector3,Vector3> OnPointerDragEvent;
     public UnityAction OnPointerUpEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.position - _touchPosition;
        direction = delta.normalized;
        rotation = delta.normalized;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent!=null)
        {
            OnPointerUpEvent.Invoke();
        }
        direction = Vector2.zero;
    }
}
