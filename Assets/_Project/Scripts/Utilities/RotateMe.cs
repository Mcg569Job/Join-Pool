using UnityEngine;

public class RotateMe : MonoBehaviour
{
    [SerializeField] [Range(-10, 10)] private float _speed;
    [SerializeField] private bool x, y, z;
    void Update()
    {
        transform.Rotate(
            x ? _speed * Time.deltaTime * 90 : 0,
            y ? _speed * Time.deltaTime * 90 : 0,
            z ? _speed * Time.deltaTime * 90 : 0
            );
    }
}
