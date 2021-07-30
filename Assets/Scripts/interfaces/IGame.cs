using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame : IService
{
    void Init(SaveData _data);
    SaveData GetData();
    void OnSave();
}
