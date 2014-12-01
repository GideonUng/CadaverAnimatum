using UnityEngine;
using System.Collections;

public class HumanEnemyMovement : MonoBehaviour 
{
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	public Vector3 destination;
	public bool isMoving;

	public float speed;
	public bool aiming;
	public bool sneaking;

	private Animator anim;
	private NavMeshAgent agent;
	
	void Start()
	{
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	void FixedUpdate ()
	{	
		anim.SetBool("Aiming", aiming);

		if(isMoving)
		{
			agent.SetDestination(destination);

			if(aiming)
			{
				StraiveMoveManagement();
			} 
			else 
			{
				MovementManagement();
				anim.SetFloat("Speed_Sideways", 0);
			}
		}
		else
		{
			anim.SetFloat("Speed", 0);
			anim.SetFloat("Speed_Sideways", 0);
		}
	}
	
	void MovementManagement ()
	{
		rotate(agent.steeringTarget - transform.position);

		anim.SetFloat("Speed", speed);
		anim.SetBool("Sneaking", sneaking);
	}
	
	void StraiveMoveManagement()
	{
		Vector3 relPos = (transform.position - agent.steeringTarget);
		relPos = Quaternion.AngleAxis(-transform.rotation.eulerAngles.y, Vector3.up) * relPos;
		relPos.Normalize();

		anim.SetFloat("Speed", -relPos.z * speed);
		anim.SetFloat("Speed_Sideways", -relPos.x * speed);
		anim.SetBool("Sneaking", sneaking);
	}
	
	public void rotate(Vector3 target)
	{
		if(target.x != 0 && target.z != 0){
			Quaternion targetRotation = Quaternion.LookRotation(new Vector3(target.x, 0, target.z), Vector3.up);
			Quaternion newTargetRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			transform.rotation = newTargetRotation;
		}
	}
}
