using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{

    [SerializeField] private Color32[] colors;
    void Start()
    {
        var meshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material.color = colors[Random.Range(0,colors.Length)];
    }

   
   
}
