using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.EventSystems;

public class PlaceObjects : MonoBehaviour
{
    

    public GameObject[] objects;
    public GameObject[] buttons;
    public GameObject placementIndicator;
    private ARSessionOrigin arSessionOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool placementIndicatorEnabled = true;
    int objIndex; 
    

    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {

        if (placementIndicatorEnabled == true)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }

    public void PlaceObject()
    {

        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < buttons.Length; i++)
        {

            if (buttons[i].name == buttonName)
            {
                objIndex = i;
            }
        }
        objects[objIndex].SetActive(true);
        objects[objIndex].transform.position = placementPose.position;
        objects[objIndex].transform.rotation = placementPose.rotation;
    }



    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);

            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }

            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);

            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }

        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // uses local variables to create a array of hits from the camera to plane
        var hits = new List<ARRaycastHit>(); // so we can track how many hit 



        arSessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);
        // starts the Raycasting and starts to track the plane

        placementPoseIsValid = hits.Count > 0; 
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            // 
        }

    }
}