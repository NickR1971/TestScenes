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
	[SerializeField] private Image mainMenu;
	[SerializeField] private Image settingsMenu;
	private CLocalisation local;
	private CMenu menu;
	private bool isActiveMenu;
	private string saveFileName;
	private CSaveFile saveFile;


    private void Awake()
    {
		thisExemplar = this;

		isActiveMenu = mainMenu.gameObject.activeSelf;
		local = GetComponent<CLocalisation>();
		if (local == null) Debug.LogError("No localisation component found!");
		menu = mainMenu.GetComponent<CMenu>();
		saveFileName = Application.persistentDataPath + "/TestSceneSaveData.dat";
		saveFile = new CSaveFile();
		saveFile.Init(saveFileName);
    }

	private void Start()
    {
    }

    private void OnDestroy()
    {
		thisExemplar = null;
    }

	public static ApplicationManager GetLink() 
	{
		if (thisExemplar == null) Debug.LogError("No created application manager!");

		return thisExemplar;
	}
	public int GetSceneID() => sceneID;

	public uint GetGameID() => gameID;

	public bool IsGameExist() => gameID > 0;

	public void HideMenu()
    {
		if(isActiveMenu)
        {
			isActiveMenu = false;
			mainMenu.gameObject.SetActive(isActiveMenu);
        }
    }

	public void ShowMenu()
    {
		if (!isActiveMenu)
        {
			isActiveMenu = true;
			mainMenu.gameObject.SetActive(isActiveMenu);
			menu = mainMenu.GetComponent<CMenu>();
			if (menu == null) Debug.Log("Menu access disabled");
        }
    }

	public void InvertMenu()
    {
		if (isActiveMenu) HideMenu();
		else ShowMenu();
    }

	public void GoToMainScene()
    {
		Debug.Log($"Game ID = {gameID}");
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

	public bool IsSavedGameExist()
    {
		return File.Exists(saveFileName);
    }

	public void Save()
	{
		SaveData data = new SaveData();
		data.id = gameID;
		saveFile.Save(data);
	}

	public void Load()
	{
		if (File.Exists(saveFileName))
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
		local.LoadLocal(_language);
		reloadText?.Invoke();
    }

	public void OpenSettings()
    {
		HideMenu();
		settingsMenu.gameObject.SetActive(true);
    }

	public void CloseSettings()
    {
		ShowMenu();
		settingsMenu.gameObject.SetActive(false);
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
