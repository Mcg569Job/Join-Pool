using UnityEngine;

public class MovingObject : MonoBehaviour
{
    #region variables
  
    [Range(0, 5)] public float speedX = 1.25f;
    [Range(0, 10)] public float motionRangeX = .5f;

    private Vector2 _start;
    private Vector2 _pos;
    private Vector3 _vec;
    private int  _dirX = 1;

    #endregion

    #region Start
    private void Start()=>  _start = transform.position;    
    #endregion

    #region Move
    public void Update() => UpdatePosition();
    private void UpdatePosition()
    {
        _pos = transform.position;
       
        if (_pos.x > _start.x + motionRangeX) _dirX = -1;
        else if (_pos.x <= _start.x - motionRangeX) _dirX = 1;
        
        _vec.x = speedX * _dirX * Time.deltaTime;

        transform.position += _vec;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 a = transform.position;
        a.x = a.x - motionRangeX;
        Vector3 b = transform.position;
        b.x = b.x + motionRangeX;
        
        Gizmos.DrawLine(a, b);
        Gizmos.DrawWireCube(a,Vector3.one /2) ;
        Gizmos.DrawWireCube(b, Vector3.one/2) ;
        
    }

}
