using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


enum SkillType
{ 
    Default,
    Melee,
    Range,
}


public class Skill : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Image[] images;
    [SerializeField] private Button SkipBut;

    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<string> Skillname = new List<string>();
    private List<string> SKilldescrt = new List<string>();
    private List<SkillSet> Skills = new List<SkillSet>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
            Images.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            Skillname.Add(Buttons[i].transform.GetChild(1).GetComponent<string>());
            SKilldescrt.Add(Buttons[i].transform.GetChild(2).GetComponent<string>());
        }

    }
    void Start()
    {
        SkipBut.onClick.AddListener(Skip);
        SkillCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Skip()
    {
        gameObject.SetActive(false);
    }

    private void SkillSelcet()
    {
        
       
    }

    private void SkillCreate()
    {
        Skills.Add(new SkillSet(SkillType.Default,0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default,0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default,0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default,0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default,0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0,"공격력 증가", "공격력이 증가해요", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackCountUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackCountUp));
    }

    private void Cut()
    {
        foreach (SkillSet skill in Skills) 
        {
            if(skill.Level >= 3)
            {
                Skills.Remove(skill);
            }
        }
    }

    //
    private void AttackUp()
    {

    }
    private void AttackSpeedUp()
    {

    }
    private void AttackRangeUp()
    {

    }
    private void AttackDelayDown()
    {

    }
    private void AttackCountUp()
    {

    }
   

    private class SkillSet
    {
        SkillType type;
        public int Level;
        string name;
        string description;
        Image Image;
        float value;
        Action Action;

        public SkillSet(SkillType type,int Level, string name, string description, Image image, float value, Action action = null)
        {
            this.type = type;
            this.Level = Level;
            this.name = name;
            this.description = description;
            Image = image;
            this.value = value;
            Action = action;
        }
    }



}



