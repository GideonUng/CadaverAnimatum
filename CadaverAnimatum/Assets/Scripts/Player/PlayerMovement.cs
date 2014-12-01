using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;
	public bool canAim = false;

	private Transform cameraPosition;
	private Animator anim;
	
	void Start()
	{
		cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate ()
	{		
		if (!anim.GetBool("Dead")){
			bool aiming = Input.GetButton("Aim");
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			bool sneak = Input.GetButton("Sneak");
			float run = Input.GetAxis("Run");

			if(!canAim){
				MovementManagement(h, v, sneak, run);
			} else {
				anim.SetBool("Aiming", aiming); 

				if(aiming){
					StraiveMoveManagement(h, v, sneak, run);
				} else {
					MovementManagement(h, v, sneak, run);
				}
			}
		}

		if (anim.GetBool("Dead"))
		{
			anim.SetBool("Aiming", false);
		}
	}

	void MovementManagement (float horizontal, float vertical, bool sneaking, float run)
	{
		anim.SetBool("Sneaking", sneaking);
		
		if(horizontal != 0f || vertical != 0f){
			Rotating(horizontal, vertical);
			anim.SetFloat("Speed", 1 + run*5, speedDampTime, Time.deltaTime);
		} else {
			anim.SetFloat("Speed", 0);
		}
	}

	void StraiveMoveManagement(float horizontal, float vertical, bool sneaking, float run)
	{
		anim.SetFloat("Speed", vertical);
		anim.SetFloat("Speed_Sideways", horizontal);

		if (!anim.GetBool("Dead"))
		{
			transform.rotation = Quaternion.AngleAxis(cameraPosition.transform.rotation.eulerAngles.y, Vector3.up);
		}
	}
	
	void Rotating (float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		targetDirection = Quaternion.AngleAxis(cameraPosition.eulerAngles.y, Vector3.up) * targetDirection;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		rigidbody.MoveRotation(newRotation);
	}
}