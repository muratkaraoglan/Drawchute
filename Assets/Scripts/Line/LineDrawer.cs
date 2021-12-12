using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public List<Vector3> points =  new List<Vector3>();

    private LineRenderer lineRenderer;
    private Camera mainCamera;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
    }

    public void CreateLine(Vector3 pos)
    {
        points.Clear();
        lineRenderer.positionCount = 0;
        pos = mainCamera.ScreenToWorldPoint(pos + new Vector3(0, 0, mainCamera.transform.position.z * -1));
        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, pos);
        points.Add(pos);
    }

    public void UpdateLine(Vector3 pos)
    {
        pos = mainCamera.ScreenToWorldPoint(pos + new Vector3(0, 0, mainCamera.transform.position.z * -1));
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
        points.Add(pos);
    }
}
