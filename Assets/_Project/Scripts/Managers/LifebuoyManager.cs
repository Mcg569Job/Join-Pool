using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebuoyManager : MonoBehaviour
{
    public static LifebuoyManager Instance = null;

    private LifebuoyNode _firstLifebuoyNode, _currentLifebuoy = null;
    [HideInInspector]public int LifebuoyCount;

    private CameraFollow _camera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main.GetComponent<CameraFollow>();
    }

    public void SetLifebuoyFirstNode(LifebuoyNode node)
    {
        _firstLifebuoyNode = node;
        LifebuoyCount = 1;
    }

    public void ConnectLifebuoy(LifebuoyNode node)
    {
        if (!GameManager.Instance.IsGameStatNormal()) return;
        if (coroutine != null) return;

        if (_currentLifebuoy == null)
        {
            _currentLifebuoy = _firstLifebuoyNode;
        }

        if (_currentLifebuoy == node || node.isConnected) return;

        if (_currentLifebuoy.nextNode == null)
        {
            _currentLifebuoy.Connect(node);
            _currentLifebuoy = node;
        }

        AudioManager.Instance.PlaySound(AudioType.Connect);
        AudioManager.Instance.Vibrate();

        UpdateLifebuoyCount(1);
        StartCoroutine(ConnectAnimation());
    }

    Coroutine coroutine = null;
    public void DisconnectLifebuoy(LifebuoyNode node)
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(DisconnectLifebuoyEnum(node));
    }

    private IEnumerator DisconnectLifebuoyEnum(LifebuoyNode node)
    {
        if (GameManager.Instance.IsGameStatNormal())
        {
            LifebuoyNode temp = node;

            if (node.previousNode != null)
            {
                _currentLifebuoy = node.previousNode;
                _currentLifebuoy.nextNode = null;
                _currentLifebuoy.DisableRope();
            }

            while (temp != null)
            {
                AudioManager.Instance.PlaySound(AudioType.Disconnect);
                AudioManager.Instance.Vibrate();

                if (temp.isConnected)
                    UpdateLifebuoyCount(-1);

                temp.isConnected = false;
                temp.transform.localScale *= 1.3f;

                yield return new WaitForSeconds(0.05f);

                temp.gameObject.SetActive(false);

                if (temp.nextNode != null)
                    temp = temp.nextNode;
                else
                    temp = null;

            }

          

        }
        coroutine = null;

    }

       private void UpdateLifebuoyCount(int value)
    {
        LifebuoyCount += value;
        _camera.CalculateCameraPosition(LifebuoyCount);

        UIManager.Instance.UpdateCountText(LifebuoyCount);

        if (LifebuoyCount <= 0)
        {
            GameManager.Instance.GameOver();
        }

        //  Debug.Log("Lifebuoy count: " + LifebuoyCount);
    }

    private IEnumerator ConnectAnimation()
    {
        LifebuoyNode temp = _firstLifebuoyNode;
        while (true)
        {
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * 12;
                temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, new Vector3(1.25f, 1.25f, 1.25f), t);
                yield return null;
            }
            t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * 12;
                temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, new Vector3(1, 1, 1), t);
                yield return null;
            }

            if (temp.nextNode != null)
                temp = temp.nextNode;
            else
                break;

            yield return null;
        }
    }

}
