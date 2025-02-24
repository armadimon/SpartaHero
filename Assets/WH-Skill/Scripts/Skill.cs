using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Player player;

    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<string> Skillname = new List<string>();
    private List<string> SKilldescrt = new List<string>();
    private List<SkillSet> Skills = new List<SkillSet>();

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
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
        List<SkillSet> skills = new List<SkillSet>();
        System.Random random = new System.Random();

        List<int> list = new List<int>();
        List<int> lists = new List<int>();


        //수정이 필요함
        bool IsHaveMelee = player.transform.Find("Mogie").GetComponent<MeleeWeaponHandler>() != null;
        bool IsHaveRange = player.transform.Find("Mogie2").GetComponent<RangeWeaponHandler>() != null;

        foreach (SkillSet skill in Skills)
        {
            //내부에서 파악
            if (skill.type == SkillType.Melee && !IsHaveMelee)
                continue;
            if (skill.type == SkillType.Range && !IsHaveRange)
                continue;
            skills.Add(skill);
        }

        list = Enumerable.Range(0, skills.Count).ToList();
        list = list.OrderBy(x => random.Next()).ToList();


        if (skills.Count  < 3) 
        {
            for(int i = 0; i < Buttons.Count - skills.Count; i++) 
            {
                Buttons[i].onClick.AddListener(() => skills[0].Action());
                Images[i].sprite = skills[0].Image.sprite;
                Skillname[i] = skills[0].name;
                SKilldescrt[i] = skills[0].description;
            }
            lists = list.Take(skills.Count).ToList();
            for (int i = 0+ Buttons.Count - skills.Count; i < Buttons.Count; i++)
            {
                Buttons[i].onClick.AddListener(() => skills[lists[i]].Action());
                Images[i].sprite = skills[lists[i]].Image.sprite;
                Skillname[i] = skills[lists[i]].name;
                SKilldescrt[i] = skills[lists[i]].description;
            }
        }
        else
        {
            lists = list.Take(3).ToList();

            for(int i = 0; i < Buttons.Count; i++) 
            {
                Buttons[i].onClick.AddListener(() => skills[lists[i]].Action());
                Images[i].sprite = skills[lists[i]].Image.sprite;
                Skillname[i] = skills[lists[i]].name;
                SKilldescrt[i] = skills[lists[i]].description;
            }
        }

  

    }

    private void SkillCreate()
    {
        Skills.Add(new SkillSet(SkillType.Default, 0, "체력 회복", "체력이 증가해요", images[0], 50, HealthUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackCountUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "공격력 증가", "공격력이 증가해요", images[0], 5, AttackCountUp));
    }

    private void Cut()
    {
        foreach (SkillSet skill in Skills)
        {

            if (skill.Level >= 3)
            {
                Skills.Remove(skill);
            }
        }
    }

    //
    private void AttackUp()
    {

    }
    private void HealthUp()
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
        public SkillType type;
        public int Level;
        public string name;
        public string description;
        public Image Image;
        public float value;
        public Action Action;

        public SkillSet(SkillType type, int Level, string name, string description, Image image, float value, Action action = null)
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



