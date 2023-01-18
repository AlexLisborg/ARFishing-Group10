using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectonPlane : MonoBehaviour
{
    public GameObject placeablePrefabe;
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;
    private Vector2 _touchPosition;

    
    private bool _isActive;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _isActive = true;
        raycastManager = GetComponent<ARRaycastManager>();

    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    private void Update()
    {
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        
       if(raycastManager.Raycast(touchPosition , hits , TrackableType.PlaneWithinPolygon))
        {
            
            var hitsPosition = hits[0].pose;
            if (spawnedObject == null)
            {
                
                spawnedObject = Instantiate(placeablePrefabe, hitsPosition.position, hitsPosition.rotation);
                if(spawnedObject.GetComponent<ARAnchor>() == null)
                {
                    
                    spawnedObject.AddComponent<ARAnchor>();
                    ARAnchor aa = spawnedObject.GetComponent<ARAnchor>();
                    
                }
            }
            
        }
    }

    
}
