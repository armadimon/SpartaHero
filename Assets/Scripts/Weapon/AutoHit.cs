using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoHit : MonoBehaviour
{
    public AutoHit autoHit;
    public GameObject hitObject;
    public Target targeting;



    protected float DistanceToTarget() //�� �����ǰ��� �Ÿ����
    {

        return Vector3.Distance(transform.position, targeting.position);
    }
}
