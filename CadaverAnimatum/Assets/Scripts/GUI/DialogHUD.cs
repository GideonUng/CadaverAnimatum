using UnityEngine;
using System.Collections;

public class DialogHUD : MonoBehaviour
{
	public HUD hud;

	public bool show;
	public int decision;
	public bool decided;
	public string[] buttonLabels;

	public float timer;
	public float time;
	public bool showTimer;

	public int buttonCount;

	public Rect[] buttonPositions;
	public Rect timerPosition;
	
	public float buttonHeight;

	public float marginLeft;
	public float marginTop;

	void Start()
	{
		buttonPositions[0] = new Rect(marginLeft, Screen.height - marginTop - buttonHeight, Screen.width / 2 - marginLeft * 3, buttonHeight);
		buttonPositions[1] = new Rect(Screen.width / 2 + 2 * marginLeft, Screen.height - marginTop - buttonHeight, Screen.width / 2 - marginLeft * 3, buttonHeight);

	}

	public void displayDialog(string[] labels, bool hasTimer)
	{
		decision = 0;
		decided = false;
		time = timer;
		showTimer = hasTimer;
		buttonCount = labels.Length;
		buttonLabels = labels;
		show = true;
		Screen.showCursor = true;
	}

	void Update()
	{
		if (show)
		{
			time -= Time.deltaTime;
		}
	}

	void OnGUI()
	{
		if (show)
		{
			for (int i = 0; i <= buttonCount - 1; i++)
			{
				if (GUI.Button(buttonPositions[i], buttonLabels[i]))
				{
					decision = i + 1;
					decided = true;
					show = false;
					Screen.showCursor = false;
				}
			}
			/*
			for (int i = buttonCount; i <= 5; i++)
			{
				if (GUI.Button(buttonPositions[i], "X"))
				{
				}
			}
*/
			if (showTimer)
			{
				GUI.HorizontalSlider(timerPosition, time, 0, timer);

				if (time <= 0)
				{
					decided = true;
					show = false;
					Screen.showCursor = false;
				}
			}
		}
	}
}

