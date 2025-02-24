using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private int[] levelExp = {1,2,3,4,5,6,7,8,9,10};
    [SerializeField] private WeaponHandler[] weaponHandlers;



    private int level = 0;
    private int AllExp = 0;
    int i = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
    }

    private void LevelUp()
    {
       
        if(AllExp > levelExp[i])
        {
            level++;
            i++;
            Debug.Log($"현재 레벨{level} 현재 순번{i}");
            UiManager.Instance.setActve();
        }

       
    }


    public void Expup()
    {
        AllExp++;
    }
}
