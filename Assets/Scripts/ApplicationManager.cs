using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IUI
{
	void OpenUI(CUI _ui);
	void CloseUI();
}

public class ApplicationManager : MonoBehaviour
{
	public event Action reloadText;

	private static uint gameID;

	private static ApplicationManager thisExemplar;

	[SerializeField] private int sceneID;
	private UsedLocal usedLanguage=UsedLocal.english;
	[SerializeField] Canvas uiCanvas;
	[SerializeField] private GameObject startUIobject;
	[SerializeField] private GameObject settingsMenu;
	[SerializeField] private CDialog dialog;
    [SerializeField] private CSaveLoad saveLoadWindow;
	[SerializeField] private GameObject prefabGameConsole;
	[SerializeField] private GameObject[] localData=new GameObject[2];
	private CUI startUI;
	private CSaveFile saveFile;
	private UImanager uiManager;
	private IGameConsole gameConsole;


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
		startUI = startUIobject.GetComponent<CUI>();
		startUI.InitUI(this);
		settingsMenu.GetComponent<CUI>().InitUI(this);
		dialog.InitUI(this);
		saveLoadWindow.InitUI(this);

		GameObject vGameConsole = Instantiate(prefabGameConsole, uiCanvas.transform);
		gameConsole = vGameConsole.GetComponent<CGameConsole>().GetIGameConsole();
    }

    private void OnDestroy()
    {
		uiManager.CloseUI();
		thisExemplar = null;
    }

    private void Start()
    {
		uiManager.OpenUI(startUI);
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

	public IDialog GetDialogManager()
    {
		return dialog;
    }

	public IGameConsole GetGameConsole()
    {
		return gameConsole;
    }

	public int GetSceneID() => sceneID;

	public uint GetGameID() => gameID;

	public string GetProfile() => saveFile.GetProfile();

	public bool SetProfile(string _name)
    {
		return saveFile.SetProfile(_name);
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

	public void Save(string _name)
	{
		CGameManager.OnSave();
		saveFile.Save(_name, CGameManager.GetData());
	}

	public void Save()
    {
		saveLoadWindow.OpenSaveWindow();
    }

	public void Load(string _name)
	{
		if (IsSavedGameExist())
		{
			SaveData data = CGameManager.GetData();
			saveFile.Load(_name, out data);
			gameID = data.id;
			CGameManager.Init(data);

			GoToMainScene();
		}
		else
			Debug.LogError("There is no save data!");
	}

	public void Load()
    {
		saveLoadWindow.OpenLoadWindow();
    }

	public void RemoveSave(string _name)
	{
		saveFile.ResetData(_name);
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
		data.profileName = GetProfile();
		data.selected = usedLanguage;
		saveFile.SaveSettings(data);
		CloseSettings();
    }

	public string[] GetSavedList() => saveFile.GetSavedList();

	public void Quit()
    {
		dialog.OpenDialog(EDialog.Question, CLocalisation.GetString(EnumStringID.msg_sure), OnQuit);
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
