using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Extended_Button_Options : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onClickDown;
    public UnityEvent onClickUp;
    bool buttonPressed = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonPressed)
        {
            onClickDown.Invoke();
        }
        else
        {
            onClickUp.Invoke();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
