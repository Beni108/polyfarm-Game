using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDrop : MonoBehaviour
{
    private bool _dragging,_placed;
    private Vector2 _offset, _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }
    private void Update()
    {
        if (_placed) return;
        if (!_dragging) 
        {
            return;
        }
        var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input .mousePosition);
        transform.position = mousePosition- _offset;
    }
    private void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
        Debug.Log("drag");
    }
    private void OnMouseUp()
    {
        /*
        if (Vector2.Distance(transform.position) < 3) 
        {
            _placed = true;
        }
        */
        if (  false ) return; //temp
        else
        {
            transform.position = _originalPosition;
            _dragging = false;
        }
    }
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
