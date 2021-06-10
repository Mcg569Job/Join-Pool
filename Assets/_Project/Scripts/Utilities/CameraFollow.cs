using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 _followingDistance;
    [SerializeField] [Range(1, 5)] private float _speed = 4;
    private Transform _target;
      
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = _target.position + _followingDistance;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameStatNormal()) return;

        if (_target!=null)
        transform.position = Vector3.Lerp(transform.position, _target.position + _followingDistance, Time.deltaTime * _speed);
    }

    public void CalculateCameraPosition(int count)
    {
        float y = 8;
        float z = -8;
        for (int i = 0; i < count - 1; i++)
        {
            y += 1.2f;
            z += -1.2f;
        }

        _followingDistance.y = y;
        _followingDistance.z = z;

    }

}
