using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    private LineRenderer lr;

    public void DrawLine(Vector2 pos1, Vector2 pos2)
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.positionCount = 2;
        lr.SetPosition(0, pos1);
        lr.SetPosition(1, pos2);
        lr.useWorldSpace = true;
    }
}
