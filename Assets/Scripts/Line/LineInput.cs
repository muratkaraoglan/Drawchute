using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInput : MonoBehaviour
{
    public Canvas myCanvas;
    public Parachute parachute;
    private LineDrawer lineDrawer;
    float xMin, xMax;
    float yMin, yMax;
    Vector3 lastPos;
    private void Start()
    {
        xMin = -23f;
        xMax = 23f;
        yMin = -78f;
        yMax = -65f;
        print($"xMin: {xMin}, xMax: {xMax}, yMin: {yMin}, yMax: {yMax}");
        lineDrawer = GetComponent<LineDrawer>();
    }

    void Update()
    {
        if ( IsMouseInTuval(GetMousePosition()) )
        {
            if ( Input.GetMouseButtonDown(0) )
            {
                Vector3 startPos = Input.mousePosition;      
                lineDrawer.CreateLine(startPos);
                lastPos = startPos;
            }
            if ( Input.GetMouseButton(0) )
            {
                Vector3 tempPos = Input.mousePosition;
                if ( Vector3.Distance(lastPos, tempPos) > 0.3f )
                {
                    lineDrawer.UpdateLine(tempPos);
                }
            }
            if ( Input.GetMouseButtonUp(0) )
            {
                parachute.GenerateMeshFromPoints(lineDrawer.points);
            }
        }
    }

    private bool IsMouseInTuval(Vector3 mousePos)
    {
        if ( mousePos.x >= xMin && mousePos.x <= xMax && mousePos.y >= yMin && mousePos.y <= yMax )
        {
            return true;
        }
        return false;
    }

    Vector3 GetMousePosition()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myCanvas.transform as RectTransform,
            Input.mousePosition, myCanvas.worldCamera,
            out movePos);
        Vector3 positionToReturn = myCanvas.transform.TransformPoint(movePos);
        positionToReturn.z = myCanvas.transform.position.z - 0.01f;
        //print(positionToReturn);
        return positionToReturn;
    }

}
