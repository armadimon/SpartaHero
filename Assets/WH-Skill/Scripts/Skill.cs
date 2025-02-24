using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] private GameObject Parent;
    private Player player;

    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<TextMeshProUGUI> Skillname = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> SKilldescrt = new List<TextMeshProUGUI>();
    private List<SkillSet> Skills = new List<SkillSet>();

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
            Images.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            Skillname.Add(Buttons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            SKilldescrt.Add(Buttons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>());
        }
        SkillCreate();

    }
    void Start()
    {
        SkipBut.onClick.AddListener(Skip);
       
  

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        Cut();
        SkillSelcet();
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
        bool IsHaveMelee = player.transform.Find("mogie").GetComponent<MeleeWeaponHandler>() != null;
        bool IsHaveRange = player.transform.Find("mogie2").GetComponent<RangeWeaponHandler>() != null;

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
                int index = i;
                Buttons[index].onClick.AddListener(() => skills[0].Action());
                Images[index].sprite = skills[0].Image.sprite;
                Skillname[index].text = skills[0].name;
                SKilldescrt[index].text = skills[0].description;
            }
            lists = list.Take(skills.Count).ToList();
            for (int i = 0+ Buttons.Count - skills.Count; i < Buttons.Count; i++)
            {
                int index = i;
                Buttons[index].onClick.AddListener(() => skills[lists[index]].Action());
                Images[index].sprite = skills[lists[index]].Image.sprite;
                Skillname[index].text = skills[lists[index]].name;
                SKilldescrt[index].text = skills[lists[index]].description;
                skills[lists[index]].Level++;
            }
        }
        else
        {
            lists = list.Take(3).ToList();

            for(int i = 0; i < Buttons.Count; i++) 
            {
                int index = i;
                Buttons[index].onClick.AddListener(() => skills[lists[index]].Action());
                Images[index].sprite = skills[lists[index]].Image.sprite;
                Skillname[index].text = skills[lists[index]].name;
                SKilldescrt[index].text = skills[lists[index]].description;
                skills[lists[index]].Level++;
            }
        }

  

    }

    private void SkillCreate()
    {
        Skills.Add(new SkillSet(SkillType.Default, 0, "healthup", "healthup healthup", images[0], 50, HealthUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackUp", "AttackUp", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackSpeedUp", "AttackSpeedUp", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackRangeUp", "AttackRangeUp", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackRangeUp", "AttackRangeUp", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackDelayDown", "AttackDelayDown", images[0], 5, AttackUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "AttackDelayDown", "AttackDelayDown", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "AttackRangeUp", "AttackRangeUp", images[0], 5, AttackSpeedUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "AttackRangeUp", "AttackRangeUp", images[0], 5, AttackCountUp));
        Skills.Add(new SkillSet(SkillType.Range, 0, "AttackDelayDown", "AttackDelayDown", images[0], 5, AttackCountUp));
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

        Parent.gameObject.SetActive(false);
    }
    private void HealthUp()
    {
        Parent.gameObject.SetActive(false);
    }
    private void AttackSpeedUp()
    {
        Parent.gameObject.SetActive(false);
    }
    private void AttackRangeUp()
    {
        Parent.gameObject.SetActive(false);
    }
    private void AttackDelayDown()
    {
        Parent.gameObject.SetActive(false);

    }
    private void AttackCountUp()
    {
        Parent.gameObject.SetActive(false);
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



