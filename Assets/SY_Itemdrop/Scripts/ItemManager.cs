using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //[SerializeField]
    //public List<GameObject> PosionPrefabs; // ������ ������ ������ ����Ʈ

    //[SerializeField]
    //public List<ItemController> activePosion = new List<ItemController>(); // ���� Ȱ��ȭ�� �����۵�

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
    //    //���� �� ��ġ Ȯ��
    //    Vector3 enemyDeathPosition = enemy.transform.position;



    //    //bool Droprate = Random.Range(0, 2) == 1? false: true;
    //    //if (Droprate)

    //    //������ ����   
    //    GameObject spawnedPosion = Instantiate(randomPrefab, enemyDeathPosition, Quaternion.identity);
    //    ItemController itemController = spawnedPosion.GetComponent<ItemController>();
    //    itemController.Init(this);
    //    activePosion.Add(itemController);
    //}



}
