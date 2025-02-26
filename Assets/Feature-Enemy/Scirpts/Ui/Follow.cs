using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform transforms;

    Transform Target;

    HUD HUD;
    public enum Who { Player, Enemy}
    public Who who;
    void Start()
    {
        HUD = GetComponent<HUD>();
        transforms = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(Target != null)
        {
            if(HUD.uitype == HUD.Uitype.HealthBar)
            transforms.position  = Camera.main.WorldToScreenPoint(Target.transform.position + new Vector3(0,1f,0));
            if (HUD.uitype == HUD.Uitype.EXP)
             transforms.position = Camera.main.WorldToScreenPoint(Target.transform.position + new Vector3(0, 0.75f, 0));
        }
    }

    public void SetTarget(Transform transform)
    {
        Target = transform;
    }

    

        
}
