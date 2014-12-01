using UnityEngine;
using System.Collections;

public class PauseMusicController : MonoBehaviour
{
	public AudioSource audioSrc;

	void Start()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.ignoreListenerPause = true;
	}
}
