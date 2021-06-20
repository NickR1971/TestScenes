using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
class SaveData
{
	public uint id;
}

public class ApplicationManager : MonoBehaviour
{
	private static uint gameID;

	[SerializeField] private int sceneID;
	[SerializeField] private Image imageMenu;
	private CMenu menu;
	private bool isActiveMenu;
	private string saveFileName;


    private void Awake()
    {
		isActiveMenu = imageMenu.gameObject.activeSelf;
		menu = imageMenu.GetComponent<CMenu>();
		saveFileName = Application.persistentDataPath + "/TestSceneSaveData.dat";
    }

	private void Start()
    {
    }

	public int GetSceneID() => sceneID;

	public uint GetGameID() => gameID;

	public bool IsGameExist() => gameID > 0;

	public void HideMenu()
    {
		if(isActiveMenu)
        {
			isActiveMenu = false;
			imageMenu.gameObject.SetActive(isActiveMenu);
        }
    }

	public void ShowMenu()
    {
		if (!isActiveMenu)
        {
			isActiveMenu = true;
			imageMenu.gameObject.SetActive(isActiveMenu);
			menu = imageMenu.GetComponent<CMenu>();
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
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(saveFileName);
		SaveData data = new SaveData();
	
		data.id = gameID;
		
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(saveFileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(saveFileName, FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
			
			gameID = data.id;

			Debug.Log("Game data loaded!");
			GoToMainScene();
		}
		else
			Debug.LogError("There is no save data!");
	}

	public void ResetData()
	{
		if (File.Exists(saveFileName))
		{
			File.Delete(saveFileName);
			Debug.Log("Data reset complete!");
		}
		else
			Debug.LogError("No save data to delete.");
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
