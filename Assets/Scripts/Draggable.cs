using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{

    protected Vector3 lastMousePos;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 deltaPos = newMousePos - lastMousePos;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position += deltaPos;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            transform.Rotate(Vector3.forward, deltaPos.x * -37);
        }
        lastMousePos = newMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        restartPhysics();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        restartPhysics();
    }

    // I want to be sure that the object gets returned to the simulation when the mouse is released,
    // regardless of whether it was clicked or dragged,
    // so this gets called from OnPointerUp and OnEndDrag
    protected void restartPhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    //private void OnMouseDown()
    //{

    //}

    //private void OnMouseDrag()
    //{
    //    Debug.Log("Clicked!");
    //}

    //private void OnMouseUp()
    //{
    //    Debug.Log("Released!");
    //}


    //private void OnMouseDown(MouseDownEvent evt)
    //{
    //    bool leftMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.LeftMouse));
    //    bool rightMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.RightMouse));
    //    bool middleMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.MiddleMouse));

    //    VisualElement targetElement = (VisualElement)evt.target;
    //    Debug.Log($"Mouse Down event. Triggered by {(MouseButton)evt.button} over element '{targetElement.name}'");
    //    Debug.Log($"Pressed buttons: Left button: {leftMouseButtonPressed} Right button: {rightMouseButtonPressed} Middle button: {middleMouseButtonPressed}");
    //}


    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Right)
    //        Debug.Log("Right button pressed.");
    //}
}
