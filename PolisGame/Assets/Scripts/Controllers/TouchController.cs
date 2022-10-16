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
        if (OnPointerDownEvent == null) return;
        OnPointerDownEvent.Invoke();
        _touchPosition = eventData.position;
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (OnPointerDragEvent != null)
        {
            var delta = eventData.position - _touchPosition;
            direction = delta.normalized;
            rotation = delta.normalized;
            OnPointerDragEvent.Invoke (direction,rotation) ;
            Debug.Log("drag çalışıyor");
        }
       
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector2.zero;
        OnPointerDragEvent.Invoke (direction,rotation) ;
        
        
    }
}
