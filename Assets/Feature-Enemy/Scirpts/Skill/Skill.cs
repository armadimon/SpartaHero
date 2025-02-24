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


    [SerializeField] private Sprite[] images;
    [SerializeField] private Button SkipBut;
    [SerializeField] private GameObject Parent;
    [SerializeField] private Transform player;
    //private Player player;

    private Transform WeaponPivot;
    private List<WeaponHandler> handlers = new List<WeaponHandler>();



    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<TextMeshProUGUI> Skillname = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> SKilldescrt = new List<TextMeshProUGUI>();

    private List<SkillSet> Skills = new List<SkillSet>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
            Images.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            Skillname.Add(Buttons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            SKilldescrt.Add(Buttons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>());

        }

        WeaponPivot = player.GetChild(1);
        for (int i = 0;i < WeaponPivot.childCount;i++)
        {
            handlers.Add(WeaponPivot.GetChild(i).GetComponent<WeaponHandler>());
        }


        SkillCreate();



    }
    void Start()
    {

        //player = FindAnyObjectByType<Player>();

        SkipBut.onClick.AddListener(Skip);
        //수정되어야 함
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
    }

    // Update is called once per frame

    private void OnEnable()
    {
        Cut();
        SkillSelcet();
    }


    public void Skip()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SkillSelcet()
    {
        List<SkillSet> skills = new List<SkillSet>();
        System.Random random = new System.Random();

        List<int> list = new List<int>();
        List<int> lists = new List<int>();


        //수정이 필요함
        bool IsRange = WeaponPivot.GetChild(0).GetComponent<RangeWeaponHandler>() != null;
        bool IsMelee = WeaponPivot.GetChild(0).GetComponent<MeleeWeaponHandler>() != null;


        foreach (SkillSet skill in Skills)
        {
            //내부에서 파악
            if (skill.type == SkillType.Melee && IsMelee)
                continue;
            if (skill.type == SkillType.Range && IsRange)
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
                Images[index].sprite = skills[0].Image;
                Skillname[index].text = skills[0].name;
                SKilldescrt[index].text = skills[0].description;
            }
            lists = list.Take(skills.Count).ToList();
            for (int i = 0+ Buttons.Count - skills.Count; i < Buttons.Count; i++)
            {
                int index = i;
                Buttons[index].onClick.AddListener(() => skills[lists[index]].Action());
                Images[index].sprite = skills[lists[index]].Image;
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
                Images[index].sprite = skills[lists[index]].Image;
                Skillname[index].text = skills[lists[index]].name;
                SKilldescrt[index].text = skills[lists[index]].description;
                skills[lists[index]].Level++;
            }
        }

  

    }

    private void SkillCreate()
    {
        Skills.Add(new SkillSet(SkillType.Default, 0, "healthup", "healthup healthup", images[0], () => HealthUp(50)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackUp", "AttackUp", images[0], () => AttackUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "SpeedUp", "SpeedUp", images[0], () => SpeedUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackSpeedUp", "AttackSpeedUp", images[0], () => AttackSpeedUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackRangeUp", "AttackRangeUp", images[0], () => AttackRangeUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackDelayDown", "AttackDelayDown", images[0], () => AttackDelayDown(3)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "AttackCountUp", "AttackCountUp", images[0], () => AttackCountUp(1)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "BulletSizeup", "BulletSizeup", images[0], () => BulletSizeup(5)));
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
    private void AttackUp(int value)
    {
        handlers[0].Power += value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void HealthUp(int value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        player.GetComponent<StatHandler>().Health += value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void SpeedUp(float value)
    {
        player.GetComponent<StatHandler>().Speed += value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void AttackSpeedUp(float value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        handlers[0].Speed += value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void AttackRangeUp(float value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        handlers[0].AttackRange += value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void AttackDelayDown(float value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        handlers[0].Delay -= value;
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);

    }
    private void AttackCountUp(int value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        if (handlers[0] is RangeWeaponHandler)
        {
            RangeWeaponHandler rangeWeaponHandler = (RangeWeaponHandler)handlers[0];
            rangeWeaponHandler.NumberOfProjectilesPerShot += value;
        }
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void BulletSizeup(float value)
    {
        if (handlers[0] is RangeWeaponHandler)
        {
            RangeWeaponHandler rangeWeaponHandler = (RangeWeaponHandler)handlers[0];
            rangeWeaponHandler.BulletSize += value;
        }
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }


    private class SkillSet
    {
        public SkillType type;
        public int Level;
        public string name;
        public string description;
        public Sprite Image;
    
        public Action Action;

        public SkillSet(SkillType type, int Level, string name, string description, Sprite image, Action action = null)
        {
            this.type = type;
            this.Level = Level;
            this.name = name;
            this.description = description;
            Image = image;
           
            Action = action;
        }
    }



}



