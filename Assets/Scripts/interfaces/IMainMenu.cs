﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainMenu
{
	bool IsGameExist();
	void SetLanguage(UsedLocal _language);
	void SaveSettings();
	void NewGame();
	void GoToMainScene();
	void MainMenuScene();
	void Save();
	void Load();
	void OpenSettings();
	void Quit();
}
