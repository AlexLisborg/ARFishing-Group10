using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInputManager : MonoBehaviour
{
    [SerializeField] GameObject Pool;
    [SerializeField] LayerMask HitLayer;
    [SerializeField] GameObject Text;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, HitLayer))
            {
                
            }
        }

    }
    
}
