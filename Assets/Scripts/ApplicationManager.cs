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
	private CUI menu;
	private CSaveFile saveFile;


    private void Awake()
    {
		thisExemplar = this;
		
		if (CLocalisation.Init())
			CLocalisation.LoadLocalPrefab(localPrefab_en);

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
		// test goto
		if (_language == UsedLocal.english) goto l_english;
		if (_language == UsedLocal.ukrainian) goto l_ukrainian;
		goto l_noLocalisation;

		l_english: 
			CLocalisation.LoadLocalPrefab(localPrefab_en);
			goto l_end;
		l_ukrainian:
			CLocalisation.LoadLocalPrefab(localPrefab_ua);
			goto l_end;
		l_noLocalisation:
			Debug.Log("No realized localisation!");
		l_end:
		// goto is work! I'm happy!

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
