using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField]private Light DirLight;
    [SerializeField] private LightPreset Preset;
    [SerializeField, Range(0, 1500)] public float TimeOfDay;

    private void Update()
    {
        if(Preset == null)
        {
            return;
        }
        if (Application.isPlaying) {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 1500;
            UpdateLight(TimeOfDay/ 1500f);
        } else
        {
            UpdateLight(TimeOfDay/ 1500f);
        }
    }
    private void UpdateLight( float timePerce)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePerce);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePerce);
        if (DirLight != null) { 
            DirLight.color = Preset.DirectionalColor.Evaluate(timePerce);
            DirLight.transform.localRotation = Quaternion.Euler(new Vector3((timePerce * 360f)-90f, 170f, 0) );
        }
    }
    private void OnValidate()
    {
        if (DirLight != null) { 
            return;
        }
        if (RenderSettings.sun != null)
        {
            DirLight = RenderSettings.sun;
        } else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (var item in lights)
            {
                if(item.type == LightType.Directional)
                {
                    DirLight = item;
                }
            }
        }
    }
}
