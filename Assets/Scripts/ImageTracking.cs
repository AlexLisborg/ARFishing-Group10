using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracking : MonoBehaviour
{
    private ARTrackedImageManager _arManager;

    void Awake()
    {
        _arManager = FindObjectOfType<ARTrackedImageManager>();
    }
    public void OnEnable()
    {
        _arManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
             Debug.Log(trackedImage.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
