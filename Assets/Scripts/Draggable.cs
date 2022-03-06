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
            HandleLeftDrag(deltaPos);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            HandleRightDrag(deltaPos);
        }
        lastMousePos = newMousePos;
    }

    private void HandleLeftDrag(Vector3 deltaPos)
    {
        transform.position += deltaPos;
    }

    private void HandleRightDrag(Vector3 deltaPos)
    {
        Vector3 localCenter = GetComponent<Rigidbody2D>().centerOfMass;
        Vector3 center = transform.position + transform.TransformDirection(localCenter);
        transform.RotateAround(center, Vector3.forward, deltaPos.x * -37);
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
}
