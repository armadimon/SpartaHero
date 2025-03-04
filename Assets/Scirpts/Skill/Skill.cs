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

        player = FindObjectOfType<PlayerController>().transform;

        WeaponPivot = player.GetChild(1);
               
        for (int i = 0;i < WeaponPivot.childCount;i++)
        {
            handlers.Add(WeaponPivot.GetChild(i).GetComponent<WeaponHandler>());
        }
        SkillCreate();
        //player = FindAnyObjectByType<Player>();

        SkipBut.onClick.AddListener(Skip);
        //�����Ǿ�� ��
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
        Parent.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SkillSelcet()
    {
        List<SkillSet> skills = new List<SkillSet>();
        System.Random random = new System.Random();

        List<int> list = new List<int>();
        List<int> lists = new List<int>();


        //������ �ʿ���
        bool IsRange = false;
        bool IsMelee = false;
        if (WeaponPivot != null && WeaponPivot.childCount > 0)
        {
            Transform firstChild = WeaponPivot.GetChild(0);
            if (firstChild != null)
            {
                IsRange = firstChild.GetComponent<RangeWeaponHandler>() != null;
                IsMelee = firstChild.GetComponent<MeleeWeaponHandler>() != null;

                // IsRange 및 IsMelee 사용
            }
            else
            {
                Debug.LogError("WeaponPivot의 첫 번째 자식이 null입니다.");
            }
        }
        else
        {
            Debug.LogError("WeaponPivot이 null이거나 자식이 없습니다.");
        }
        foreach (SkillSet skill in Skills)
        {
            //���ο��� �ľ�
            if (skill.type == SkillType.Melee && !IsMelee)
                continue;
            if (skill.type == SkillType.Range && !IsRange)
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

        
        Skills.Add(new SkillSet(SkillType.Default, 0, "healthup", "HP 50 UP", images[0], () => HealthUp(50)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackUp", "Attack Power \n3 UP", images[1], () => AttackUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "SpeedUp", "Speed \n3 UP", images[2], () => SpeedUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackSpeedUp", "AttackSpeed \n3 UP", images[3], () => AttackSpeedUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackRangeUp", "AttackRange \n3 UP", images[4], () => AttackRangeUp(3)));
        Skills.Add(new SkillSet(SkillType.Default, 0, "AttackDelayDown", "AttackDelay  \n0.2 Down", images[5], () => AttackDelayDown(0.2f)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "AttackCountUp", "AttackCount \n1 UP", images[6], () => AttackCountUp(1)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "BulletSizeup", "BulletSize \n1 UP", images[7], () => BulletSizeup(1)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "Boundup", "Bound Count \n1 UP", images[8], () => Boundup(1)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "Penetration", "Penetration \n1 UP", images[9], () => Penetration(1)));
        Skills.Add(new SkillSet(SkillType.Range, 0, "Slow", "Shoot a \nslowing arrow", images[10], Slow));
        Skills.Add(new SkillSet(SkillType.Melee, 0, "Parrying", "Parrying", images[11], () => Parrying()));


    }

    private void Parrying()
    {
        MeleeWeaponHandler pWeaponHandler = player.GetComponentInChildren<MeleeWeaponHandler>();
        pWeaponHandler.ActiveSkills.Add(ActiveSkill.Parrying, true);
        pWeaponHandler.StartParryCoroutine();
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
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
        player.GetComponent<ResourceController>().CurrentHealth += value;
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
    private void Boundup(int value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        if (handlers[0] is RangeWeaponHandler)
        {
            RangeWeaponHandler rangeWeaponHandler = (RangeWeaponHandler)handlers[0];
            rangeWeaponHandler.BoundCountt += value;
        }
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }

    private void Penetration(int value)
    {
        //handlers.Add(FindAnyObjectByType<WeaponHandler>());
        if (handlers[0] is RangeWeaponHandler)
        {
            RangeWeaponHandler rangeWeaponHandler = (RangeWeaponHandler)handlers[0];
            rangeWeaponHandler.Penetration += value;
        }
        Time.timeScale = 1f;
        Parent.gameObject.SetActive(false);
    }
    private void Slow()
    {
        if (handlers[0] is RangeWeaponHandler)
        {
            RangeWeaponHandler rangeWeaponHandler = (RangeWeaponHandler)handlers[0];
            rangeWeaponHandler.Debuff[0] = true;
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




