using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class SaveData
{
	public int[] map = new int[81];
}

public class ApplicationManager : MonoBehaviour
{
	[SerializeField] private int sceneID;
	[SerializeField] private Image imageMenu;
	private CMenu menu;
	private bool isActiveMenu;


    private void Awake()
    {
		isActiveMenu = imageMenu.gameObject.activeSelf;
		menu = imageMenu.GetComponent<CMenu>();
    }

	private void Start()
    {
    }

	public int GetSceneID() { return sceneID; }

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

	public void NewGame()
    {
		SceneManager.LoadScene("MainScene");
    }

	public void MainMenuScene()
	{
		SceneManager.LoadScene("LogoScene");
	}

	/**********************
		public void Save()
		{
		 int i;

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath
						 + "/MySaveData.dat");
			SaveData data = new SaveData();
			for (i = 0; i < 81; i++)
			{
				data.map[i] = map[i];
			}
			bf.Serialize(file, data);
			file.Close();
			Debug.Log("Save to " + Application.persistentDataPath + "/MySaveData.dat");
		}

		public void Load()
		{
		 int i;

			if (File.Exists(Application.persistentDataPath
						   + "/MySaveData.dat"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file =
						   File.Open(Application.persistentDataPath
						   + "/MySaveData.dat", FileMode.Open);
				SaveData data = (SaveData)bf.Deserialize(file);
				for (i = 0; i < 81; i++)
				{
					if (cellList[i].SetValue(data.map[i]))
					{
						map[i] = data.map[i];
					}
				}
				file.Close();
				refresh?.Invoke();
				Debug.Log("Game data loaded!");
			}
			else
				Debug.LogError("There is no save data!");
		}

		public void ResetData()
		{
			if (File.Exists(Application.persistentDataPath
				  + "/MySaveData.dat"))
			{
				File.Delete(Application.persistentDataPath
								  + "/MySaveData.dat");
				Debug.Log("Data reset complete!");
			}
			else
				Debug.LogError("No save data to delete.");

		}
	**************************/

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
