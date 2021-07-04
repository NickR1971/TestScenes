using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
	public event Action reloadText;

	private static uint gameID;

	private static ApplicationManager thisExemplar;

	[SerializeField] private int sceneID;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject settingsMenu;
	[SerializeField] private GameObject[] localData=new GameObject[2];
	private CUI menu;
	private CSaveFile saveFile;


    private void Awake()
    {
		SaveData data = CGameManager.GetData();
		
		thisExemplar = this;
		
		if (data == null)
		{
			data = new SaveData();
			CGameManager.Init(data);
		}

		if (CLocalisation.Init())
			CLocalisation.LoadLocalPrefab(localData[0]);

		UI_manager.Init();
		menu = mainMenu.GetComponent<CUI>();
		saveFile = new CSaveFile();
    }

    private void OnDestroy()
    {
		UI_manager.CloseUI();
		thisExemplar = null;
    }

    private void Start()
    {
		UI_manager.OpenUI(menu);
    }

    public static ApplicationManager GetLink()
	{
		if (thisExemplar == null) Debug.LogError("No created application manager!");

		return thisExemplar;
	}

	public int GetSceneID() => sceneID;

	public uint GetGameID() => gameID;

	public bool IsGameExist() => gameID > 0;

	public void GoToMainScene()
    {
		SceneManager.LoadScene("MainScene");
	}

	public void NewGame()
    {
		SaveData data = CGameManager.GetData();
		gameID = (uint)UnityEngine.Random.Range(100, 10000000);
		if(data==null)
        {
			data = new SaveData();
			CGameManager.Init(data);
        }

		data.id = gameID;
        data.SetColor(Color.gray);
		GoToMainScene();
    }

	public void MainMenuScene()
	{
		CGameManager.OnSave();
		SceneManager.LoadScene("LogoScene");
	}

	public bool IsSavedGameExist() => saveFile.IsSavedFileExist();

	public void Save()
	{
		CGameManager.OnSave();
		saveFile.Save(CGameManager.GetData());
	}

	public void Load()
	{
		if (IsSavedGameExist())
		{
			SaveData data = CGameManager.GetData();
			saveFile.Load(out data);
			gameID = data.id;
			CGameManager.Init(data);

			GoToMainScene();
		}
		else
			Debug.LogError("There is no save data!");
	}

	public void ResetData()
	{
		saveFile.ResetData();
	}

	public void SetLanguage(UsedLocal _language)
    {
		CLocalisation.LoadLocalPrefab(localData[(int)_language]);
		reloadText?.Invoke();
    }

	public void OpenSettings()
    {
		UI_manager.OpenUI(settingsMenu.GetComponent<CUI>());
    }

	public void CloseSettings()
    {
		UI_manager.CloseUI();
    }

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
