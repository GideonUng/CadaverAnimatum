using UnityEngine;
using System.Collections;

public class WalkingStepsAudio : MonoBehaviour 
{
	public AudioSource feet;
	public AudioClip[] stepSound;

	public float maxPitch;
	public float minPitch;
	public float maxVolume;
	public float minVolume;

	void Walk_Sound()
	{
		feet.clip = stepSound[Random.Range(0, stepSound.Length - 1)];
		feet.pitch = Random.Range(minPitch, maxPitch);
		feet.volume = Random.Range(minVolume, maxVolume);
		feet.Play();
	}
}