using UnityEngine;
using System.Collections;

public class EleanorFirstMonolgue : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public AudioClip clip;
	public Animator playerAnim;
	public SaveDataReference saveDataRef;
	public EnableQuest enableQuest;

	void Start()
	{
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();

		if (!saveDataRef.saveData.data.escapeCampFirstMonoDone)
		{
			StartCoroutine(monolog());
		}
		else
		{
			letPlayerMove();
		}
	}

	IEnumerator monolog()
	{
		yield return new WaitForSeconds(2);

		playerAnim.SetBool("Dialog", true);
		player.GetComponent<AudioSource>().clip = clip;
		player.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(clip.length);

		playerAnim.SetBool("Dialog", false);
		saveDataRef.saveData.data.escapeCampFirstMonoDone = true;
		SaveLoad.Save(false);

		enableQuest.enabelQuest(1);
		letPlayerMove();
	}

	void letPlayerMove()
	{
		player.GetComponent<PlayerMovement>().enabled = true;
		mainCamera.GetComponent<MouseLook>().enabled = true;
	}
}
