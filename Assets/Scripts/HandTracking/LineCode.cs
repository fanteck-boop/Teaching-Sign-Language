using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCode : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform origin;
    public Transform destination;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineRenderer != null)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 1.0f;
        }
    }

    void Update()
    {
        if (lineRenderer != null && origin != null && destination != null)
        {
            lineRenderer.SetPosition(0, origin.position);
            lineRenderer.SetPosition(1, destination.position);

        }
    }
}