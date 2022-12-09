using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    float x = 0;
    float speed = 0.001f;
    [SerializeField] GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = x + (float)speed;
        transform.position = new Vector3(transform.position.x, -0.9f + Mathf.Sin(x), transform.position.z); //* Mathf.Sin(3*x) * Mathf.Sin((float)(0.5*x)) 
        bar.GetComponent<ElevationSlider>().setValue((Mathf.Sin(x) + 1) * 50);
    }
}
