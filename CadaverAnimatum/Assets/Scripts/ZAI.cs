using UnityEngine;
using System.Collections;

public class ZAI : MonoBehaviour
{
	public Transform player;
	public float turnSmoothing;
	public float stoppingDisdance;

	private Animator anim;
	private Animator playerAnim;
	private NavMeshAgent agent;

	void Start()
	{
		anim = GetComponent<Animator>();
		playerAnim = player.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			anim.SetBool("PlayerInSight", true);
			Debug.Log("Player entered");
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!anim.GetBool("Dead"))
			{
				float distance = (player.position - transform.position).magnitude;

				agent.SetDestination(player.position);
				rotate(agent.steeringTarget - transform.position);

				if (distance <= stoppingDisdance)
				{
					playerAnim.SetBool("Dead", true);

					if (anim.GetBool("PlayerInSight"))
					{
						anim.SetBool("PlayerInSight", false);
						Debug.Log("Player dead");
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			anim.SetBool("PlayerInSight", false);
		}
	}

	void rotate(Vector3 target)
	{
		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(target.x, 0, target.z), Vector3.up);
		Quaternion newTargetRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		transform.rotation = newTargetRotation;
	}
}