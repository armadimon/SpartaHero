using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class AutoHit : MonoBehaviour

{
    public Transform targeting;
    public float scanRange;

    public LayerMask targetLayer;
    public Collider2D[] Enemy;

    public List<GameObject> targetsInRange = new List<GameObject>();

    private void FixedUpdate()
    {
        Enemy = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);
        targeting = GetNearest();
    }

    public Transform GetNearest()  //사정거리내 에너미 리스트
    { 
        Transform result = null;
        float diff = 50;
        foreach (Collider2D target in Enemy)
        {
            Vector3 mypos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(mypos, targetPos);
            if (curDiff > diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;


    }
  


}
