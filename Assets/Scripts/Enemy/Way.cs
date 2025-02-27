using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Way : MonoBehaviour
{
    public List<Transform> directionOfPath = new List<Transform>();
    [SerializeField]
    private bool alwaysDrawPath;
    [SerializeField]
    private bool drawAsLoop;
    [SerializeField]
    private bool drawNumbers;
    public Color debugColour = Color.white;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (alwaysDrawPath)
        {
            DrawPath();
        }
    }
    public void DrawPath()
    {
        for (int i = 0; i < directionOfPath.Count; i++)
        {
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 30;
            labelStyle.normal.textColor = debugColour;
            if (drawNumbers)
                Handles.Label(directionOfPath[i].position, i.ToString(), labelStyle);
            //Draw Lines Between Points.
            if (i >= 1)
            {
                Gizmos.color = debugColour;
                Gizmos.DrawLine(directionOfPath[i - 1].position, directionOfPath[i].position);

                if (drawAsLoop)
                    Gizmos.DrawLine(directionOfPath[directionOfPath.Count - 1].position, directionOfPath[0].position);

            }
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (alwaysDrawPath)
            return;
        else
            DrawPath();
    }
#endif
}
