using UnityEngine;
using System.Collections;

public class InstantiateLoadingScreen : MonoBehaviour
{
	public Object loadingScreen;

	void Start()
	{
		if (GameObject.Find("LoadingScreen(Clone)") == null)
		{
			Instantiate(loadingScreen);
		}
	}
}
