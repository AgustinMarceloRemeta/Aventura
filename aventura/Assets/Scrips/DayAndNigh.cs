using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNigh : MonoBehaviour
{
    public GameObject luz;
    IStatus status;
    public float velocity;
    void Start()
    {
        status = FindObjectOfType<IStatus>();
      
    }

    // Update is called once per frame
    void Update()
    {
        luz.transform.Rotate(velocity,0,0);
    }
}
