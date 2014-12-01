using UnityEngine;
using System.Collections;

public class HumanEnemySight : MonoBehaviour 
{
	public float fieldOfViewAngle = 110;

	private GameObject player;
	private GameObject friend;
	private HumanEnemyAI ai;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		friend = GameObject.FindGameObjectWithTag("Friend");
		ai = GetComponent<HumanEnemyAI>();
	}

	void FixedUpdate()
	{
		if((player.transform.position - transform.position).magnitude <= 20f)
		{
			Vector3 direction = player.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				Physics.Raycast(transform.position + Vector3.up, (player.gameObject.transform.position + Vector3.up) - (transform.position + Vector3.up), out hit, 20f);

				if(hit.collider.gameObject == player)
				{
					ai.playerPosition = player.transform.position;
					ai.seesPlayer = true;
					Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.blue, 5f);
				}
				else
				{	
					ai.seesPlayer = false;
				}
			}
			else
			{	
				ai.seesPlayer = false;
			}
		}
		else
		{	
			ai.seesPlayer = false;
		}

		if((friend.transform.position - transform.position).magnitude <= 20f)
		{
			Vector3 direction = friend.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				Physics.Raycast(transform.position + Vector3.up, (friend.gameObject.transform.position + Vector3.up) - (transform.position + Vector3.up), out hit, 20f);
				
				if(hit.collider.gameObject == friend)
				{
					ai.friendPosition = friend.transform.position;
					ai.seesFriend = true;
					Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.blue, 5f);
				}
				else
				{	
					ai.seesFriend = false;
				}
			}
			else
			{	
				ai.seesFriend = false;
			}
		}
		else
		{	
			ai.seesFriend = false;
		}
	}
}
