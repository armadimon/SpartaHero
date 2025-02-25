using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //[SerializeField]
    //public List<GameObject> PosionPrefabs; // 생성할 아이템 프리팹 리스트

    //[SerializeField]
    //public List<ItemController> activePosion = new List<ItemController>(); // 현재 활성화된 아이템들

    //public EnemyManager enemyManager;
    //public EnemyController enemyController;
    //public ResourceController resourceController;

    //GameObject spawnedPosion;

    //public void Start()
    //{
    //    enemyManager = GetComponent<EnemyManager>();
    //    enemyController = GetComponent<EnemyController>();
    //    resourceController = GetComponent<ResourceController>();
    //}
    //public void Spawnitem(EnemyController enemy)
    //{
    //    GameObject randomPrefab = PosionPrefabs[Random.Range(0, PosionPrefabs.Count)];
    //    //죽은 적 위치 확인
    //    Vector3 enemyDeathPosition = enemy.transform.position;



    //    //bool Droprate = Random.Range(0, 2) == 1? false: true;
    //    //if (Droprate)

    //    //아이템 생성   
    //    GameObject spawnedPosion = Instantiate(randomPrefab, enemyDeathPosition, Quaternion.identity);
    //    ItemController itemController = spawnedPosion.GetComponent<ItemController>();
    //    itemController.Init(this);
    //    activePosion.Add(itemController);
    //}



}
