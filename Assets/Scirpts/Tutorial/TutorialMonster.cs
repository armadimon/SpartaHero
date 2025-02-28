using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonster : BaseController
{
    ResourceController resourceController;
    public ResourceController Player;
    private void Start()
    {
        resourceController = GetComponent<ResourceController>();
        Player = GetComponent<ResourceController>();
    }

    private void Update()
    {
        Death();
    }


    public override void Death()
    {
        if (resourceController.CurrentHealth <= 0)
        {
            Player.GetExp(10);
            base.Death();
        }
            
    }

}
