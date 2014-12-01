using UnityEngine;
using System.Collections;

using System.IO;


public class PauseMenuGUI : MonoBehaviour
{
	public bool paused = false;

	public bool settingsActive = false;
	public bool saveActive = false;
	public bool helpActive = false;
	public bool settingsGameplayActive;
	public bool settingsAudioActive;
	public bool settingsGraphicsActive;
	public bool settingsControllsActive;

	public float marginLeft;
	public float marginTop;

	public float mainSectionWith;

	public Rect mainSection;
	public Rect secondarySection;

	public string[] toolbarArray;

	public bool mouseLookIsEnabled;
	public bool hudIsEnabled;
	public bool cursorIsEnabled;

	private GameObject player;
	private AudioSource pauseMusic;

	private SaveData saveData;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		pauseMusic = GameObject.Find("PauseMusic").GetComponent<AudioSource>();

		saveData = GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData;

		Screen.showCursor = false;
		Screen.lockCursor = true;
		mainSection = new Rect(marginLeft, marginTop, mainSectionWith, Screen.height - 2 * marginTop);
		secondarySection = new Rect(marginLeft * 2 + mainSectionWith, marginTop, Screen.width - 3 * marginLeft - mainSectionWith, Screen.height - 2 * marginTop);
	}

	void Update()
	{
		if (Input.GetButtonUp("Pause"))
		{
			paused = !paused;

			if (paused)
			{
				pause();
			}
			else
			{
				continuePause();
			}
		}
	}

	void OnGUI()
	{
		if (paused)
		{
			if (!pauseMusic.isPlaying)
			{
			}

			GUI.Box(mainSection, "");
			GUI.Box(secondarySection, "");

			GUILayout.BeginArea(mainSection);

			if (!settingsActive && !saveActive && !helpActive)
			{
				if (GUILayout.Button("Continue"))
				{
					continuePause();
					paused = false;
				}

				if (GameObject.FindGameObjectWithTag("Quest") != null)
				{
					if (GUILayout.Button("Help"))
					{
						helpActive = true;
					}
				}

				if (GUILayout.Button("Settings"))
				{
					settingsActive = !settingsActive;
					settingsGameplayActive = true;
				}

				if (GUILayout.Button("Save"))
				{
					saveActive = true;
				}

				if (GUILayout.Button("Main Menu"))
				{
					Time.timeScale = 1f;
					AudioListener.pause = false;
					Application.LoadLevel(0);
				}

				if (GUILayout.Button("Quit"))
				{
					Application.Quit();
				}
			}
			else
			{
				if (GUILayout.Button("Back"))
				{
					back();
				}
			}

			GUILayout.EndArea();

			GUILayout.BeginArea(secondarySection);

			if (settingsActive)
			{
				switch (GUILayout.Toolbar(toolbarArray.Length, toolbarArray))
				{
					case 0:
						settingsGameplayActive = true;
						settingsAudioActive = false;
						settingsGraphicsActive = false;
						settingsControllsActive = false;
						break;
					case 1:
						settingsAudioActive = true;
						settingsGameplayActive = false;
						settingsGraphicsActive = false;
						settingsControllsActive = false;
						break;
					case 2:
						settingsGraphicsActive = true;
						settingsAudioActive = false;
						settingsGameplayActive = false;
						settingsControllsActive = false;
						break;
					case 3:
						settingsControllsActive = true;
						settingsAudioActive = false;
						settingsGraphicsActive = false;
						settingsGameplayActive = false;
						break;
				}

				if (settingsControllsActive)
				{
					bool acceleration = GUILayout.Toggle(GetComponent<MouseLook>().mouseAcceleration, "Mouse Acceleration");
					GetComponent<MouseLook>().mouseAcceleration = acceleration;

					if (GUILayout.Button("Controlls"))
					{
					}
				}

				if (settingsAudioActive)
				{
					AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
				}

				if (settingsGraphicsActive)
				{
					string[] names = QualitySettings.names;

					int quality = 0;
					foreach (string name in names)
					{
						if (GUILayout.Button(name))
						{
							QualitySettings.SetQualityLevel(quality);
						}
						quality++;
					}
				}

				if (settingsGameplayActive)
				{
					if (GUILayout.Button("Gameplay"))
					{
					}
				}
			}

			if (saveActive)
			{
				if (GUILayout.Button("override"))
				{
					SaveLoad.Save(false);
					saveActive = false;
				}

				if (GUILayout.Button("new Savepoint"))
				{
					SaveLoad.Save(true);
					saveActive = false;
				}

				string[] saveFiles = Directory.GetFiles(Application.dataPath);
				string[] saveFileNames = new string[10];

				int i = 0;
				foreach (string saveFile in saveFiles)
				{
					if (saveFile.Contains("SaveData") && !saveFile.Contains("meta"))
					{
						saveFileNames[i] = saveFile;
						i++;
					}
				}

				i = 0;
				foreach (string saveFileName in saveFileNames)
				{
					if (saveFileName != null)
					{
						if (GUILayout.Button("Load: " + Directory.GetLastWriteTime(saveFileName)))
						{
							SaveLoad.Load(saveFileName);
							continuePause();
							Application.LoadLevel(saveData.data.currentLevel);
							saveActive = false;
						}

						if (GUILayout.Button("X"))
						{
							File.Delete(saveFileName);
						}
					}

					i++;
				}
			}

			if (helpActive)
			{
				if (GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest>() != null)
				{
					Quest quest = GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest>();

					GUILayout.Label(quest.titel);
					GUILayout.Label(quest.description);
				}
			}

			GUILayout.EndArea();
		}
	}

	void pause()
	{
			Time.timeScale = 0f;
			AudioListener.pause = true;

			if (gameObject.name == "Camera")
			{
				mouseLookIsEnabled = GetComponent<MouseLook>().enabled;
				GetComponent<MouseLook>().enabled = false;

				hudIsEnabled = GetComponent<HUD>().enabled;
				GetComponent<HUD>().enabled = false;
			}

			cursorIsEnabled = Screen.showCursor;
			Screen.showCursor = true;
			Screen.lockCursor = false;

			pauseMusic.Play();
	}

	void continuePause()
	{
		Time.timeScale = 1f;
		AudioListener.pause = false;

		if (gameObject.name == "Camera")
		{
			GetComponent<MouseLook>().enabled = mouseLookIsEnabled;

			GetComponent<HUD>().enabled = hudIsEnabled;
		}
		Screen.showCursor = cursorIsEnabled;
		Screen.lockCursor = !cursorIsEnabled;

		settingsActive = false;
		helpActive = false;
		settingsGameplayActive = false;
		settingsAudioActive = false;
		settingsGraphicsActive = false;
		settingsControllsActive = false;
		saveActive = false;

		pauseMusic.Stop();
	}

	void back()
	{
		settingsActive = false;
		helpActive = false;
		settingsGameplayActive = false;
		settingsAudioActive = false;
		settingsGraphicsActive = false;
		settingsControllsActive = false;
		saveActive = false;
	}
}