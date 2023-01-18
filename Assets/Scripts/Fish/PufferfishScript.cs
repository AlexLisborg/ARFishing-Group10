using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferfishScript : MonoBehaviour
{
    private void Start()
    {
        Deflate();
    }
    public void Inflate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void Deflate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
