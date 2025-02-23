using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Image[] images;
    [SerializeField] private Button SkipBut;

    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<string> Skillname = new List<string>();
    private List<string> SKilldescrt = new List<string>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
            Images.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            Skillname.Add(Buttons[i].transform.GetChild(1).GetComponent<string>());
            SKilldescrt.Add(Buttons[i].transform.GetChild(2).GetComponent<string>());
        }

        SkipBut.onClick.AddListener(Skip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Skip()
    {
        gameObject.SetActive(false);
    }

    //




}
