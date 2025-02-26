using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform transform;
    public enum Who { Player, Enemy}
    public Who who;
    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (who)
        {
            case Who.Player:
                transform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position + new Vector3(-0.9f, 0.6f, 0f));
                break;
        }
    }

        
}
