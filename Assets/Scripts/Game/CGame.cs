using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGame : MonoBehaviour, IGame
{
    protected CGameManager manager;
    //----------------------------
    // IGame
    //----------------------------
    public void Init(CGameManager _manager)
    {
        manager = _manager;
    }

    public SaveData GetData()
    {
        return manager.GetData();
    }

    public void OnSave()
    {
        manager.OnSave();
    }
    public void AddOnSaveAction(Action _a)
    {
        manager.onSave += _a;
    }
    public void RemoveOnSaveAction(Action _a)
    {
        manager.onSave -= _a;
    }
    //----------------------------
}
