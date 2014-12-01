using UnityEngine;
using System.Collections;

public class RemoveShotSound : MonoBehaviour
{
	void Update()
	{
		if (!GetComponent<AudioSource>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
