using System.Collections;
using UnityEngine;


public class LifebuoyPlayer : MonoBehaviour
{
    [Header("SPEED")]
    [SerializeField] [Range(1, 10)] private float _speed = 5;

    [Header("FX")]
    [SerializeField] private GameObject _idleFX;
    [SerializeField] private GameObject _moveFX;

    private Rigidbody _rigidbody;
    private Joystick _joystick;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joystick = GameObject.FindObjectOfType<Joystick>();

        LifebuoyManager.Instance.SetLifebuoyFirstNode(GetComponent<LifebuoyNode>());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            StartCoroutine(GameFinished());
        }
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            Data.Instance.AddCoin(10);
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameStatNormal()) return;

        if (_joystick != null)
        {
            if (_joystick.isDrag)
            {
                _rigidbody.velocity = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y) * _speed;
                transform.eulerAngles = new Vector3(0, 90 + Mathf.Atan2(-_joystick.Direction.y, _joystick.Direction.x) * 180 / Mathf.PI, 0) ;
            }
            else
            {
                if (GameManager.Instance.IsGameStatNormal())
                    _rigidbody.velocity *= 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "LifebuoyNode")
        {
            LifebuoyManager.Instance.ConnectLifebuoy(collision.transform.GetComponent<LifebuoyNode>());
        }
    }



    public void Idle()
    {
       // if (!GameManager.Instance.IsGameStatNormal()) return;

        _idleFX.SetActive(true);
        _moveFX.SetActive(false);
    }
    public void MovementStarted()
    {
      //  if (!GameManager.Instance.IsGameStatNormal()) return;

        _idleFX.SetActive(false);
        _moveFX.SetActive(true);
    }

    private IEnumerator GameFinished()
    {
        GameManager.Instance.FinishGame();
      
        transform.rotation = Quaternion.identity;
               
        _rigidbody.velocity = transform.forward * _speed * 5;

        yield return new WaitForSeconds(10);
      
        _rigidbody.velocity *= 0;

        yield return null;
    }

}
