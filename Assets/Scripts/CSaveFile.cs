using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSaveFile
{
	private string saveFileName;
	private string settingsFileName;

	public CSaveFile()
    {
		SetProfile("Default");
		settingsFileName = Application.persistentDataPath + "/SettingsData.dat";
	}

	public void SetProfile(string _name)
    {
		saveFileName = Application.persistentDataPath + "/"+_name.Trim()+"_Data.dat";
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
			FileStream file = File.Open(_name, FileMode.Open);
			_data = (T)bf.Deserialize(file);
			file.Close();
		}
		else _data = default;// (T);
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

	public void LoadSettings(out SettingsData _data)
    {
		SettingsData data;
		LoadFile<SettingsData>(out data, settingsFileName);
		_data = data;
    }

	public void SaveSettings(SettingsData _data)
    {
		SaveFile<SettingsData>(_data, settingsFileName);
    }

	private void RemoveFile(string _name)
    {
		if (File.Exists(_name))
		{
			File.Delete(_name);
		}
		else
			Debug.LogError("No save data to delete.");
    }

	public void ResetData()
	{
		RemoveFile(saveFileName);
	}

}
