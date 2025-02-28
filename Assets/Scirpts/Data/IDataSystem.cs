using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSystem
{
    int GetData(string data_key);
    void SetData(string data_key, int amount);
    Dictionary<string, int> GetAllDatas();
}