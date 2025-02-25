using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoHit : MonoBehaviour
{
    public AutoHit autoHit;
    public GameObject hitObject;
    public Target targeting;



    protected float DistanceToTarget() //두 포지션간의 거리계산
    {

        return Vector3.Distance(transform.position, targeting.position);
    }
}
