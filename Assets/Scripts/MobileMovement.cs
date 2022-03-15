using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _joystickBG;
    private Image _handler;
    private Vector2 _input;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    private void Start()
    {
        _joystickBG = GetComponent<Image>();
        _handler = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _input = Vector2.zero;
        _handler.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 position;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBG.rectTransform, ped.position, ped.pressEventCamera, out position))
        {
            position.x = (position.x / _joystickBG.rectTransform.sizeDelta.x);
            position.y = (position.y / _joystickBG.rectTransform.sizeDelta.x);

            _input = new Vector2(position.x, position.y);
            _input = (_input.magnitude > 1) ? _input.normalized : _input;

            _handler.rectTransform.anchoredPosition = new Vector2(_input.x * (_joystickBG.rectTransform.sizeDelta.x), _input.y * (_joystickBG.rectTransform.sizeDelta.y));
        }
    }

    public float Horizontal()
    {
        if (_input.x != 0)
            return _input.x;
        else
            return Input.GetAxis(_horizontal);
    }

    public float Vertical()
    {
        if (_input.y != 0)
            return _input.y;
        else
            return Input.GetAxis(_vertical);
    }
}
