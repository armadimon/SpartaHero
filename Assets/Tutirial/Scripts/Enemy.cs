using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] GameObjects;

    private ResourceController Player;
    private TutorialMonster[] tutorialMonsters;


    private bool[] bools = { false, false, false };

    private void Start()
    {
        if (GameObjects[0].GetComponent<ResourceController>() != null)
        {
            Player = GameObjects[0].GetComponent<ResourceController>();
        }
        else
        {
            Debug.Log("null");
        }

        tutorialMonsters = new TutorialMonster[GameObjects.Length -1];

        for (int i = 1; i < GameObjects.Length; i++)
        {
            tutorialMonsters[i-1] = GameObjects[i].GetComponent<TutorialMonster>();
        }
    }




















}
