using UnityEngine;

public class LifebuoyNode : MonoBehaviour
{
    [HideInInspector]
    public LifebuoyNode previousNode;
    [HideInInspector]
    public LifebuoyNode nextNode;

    [SerializeField] public Transform connectionPoint;
    [HideInInspector] public bool isConnected = false;

    private Rigidbody _rigidbody;

    [SerializeField] private LineRenderer _rope;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        DisableRope();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            LifebuoyManager.Instance.DisconnectLifebuoy(this);
        }
    }

    public void Connect(LifebuoyNode node)
    {
        nextNode = node;
        node.previousNode = this;
        nextNode.transform.position = connectionPoint.position;
        node.ResetVelocity();
        EnableRope();
        isConnected = true;
    }

    private void FixedUpdate()
    {
      

        if (previousNode != null)
        {
            transform.localPosition = Vector3.Lerp(transform.position, previousNode.connectionPoint.position, Time.deltaTime * 4f);
            _rigidbody.rotation = Quaternion.Lerp(transform.rotation, previousNode.transform.rotation, Time.deltaTime * 4);
        }

        DrawRope();
  if (!GameManager.Instance.IsGameStatNormal()) return;

        if (nextNode != null)
        {
            float distance = Vector3.Distance(transform.position, nextNode.transform.position);
            if (distance >= 4.5f)
            {
                LifebuoyManager.Instance.DisconnectLifebuoy(nextNode);
                nextNode = null;
            }
        }

    }



    public void ResetVelocity() => _rigidbody.velocity *= 0;

    private void DrawRope()
    {
        if (connectionPoint == null || nextNode == null || !_rope.gameObject.activeSelf) return;

        Vector3 tmpStart = connectionPoint.position;
        Vector3 tmpEnd = nextNode.transform.position + new Vector3(0, 0, .7f);
        Vector3 tmpMid = (tmpStart + tmpEnd) / 2;

        _rope.SetPosition(0, tmpStart);
        _rope.SetPosition(1, tmpMid);
        _rope.SetPosition(2, tmpEnd);
    }

    public void DisableRope() => _rope.gameObject.SetActive(false);
    private void EnableRope() => _rope.gameObject.SetActive(true);

}
