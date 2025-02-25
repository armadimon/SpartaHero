using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{

    public int boundcount = 0;
    public int Penetration = 0;


    public bool isFirst = true;
    public void Bounding(Rigidbody2D rigidbody, SpriteRenderer spriteRenderer)
    {
        if (boundcount > 0 && !isFirst)
        {
            rigidbody.velocity = -rigidbody.velocity;
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    public void StartSlow(Collider2D collider)
    {
        StartCoroutine(Slowro(collider));
    }

    private IEnumerator Slowro(Collider2D collider)
    {
        Transform Enemy = collider.transform;
        if (Enemy.GetComponent<StatHandler>() != null && Enemy.transform.childCount != 0)
        {
            Debug.Log("speedon");
            StatHandler statHandler = Enemy.GetComponent<StatHandler>();
            statHandler.Speed = 2f;
            Transform sprite = Enemy.transform.GetChild(1);
            sprite.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            statHandler.Speed = 3f;
            sprite.gameObject.SetActive(false);
            Debug.Log("Coloron");

        }
        else
        {
            Debug.Log("null");
        }
 
    }
}
