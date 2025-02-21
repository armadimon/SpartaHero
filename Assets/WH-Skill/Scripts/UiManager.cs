using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject SkillSelect;
    private List<Button> Buttons = new List<Button>();
    
    void Start()
    {
      for (int i = 0;i < SkillSelect.transform.childCount;i++) 
        {
            Buttons.Add(SkillSelect.transform.GetChild(i).GetComponent<Button>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
