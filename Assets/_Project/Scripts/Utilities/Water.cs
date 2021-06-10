using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] [Range(.001f, .2f)] private float _speed= .02f;
    [SerializeField] [Range(.001f, .2f)] private float _maxOffset= 2f;
    Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float _offset = Mathf.PingPong(Time.time * _speed,_maxOffset);
        _renderer.material.SetTextureOffset("_MainTex",new Vector2(_offset,_offset/2));
    }
}
