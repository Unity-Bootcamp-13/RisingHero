using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 inputVector;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= joystickBackground.sizeDelta.x;
            pos.y /= joystickBackground.sizeDelta.y;

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickHandle.anchoredPosition = new Vector2(
                inputVector.x * (joystickBackground.sizeDelta.x / 2),
                inputVector.y * (joystickBackground.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
    public Vector2 Direction() => inputVector;
}
