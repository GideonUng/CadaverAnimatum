using UnityEngine;
using System.Collections;

public class CoverNode : MonoBehaviour 
{
	public bool onLeftSide;
	public Transform coverPosition;
	public Transform shootingPosition;

	public bool safe;
	public GameObject personInCover;

	private GameObject player;
	private GameObject friend;

	private Vector3 bounds;
	private Vector3 raypos;

	private RaycastHit hitPlayer;
	private RaycastHit hitFriend;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		friend = GameObject.FindGameObjectWithTag("Friend");

		bounds = collider.bounds.size;
		if(onLeftSide)
		{			
			raypos = transform.position + (transform.forward * bounds.z + transform.right * bounds.x + transform.up * bounds.y) / 2;
		}
		else
		{		
			raypos = transform.position + (transform.forward * bounds.z - transform.right * bounds.x + transform.up * bounds.y) / 2;
		}
	}

	void FixedUpdate()
	{
		Vector3 playerRelpos = player.transform.position + Vector3.up - raypos;
		Vector3 friendRelpos = friend.transform.position + Vector3.up - raypos;

		Physics.Raycast(raypos, playerRelpos, out hitPlayer);
		Physics.Raycast(raypos, friendRelpos, out hitFriend);

		if(Vector3.Angle(-transform.forward, player.transform.position - transform.position) >= 45)
		{
			safe = false;
			return;
		}
		Debug.Log((Vector3.Angle(-transform.forward, player.transform.position - transform.position) <= 45 )+ gameObject.ToString());
		if(hitPlayer.collider.gameObject == player || hitFriend.collider.gameObject == friend)
		{
			safe = false;
			return;
		}
		else
		{
			safe = true;
		}
	}
}
