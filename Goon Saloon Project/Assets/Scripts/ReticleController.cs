using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public Transform controller; 
    public RectTransform reticle; 
    public Canvas canvas; 

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(controller.position);
        
        reticle.position = screenPoint;
    }
}

