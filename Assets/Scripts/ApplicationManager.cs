using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IUI
{
	void OpenUI(CUI _ui);
	void CloseUI();
}

public class ApplicationManager : MonoBehaviour //, IUI
{
	public event Action reloadText;

	private static uint gameID;

	private static ApplicationManager thisExemplar;

	[SerializeField] private int sceneID;
	private UsedLocal usedLanguage=UsedLocal.english;
	private string nameProfile;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject settingsMenu;
	[SerializeField] private CDialog dialog;
	[SerializeField] private GameObject[] localData=new GameObject[2];
	private CUI menu;
	private CSaveFile saveFile;
	private UImanager uiManager;


    private void Awake()
    {
		SettingsData settingsData;
		SaveData data = CGameManager.GetData();
		
		thisExemplar = this;
		
		if (data == null)
		{
			data = new SaveData();
			CGameManager.Init(data);
		}
		saveFile = new CSaveFile();
		saveFile.LoadSettings(out settingsData);
		if(settingsData==null)
        {
			settingsData = new SettingsData();
        }
		usedLanguage = settingsData.selected;
		SetProfile(settingsData.profileName);

		if (CLocalisation.Init())
			CLocalisation.LoadLocalPrefab(localData[(int)usedLanguage]);

		uiManager = new UImanager();
		uiManager.Init();
		menu = mainMenu.GetComponent<CUI>();
    }

    private void OnDestroy()
    {
		uiManager.CloseUI();
		thisExemplar = null;
    }

    private void Start()
    {
		uiManager.OpenUI(menu);
    }

    public static ApplicationManager GetLink()
	{
		if (thisExemplar == null) Debug.LogError("No created application manager!");

		return thisExemplar;
	}

	public IUI GetUImanager()
    {
		return uiManager;
    }

	public int GetSceneID() => sceneID;

	public uint GetGameID() => gameID;

	public string GetProfile() => nameProfile;

	public void SetProfile(string _name)
    {
		nameProfile = _name;
		saveFile.SetProfile(nameProfile);
    }

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
		usedLanguage = _language;
		CLocalisation.LoadLocalPrefab(localData[(int)_language]);
		reloadText?.Invoke();
    }

	public void OpenSettings()
    {
		uiManager.OpenUI(settingsMenu.GetComponent<CUI>());
    }

	public void CloseSettings()
    {
		uiManager.CloseUI();
    }

	public void SaveSettings()
    {
		SettingsData data = new SettingsData();
		data.profileName = nameProfile;
		data.selected = usedLanguage;
		saveFile.SaveSettings(data);
		CloseSettings();
    }

	public void Message(EnumStringID _strID, Action _onDialogYes=null)
    {
		dialog.OpenDialog(EDialog.Warning, CLocalisation.GetString(_strID), _onDialogYes);
    }

	public void ErrorMessage(EnumStringID _strID, Action _onDialogYes = null)
    {
		dialog.OpenDialog(EDialog.Error, CLocalisation.GetString(_strID), _onDialogYes);
    }

	public void Question(EnumStringID _strID, Action _onDialogYes = null)
    {
		dialog.OpenDialog(EDialog.Question, CLocalisation.GetString(_strID), _onDialogYes);
    }

	public void Quit()
    {
		Question(EnumStringID.msg_sure, OnQuit);
	}

	private void OnQuit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
