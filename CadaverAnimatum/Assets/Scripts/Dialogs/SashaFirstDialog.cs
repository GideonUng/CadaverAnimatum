using UnityEngine;
using System.Collections;

public class SashaFirstDialog : MonoBehaviour
{
	public GameObject el;
	public GameObject sa;

	public Animator elAnim;
	public Animator saAnim;

	public AudioSource elAudio;
	public AudioSource saAudio;

	public GameObject dialogCamera;

	public DialogHUD dialogHUD;

	public FadeIn fader;

	public Transform[] cameraPositions;
	public Transform[] standingPositions;

	public GameObject door;

	public AnimationClip doorOpen;
	public AnimationClip doorClose;

	public AudioSource doorAudio;
	public AudioClip knockKnock;

	public AudioClip[] clips;
	public int clip;

	public AudioClip[] okLetsGoClips;
	public int okLetsGoClip;

	public AudioClip[] ihaveToTalktToThemFirstClips;
	public int ihaveToTalktToThemFirstClip;

	void Start()
	{
		StartCoroutine(startDialog());
	}

	IEnumerator startDialog()
	{
		setCameraPos(0);

		yield return new WaitForSeconds(1);

		doorAudio.clip = knockKnock;
		doorAudio.Play();

		yield return new WaitForSeconds(1.5f);

		door.animation.Play(doorOpen.name);

		yield return new WaitForSeconds(0.5f);

		// Hey Sasha
		elStartTalking();
		elAnim.SetInteger("Custom_Animation", clip);
		yield return new WaitForEndOfFrame();
		elAnim.SetInteger("Custom_Animation", -1);
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		yield return new WaitForSeconds(0.2f);

		//Hey El I really need to talk to you! It’s super important!
		setCameraPos(1);
		saStartTalking();
		saAnim.SetInteger("Custom_Animation", clip);
		yield return new WaitForEndOfFrame();
		saAnim.SetInteger("Custom_Animation", -1);
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Can it wait till morn-...
		setCameraPos(0);
		elStartTalking();
		elAnim.SetInteger("Custom_Animation", clip);
		yield return new WaitForEndOfFrame();
		elAnim.SetInteger("Custom_Animation", -1);
		yield return new WaitForSeconds(clips[clip].length - 0.1f);
		elStopTalking();

		//No it can’t.
		setCameraPos(1);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Well come in then.
		setCameraPos(0);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		saAnim.SetFloat("Speed", 1);

		yield return new WaitForSeconds(0.3f);

		saAnim.SetFloat("Speed", 0);

		fader.fadeToBlack(true);

		yield return new WaitForSeconds(2);

		el.transform.position = standingPositions[1].position;
		el.transform.rotation = standingPositions[1].rotation;

		sa.transform.position = standingPositions[0].position;
		sa.transform.rotation = standingPositions[0].rotation;

		door.animation.Play(doorClose.name);

		fader.fadeToBlack(false);

		setCameraPos(2);

		yield return new WaitForSeconds(0.5f);

		//Can we talk in private? Like in your room maybe?
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Yeah, I mean, is everything okay?
		setCameraPos(3);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Just hang on!
		setCameraPos(2);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		fader.fadeToBlack(true);

		yield return new WaitForSeconds(1);

		el.transform.position = standingPositions[2].position;
		el.transform.rotation = standingPositions[2].rotation;

		sa.transform.position = standingPositions[3].position;
		sa.transform.rotation = standingPositions[3].rotation;

		setCameraPos(4);

		fader.fadeToBlack(false);

		yield return new WaitForSeconds(1);

		//Okay… You won’t believe it! Oh my god oh my god oh my god!
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Sasha, calm down. Gee… what’s going on?
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Okay… My dad was trading with some merchants coming from the south, right?
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Yeah yeah, get on with it.
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Right, so one of them mentioned a bigger city about 3 weeks from here.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Sasha!
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Fine! Get this… it’s run by a man named:
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		yield return new WaitForSeconds(1f);

		//Titus Dwyer!
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Look at Eleanors face
		setCameraPos(5);		

		yield return new WaitForSeconds(1f);

		//Eleanor! It’s gotta be your dad!
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//But... well… I mean that’s impossible...
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//I don’t think that’s a very common name. Especially now ... you know.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//I know. But still… It can’t be him. I have seen the city it was completely overrun.
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Have some faith! What if it is him? We have to find out!
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//I haven’t seen him in such a long time. 
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//No it’s not! Well, yes it is! Don’t you want to know though!? And don’t you want to leave this place? 
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Well… yeah.
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Then what’s stopping you?
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		// Little pause
		setCameraPos(5);
		yield return new WaitForSeconds(1);

		//Exactly.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Well, do you think maybe we could ride with the traders at your house?
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Unfortunately no, they are waking up bright and early to head north. Our destination would be pretty far south.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Damn it, and our people wont make a trip south until spring and especially not that far.
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Well you know... we could also just leave together. Just you and me.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//What? No that’s crazy!
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//El, think about it! You want to find your father and I’ve wanted to leave here for a long time already. It is a win-win situation.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Three weeks? It’s just way too dangerous... what if something happens or one of us gets infected? What then?
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//All the merchants get through. The trading routes are in good condition.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//But still.
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//All I am saying is that we can easily make it. I am grown up now and you are in a few months.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//Okay, but what about Max and Miranda?
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//Your stepparents will understand.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		//What will I say to them? What if they wont let me leave?
		setCameraPos(5);
		elStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		elStopTalking();

		//You don’t have to say anything.
		setCameraPos(4);
		saStartTalking();
		yield return new WaitForSeconds(clips[clip].length);
		saStopTalking();

		setCameraPos(5);

		// Decision Talk to parents or not
		string[] labels = { "OK lets go", "I have to Talk to them" };
		dialogHUD.displayDialog(labels, false);

		while (!dialogHUD.decided)
		{
			yield return new WaitForEndOfFrame();
		}

		switch (dialogHUD.decision)
		{
			case 1:
				StartCoroutine(okLetsGo());
				break;
			case 2:
				StartCoroutine(iHaveToTalkToThem());
				break;
			default:
				break;
		}
	}

	IEnumerator okLetsGo()
	{
		setCameraPos(5);
		elAudio.clip = okLetsGoClips[okLetsGoClip];
		elAudio.Play();
		yield return new WaitForSeconds(okLetsGoClips[okLetsGoClip].length);
		okLetsGoClip++;

		setCameraPos(4);
		saAudio.clip = okLetsGoClips[okLetsGoClip];
		saAudio.Play();
		yield return new WaitForSeconds(okLetsGoClips[okLetsGoClip].length);
		okLetsGoClip++;

		yield return new WaitForSeconds(0.5f);

		fader.fadeToBlack(true);

		yield return new WaitForSeconds(1f);

		Application.LoadLevel(2);
	}

	IEnumerator iHaveToTalkToThem()
	{
		setCameraPos(5);
		elAudio.clip = ihaveToTalktToThemFirstClips[ihaveToTalktToThemFirstClip];
		elAudio.Play();
		yield return new WaitForSeconds(ihaveToTalktToThemFirstClips[ihaveToTalktToThemFirstClip].length);
		ihaveToTalktToThemFirstClip++;

		setCameraPos(4);
		saAudio.clip = ihaveToTalktToThemFirstClips[ihaveToTalktToThemFirstClip];
		saAudio.Play();
		yield return new WaitForSeconds(ihaveToTalktToThemFirstClips[ihaveToTalktToThemFirstClip].length);
		ihaveToTalktToThemFirstClip++;

		yield return new WaitForSeconds(0.5f);

		fader.fadeToBlack(true);

		yield return new WaitForSeconds(1f);

		Application.LoadLevel(2);
	}

	void elStartTalking()
	{
		elAudio.clip = clips[clip];
		elAudio.Play();
	}

	void elStopTalking()
	{
		clip++;
		elAudio.Stop();
	}

	void saStartTalking()
	{
		saAudio.clip = clips[clip];
		saAudio.Play();
	}

	void saStopTalking()
	{
		clip++;
		saAudio.Stop();
	}

	void setCameraPos(int pos)
	{
		dialogCamera.transform.position = cameraPositions[pos].position;
		dialogCamera.transform.rotation = cameraPositions[pos].rotation;
	}
}