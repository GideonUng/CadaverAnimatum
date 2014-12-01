using UnityEngine;
using System.Collections;

public class HumanEnemyHearing : MonoBehaviour 
{
	public HumanEnemyAI ai;

	void Start()
	{
		ai = GetComponent<HumanEnemyAI>();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(!other.gameObject.GetComponent<Animator>().GetBool("Sneaking") && other.gameObject.GetComponent<Animator>().GetFloat("Speed") >= 1.1f)
			{
				ai.playerPosition = other.gameObject.transform.position;
				ai.hearsPlayer = true;
			}
			else
			{
				ai.hearsPlayer = false;
			}
		}
		else if(other.gameObject.tag == "Friend")
		{
			if(!other.gameObject.GetComponent<Animator>().GetBool("Sneaking") && other.gameObject.GetComponent<Animator>().GetFloat("Speed") >= 1.1f)
			{
				ai.friendPosition = other.gameObject.transform.position;
				ai.hearsFriend = true;
			}
			else
			{
				ai.hearsFriend = false;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			ai.hearsPlayer = false;
		}
		else if(other.gameObject.tag == "Friend")
		{
			ai.hearsFriend = false;
		}
	}
}