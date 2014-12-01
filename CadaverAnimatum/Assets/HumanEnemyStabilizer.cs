using UnityEngine;
using System.Collections;

public class HumanEnemyStabilizer : MonoBehaviour 
{
	public GameObject thisEnemy;

	private Animator anim;

	void Start()
	{
		anim = thisEnemy.GetComponent<Animator>();
	}

	void LateUpdate()
	{
		if(anim.GetBool("Aiming"))
		{
			transform.rotation = Quaternion.Euler(anim.GetFloat("Aim_Angle"), thisEnemy.transform.rotation.eulerAngles.y, 0) * Quaternion.Euler(0, -90, -180);
		}
	}
}
