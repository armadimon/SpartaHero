using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Image[] images;

    private List<Button> Buttons = new List<Button>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Skip(Skill skill)
    {
        skill.gameObject.SetActive(false);
    }

    //




}
