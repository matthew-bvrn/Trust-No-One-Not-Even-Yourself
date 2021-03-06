﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public string resourceType;

    public float bobYOffset;
    float absoluteY;
    public bool rotateActive=true;
    public float bobSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        absoluteY = transform.position.y;
        bobYOffset = 0;

        if (transform.tag != "Flag")
        {
            {
                if (resourceType == "matter")
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f));
                }
                else if (resourceType == "force")
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.3f, 0.3f));
                }
                else if (resourceType == "smarts")
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(0.3f, 0.3f, 1.0f));
                }
                else if (resourceType == "motion")
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f));
                }
                else
                {
                    throw new System.Exception("Invalid Resource Type! The object " + transform.name + " tried to access the colour of " + resourceType + ".");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Bob();
    }

    void Bob()
    {
        bobYOffset += Time.deltaTime*bobSpeed;
        bobYOffset = bobYOffset % (Mathf.PI * 2);
        transform.position = new Vector3(transform.position.x,absoluteY+Mathf.Sin(bobYOffset)/7f,transform.position.z);
        if (rotateActive)
        {
            transform.Rotate(0, Time.deltaTime * 10f, 0);
        }
    }
}
