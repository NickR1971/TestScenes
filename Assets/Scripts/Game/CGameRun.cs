using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameRun : MonoBehaviour, IGame
{
    private CCubeRotation cube;

    void Start()
    {
        cube = FindObjectOfType<CCubeRotation>();
    }

    //----------------------------
    // IGame
    //----------------------------
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
    //----------------------------
}
