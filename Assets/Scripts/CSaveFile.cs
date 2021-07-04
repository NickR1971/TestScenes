using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSaveFile
{
	private string saveFileName;

	public CSaveFile()
    {
		saveFileName = Application.persistentDataPath + "/TestSceneSaveData.dat";
	}

	public bool IsSavedFileExist() => File.Exists(saveFileName);

	private void SaveFile<T>(T _data, string _name)
    {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(_name);
		bf.Serialize(file, _data);
		file.Close();
    }

	public void Save(SaveData data)
	{
		SaveFile<SaveData>(data, saveFileName);
	}

	private void LoadFile<T>(out T _data, string _name)
    {
		if (File.Exists(_name))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(saveFileName, FileMode.Open);
			_data = (T)bf.Deserialize(file);
			file.Close();
		}
		else _data = default(T);
    }

	public void Load(out SaveData _data)
	{
		SaveData data = new SaveData();
		LoadFile<SaveData>(out data, saveFileName);
		_data = data;
		if (_data==null)
		{
			Debug.LogError("There is no save data!");
		}
	}

	public void ResetData()
	{
		if (File.Exists(saveFileName))
		{
			File.Delete(saveFileName);
		}
		else
			Debug.LogError("No save data to delete.");
	}

}
