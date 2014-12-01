using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
	public GUITexture fade;

	public float fadingStart;

	public bool fading;
	public bool isFadeingToBlack;
	public float fadingSpeed;

	void Start()
	{
		StartCoroutine(fadeWithDeley(fadingStart));
	}

	void OnGUI()
	{
		if (fading)
		{
			if (isFadeingToBlack)
			{
				fade.color = new Color(0, 0, 0, fade.color.a + fadingSpeed * Time.deltaTime);
			}
			else
			{
				fade.color = new Color(0, 0, 0, fade.color.a - fadingSpeed * Time.deltaTime);
			}
		}

		if (fade.color.a <= 0)
		{
			fading = false;
			fade.color = new Color(0, 0, 0, 0);
		}
		else if (fade.color.a >= 0.5)
		{
			fading = false;
			fade.color = new Color(0, 0, 0, 0.5f);
		}
	}

	public void fadeToBlack(bool fadeToBlack)
	{
		fading = true;
		isFadeingToBlack = fadeToBlack;
	}

	public IEnumerator fadeWithDeley(float deley)
	{
		yield return new WaitForSeconds(deley);
		fading = true;
	}
}
