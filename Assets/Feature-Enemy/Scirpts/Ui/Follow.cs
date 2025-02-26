using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform transforms;

    Transform Target;
    public enum Who { Player, Enemy}
    public Who who;
    void Start()
    {
        transforms = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(Target != null)
        {
            transforms.position  = Camera.main.WorldToScreenPoint(Target.transform.position + new Vector3(0,1f,0));
        }
    }

    public void SetTarget(Transform transform)
    {
        Target = transform;
    }

    

        
}
