using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager
{
	public event Action onSave;
    private SaveData gameData = null;

    public CGameManager()
    {
        gameData = new SaveData();
    }

	public void SetGameData(SaveData _data)
    {
        gameData = _data;
    }

    public SaveData GetData() => gameData;

    public void OnSave()
    {
        onSave?.Invoke();
    }

}
