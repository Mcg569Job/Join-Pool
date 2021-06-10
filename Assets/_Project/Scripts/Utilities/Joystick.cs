using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [System.Serializable] public class PointerDownEvent : UnityEvent { }
    [System.Serializable] public class PointerUpEvent : UnityEvent { }

    public PointerDownEvent _downEvent;
    public PointerUpEvent _upEvent;

    public bool isDrag;
    public Vector2 Direction;
    private Vector2 _startPosition;
    private float _joystickArea;

    private void Awake()
    {
        _joystickArea = Screen.width / 10;

        LifebuoyPlayer player = GameObject.FindGameObjectWithTag("Player").GetComponent<LifebuoyPlayer>();
        _downEvent.AddListener(player.MovementStarted);
        _downEvent.AddListener(GameManager.Instance.StartGame);
        _upEvent.AddListener(player.Idle);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 temp = (eventData.position - _startPosition);

        Direction = new Vector2(
            Mathf.Clamp(temp.x / _joystickArea, -1, 1),
            Mathf.Clamp(temp.y / _joystickArea, -1, 1));
        
        DrawUI();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
        _startPosition = eventData.position;
        _downEvent.Invoke();   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        _startPosition = Vector2.zero;
        Direction = Vector2.zero;
        _upEvent.Invoke();
    }

    public Image joyBack,joy;
    private void DrawUI()
    {
        joyBack.rectTransform.position = _startPosition;
        joyBack.transform.localScale = new Vector3(_joystickArea/50,_joystickArea/50) ;
        joy.rectTransform.localPosition = new Vector3(_joystickArea/4,_joystickArea/4) * Direction;
    }


}
