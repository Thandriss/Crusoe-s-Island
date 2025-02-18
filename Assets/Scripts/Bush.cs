using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private bool hasBerry = true;
    private LightManager lightManager;
    void Start()
    {
        lightManager = FindObjectOfType<LightManager>();
        if (lightManager == null)
        {
            Debug.LogError("LightManager not found in the scene.");
            return;
        }

    }

    private void Update()
    {
        if (lightManager.TimeOfDay <= 1f)
        {
            hasBerry = true;
        }
    }

    public bool TryTakeBerry()
    {
        if (hasBerry && !IsNightTime())
        {
            hasBerry = false;
            return true;
        }
        return false;
    }

    private bool IsNightTime()
    {
        return !(lightManager.TimeOfDay <= 1162 && lightManager.TimeOfDay >= 286);
    }
}
