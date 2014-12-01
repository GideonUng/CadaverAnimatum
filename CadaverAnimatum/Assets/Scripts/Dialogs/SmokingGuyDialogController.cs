using UnityEngine;
using System.Collections;

public class SmokingGuyDialogController : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public GameObject dialogCamera;

	public Animator anim;
	public Animator playerAnim;

	public PauseMenuGUI pauseMenuGUI;
	public HUD hud;

	public Transform playerStandingPos;
	public Transform NPCStandingPos;

	public AudioSource playerAudio;

	public AudioClip[] clips;
	public AudioClip iwillmeetafriend;
	public AudioClip iwantofindmyfather;
	public AudioClip icantletyoudothat;

	public AudioSource audiosrc;
	public DialogHUD dialogHUD;

	public bool skip;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			skip = true;
			StartCoroutine(endSkip());
		}
	}

	public void startDialog()
	{
		StartCoroutine(dialog());
	}

	IEnumerator dialog()
	{
		player.transform.position = playerStandingPos.position;
		player.transform.rotation = playerStandingPos.rotation;

		transform.position = NPCStandingPos.position;
		transform.rotation = NPCStandingPos.rotation;

		player.GetComponent<PlayerMovement>().enabled = false;
		playerAnim.SetFloat("Speed", 0);
		playerAnim.SetBool("Aiming", false);

		mainCamera.SetActive(false);
		mainCamera.GetComponent<MouseLook>().enabled = false;
		dialogCamera.SetActive(true);

		dialogCamera.transform.position = player.transform.position - player.transform.forward + Vector3.up + transform.right;
		dialogCamera.transform.LookAt(gameObject.transform);

		anim.SetBool("Dialog", true);
		audiosrc.clip = clips[0];
		audiosrc.Play();
		yield return new WaitForSeconds(clips[0].length);
		audiosrc.Stop();

		anim.SetBool("Dialog", false);

		string[] labels = { "I will meet a frend", "I want to find my father" };
		dialogHUD.displayDialog(labels, true);

		while (!dialogHUD.decided)
		{
			yield return new WaitForEndOfFrame();
		}

		switch (dialogHUD.decision)
		{
			case 0:
				StartCoroutine(silence());
				break;
			case 1:
				StartCoroutine(iWillMeetAFriend());
				break;
			case 2:
				StartCoroutine(iWantToFindMyFather());
				break;
			default:
				break;
		}
	}

	IEnumerator silence()
	{
		yield return new WaitForSeconds(0.1f);

		hud.enabled = true;

		mainCamera.SetActive(true);
		mainCamera.GetComponent<MouseLook>().enabled = true;
		dialogCamera.SetActive(false);

		player.GetComponent<PlayerMovement>().enabled = true;
	}

	IEnumerator iWillMeetAFriend()
	{
		yield return new WaitForSeconds(0.1f);

		playerAudio.clip = iwillmeetafriend;
		playerAudio.Play();

		playerAnim.SetBool("Dialog", true);

		yield return new WaitForSeconds(iwillmeetafriend.length);

		playerAnim.SetBool("Dialog", false);

		mainCamera.SetActive(true);
		mainCamera.GetComponent<MouseLook>().enabled = true;
		dialogCamera.SetActive(false);

		player.GetComponent<PlayerMovement>().enabled = true;
	}

	IEnumerator iWantToFindMyFather()
	{
		yield return new WaitForSeconds(0.1f);

		playerAudio.clip = iwantofindmyfather;
		playerAudio.Play();

		playerAnim.SetBool("Dialog", true);

		yield return new WaitForSeconds(iwantofindmyfather.length);


		playerAnim.SetBool("Dialog", false);

		yield return new WaitForSeconds(1);

		audiosrc.clip = icantletyoudothat;
		audiosrc.Play();

		anim.SetBool("Dialog", true);

		yield return new WaitForSeconds(icantletyoudothat.length);


		anim.SetBool("Dialog", false);

		SaveLoad.Load();
		Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator endSkip()
	{
		yield return new WaitForEndOfFrame();
		skip = false;
	}
}