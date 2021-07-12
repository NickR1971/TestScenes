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
	private string profileName;
	private ProfileData profileData;

	public CSaveFile()
    {
		SetProfile("Default");
		settingsFileName = Application.persistentDataPath + "/SettingsData.dat";
	}

	private string CreateProfileName(string _name) => Application.persistentDataPath + "/" + _name.Trim() + ".dat";

	private void SaveProfile()
    {
		string fileName = CreateProfileName(profileName);
		SaveFile<ProfileData>(profileData, fileName);
    }

	private bool LoadProfile(string _name)
    {
		if (_name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) return false;
		string fileName = CreateProfileName(_name);
		if (File.Exists(fileName))
        {
			LoadFile<ProfileData>(out profileData, fileName);
        }
		else
        {
			profileData = new ProfileData();
			SaveFile<ProfileData>(profileData, fileName);
        }
		return true;
    }

	public bool SetProfile(string _name)
    {
		if ( ! LoadProfile(_name)) return false;

		profileName = _name;
		saveFileName = Application.persistentDataPath + "/" + _name.Trim() + "_Data.dat";

		return true;
    }

	public string GetProfile() => profileName;

	public bool IsSavedFileExist() => profileData.savedList.Length > 1;

	private void SaveFile<T>(T _data, string _name)
    {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(_name);
		bf.Serialize(file, _data);
		file.Close();
    }

	private void Save(SaveData data)
	{
		SaveFile<SaveData>(data, saveFileName);
	}

	public void Save(string _name,SaveData _data)
    {
		profileData.RemoveSave(_name);
		profileData.AddSave(_name);
		SaveProfile();
		saveFileName = CreateSaveFileName(_name);
		Save(_data);
    }

	private string CreateSaveFileName(string _name) => Application.persistentDataPath + "/" + profileName.Trim() + "_" + _name.Trim() + "_save.dat";
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

	private void Load(out SaveData _data)
	{
		SaveData data;
		LoadFile<SaveData>(out data, saveFileName);
		_data = data;
		if (_data==null)
		{
			Debug.LogError("There is no save data!");
		}
	}

	public void Load(string _name,out SaveData _data)
    {
		SaveData data;
		saveFileName = CreateSaveFileName(_name);
		Load(out data);
		_data = data;
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
