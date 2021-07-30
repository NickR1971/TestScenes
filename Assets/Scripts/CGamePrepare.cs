using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePrepare : MonoBehaviour, IGame
{
    void Start()
    {
        
    }

    public void Init(SaveData _data)
    {
        CGameManager.SetGameData(_data);
    }

    public SaveData GetData()
    {
        return CGameManager.GetData();
    }

    public void OnSave()
    {
        CGameManager.OnSave();
    }
}
