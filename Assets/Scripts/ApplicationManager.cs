using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ApplicationManager : MonoBehaviour, IMainMenu, ISaveLoad
{
	private static uint gameID;

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
		
		AllServices.Container.Register<IDialog>(dialog);
		AllServices.Container.Register<IMainMenu>(this);
		AllServices.Container.Register<ISaveLoad>(this);

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
		AllServices.Container.Register<IUI>(uiManager);
		uiManager.Init();
		startUI = startUIobject.GetComponent<CUI>();
		startUI.InitUI();
		settingsMenu.GetComponent<CUI>().InitUI();
		dialog.InitUI();
		saveLoadWindow.InittInterface();

		GameObject vGameConsole = Instantiate(prefabGameConsole, uiCanvas.transform);
		gameConsole = vGameConsole.GetComponent<CGameConsole>().GetIGameConsole();
		AllServices.Container.Register<IGameConsole>(gameConsole);
	}

	private void OnDestroy()
    {
		uiManager.CloseUI();
    }

    private void Start()
    {
		uiManager.OpenUI(startUI);
    }

	//-----------------------------------------------------
	// ISaveLoad
	//-----------------------------------------------------
	public string GetProfile() => saveFile.GetProfile();

	public bool SetProfile(string _name)
    {
		return saveFile.SetProfile(_name);
    }

	public bool IsSavedGameExist() => saveFile.IsSavedFileExist();
	public bool IsSavedGameExist(string _name) => saveFile.IsSavedFileExist(_name);

	public void Save(string _name)
	{
		CGameManager.OnSave();
		saveFile.Save(_name, CGameManager.GetData());
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

	public void RemoveSave(string _name)
	{
		saveFile.ResetData(_name);
	}

	public string[] GetSavedList() => saveFile.GetSavedList();

	//--------------------------------------------------------------
	// IMainMenu interface
	//--------------------------------------------------------------
	public bool IsGameExist() => gameID > 0;

	public void SetLanguage(UsedLocal _language)
    {
		usedLanguage = _language;
		CLocalisation.LoadLocalPrefab(localData[(int)_language]);
    }

	public void SaveSettings()
    {
		SettingsData data = new SettingsData();
		data.profileName = GetProfile();
		data.selected = usedLanguage;
		saveFile.SaveSettings(data);
		uiManager.CloseUI();
    }

	public void NewGame()
	{
		SaveData data = CGameManager.GetData();
		gameID = (uint)UnityEngine.Random.Range(100, 10000000);
		if (data == null)
		{
			data = new SaveData();
			CGameManager.Init(data);
		}

		data.id = gameID;
		data.SetColor(Color.gray);
		GoToMainScene();
	}

	public void GoToMainScene()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void MainMenuScene()
	{
		CGameManager.OnSave();
		SceneManager.LoadScene("LogoScene");
	}

	public void Save()
	{
		saveLoadWindow.OpenSaveWindow();
	}

	public void Load()
	{
		saveLoadWindow.OpenLoadWindow();
	}

	public void OpenSettings()
    {
		uiManager.OpenUI(settingsMenu.GetComponent<CUI>());
    }

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
