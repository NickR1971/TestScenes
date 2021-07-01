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
	[SerializeField] private GameObject localPrefab_en;
	[SerializeField] private GameObject localPrefab_ua;
	private UI_manager uiManager;
	private CMenu menu;
	private CSaveFile saveFile;


    private void Awake()
    {
		thisExemplar = this;

		if (CLocalisation.Init())
			CLocalisation.LoadLocalPrefab(localPrefab_en);
		menu = mainMenu.GetComponent<CMenu>();
		saveFile = new CSaveFile();
    }

    private void OnDestroy()
    {
		thisExemplar = null;
    }

    private void Start()
    {
		uiManager = UI_manager.GetLink();
		uiManager.SetActiveUI(menu);
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
		gameID = (uint)UnityEngine.Random.Range(100, 10000000);
		GoToMainScene();
    }

	public void MainMenuScene()
	{
		SceneManager.LoadScene("LogoScene");
	}

	public bool IsSavedGameExist() => saveFile.IsSavedFileExist();

	public void Save()
	{
		SaveData data = new SaveData();
		data.id = gameID;
		saveFile.Save(data);
	}

	public void Load()
	{
		if (IsSavedGameExist())
		{
			SaveData data = new SaveData();
			saveFile.Load(ref data);
			gameID = data.id;

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
		if (_language == UsedLocal.english) CLocalisation.LoadLocalPrefab(localPrefab_en);
		else if (_language == UsedLocal.ukrainian) CLocalisation.LoadLocalPrefab(localPrefab_ua);
		else Debug.Log("No realized localisation!");
		reloadText?.Invoke();
    }

	public void OpenSettings()
    {
		uiManager.SetActiveUI(settingsMenu.GetComponent<CUI>());
    }

	public void CloseSettings()
    {
		uiManager.CloseUI();
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
