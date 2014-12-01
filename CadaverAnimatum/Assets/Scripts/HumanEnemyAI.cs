using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanEnemyAI : MonoBehaviour 
{
	public GameObject currentEnemy;
	public GameObject cover;
	private GameObject player;
	private GameObject friend;

	public EnemyPath path;
	public Transform currentPathTarget;
	
	public bool shotsFell;

	public bool seesPlayer;
	public bool hearsPlayer;

	public bool seesFriend;
	public bool hearsFriend;

	public Vector3 playerPosition;
	public Vector3 friendPosition;

	public enum States
	{
		idling,
		followingPath,
		searching,
		combat,
		isHostage,
		surrenders
	};
	public States state;

	public enum CombatStates
	{
		approaches,
		standoff,
		takesCover,
		behindCover,
		attacks
	};
	public CombatStates combatState;

	private Animator anim;
	private NavMeshAgent agent;

	private HumanEnemyShooting shooting;
	private HumanEnemyMovement movement;

	private float timeTillShoot;

	void Start () 
	{
		player  = GameObject.FindGameObjectWithTag("Player"); 
		friend  = GameObject.FindGameObjectWithTag("Friend"); 
		currentEnemy = player;

		anim = GetComponent<Animator>();

		shooting = GetComponent<HumanEnemyShooting>();
		movement = GetComponent<HumanEnemyMovement>();

		currentPathTarget = path.pathNodes[0];
	}
	
	void Update () 
	{
		if(anim.GetBool("Dead") || player.GetComponent<Animator>().GetBool("Dead"))
		{
			return;
		}

		if(shotsFell)
		{
			state = States.combat;
		}

		switch(state)
		{
		case States.idling:
			idling();
			break;
		case States.followingPath:
			if(seesPlayer || seesFriend)
				state = States.combat;
			followingPath();
			break;
		case States.searching:
			searching();
			break;
		case States.combat:
			combat();
			break;
		case States.isHostage:
			isHostage();
			break;
		case States.surrenders:
			surrenders();
			break;
		}
	}

	void idling()
	{
		if(player.GetComponent<Animator>().GetBool("Dead"))
		{
			state = States.idling;
		}

		if((seesPlayer || seesFriend) && !(friend.GetComponent<Animator>().GetBool("Dead") && player.GetComponent<Animator>().GetBool("Dead")))
		{
			state = States.combat;
		}
		if(hearsPlayer || hearsFriend)
		{
			state = States.searching;
			return;
		}

		if(movement.isMoving)
		{		
			movement.isMoving = false;
		}

		if(movement.aiming)
		{
			movement.aiming = false;
		}
	}

	void followingPath()
	{
		if((transform.position - currentPathTarget.position).magnitude <= 0.5f)
		{
			currentPathTarget = path.returnNextPosition(currentPathTarget);
			movement.destination = currentPathTarget.position;
		}
		else
		{
			movement.destination = currentPathTarget.position;
		}

		movement.isMoving = true;
		movement.speed = 0.6f;
		movement.aiming = false;
	}

	void searching()
	{
		if(seesPlayer || seesFriend)
		{
			movement.isMoving = false;
			state = States.combat;
			return;
		}

		if((transform.position - playerPosition).magnitude <= 1 || (transform.position - friendPosition).magnitude <= 1)
		{
			state = States.idling;
			return;
		}
		
		if(hearsPlayer)
		{
			movement.destination = playerPosition;
		}
		else if(hearsFriend)
		{
			movement.destination = friendPosition;
		}

		movement.isMoving = true;
		movement.speed = 1f;
		movement.aiming = false;
	}

	void combat()
	{
		if(player.GetComponent<Animator>().GetBool("Dead"))
		{
			state = States.idling;
		}

		currentEnemy = selectingOponent();

		switch(combatState)
		{
		case CombatStates.approaches:
			approaches();
			break;
		case CombatStates.standoff:
			standoff();
			break;
		case CombatStates.takesCover:
			takesCover();
			break;
		case CombatStates.behindCover:
			behindCover();
			break;
		case CombatStates.attacks:
			attacks();
			break;
		}
	}

	GameObject selectingOponent()
	{
		if(player.GetComponent<Animator>().GetBool("Dead"))
		{
			return friend;
		}

		if(friend.GetComponent<Animator>().GetBool("Dead"))
		{
			return player;
		}

		if((transform.position - player.transform.position).magnitude <= (transform.position - friend.transform.position).magnitude)
		{
			return player;
		}
		else
		{
			return friend;
		}
	}

	void isHostage()
	{

	}

	void surrenders()
	{

	}

	void approaches()
	{
		if(player.GetComponent<Inventory>().secondaryGun != null || friend.GetComponent<Inventory>().secondaryGun != null)
		{
			combatState = CombatStates.standoff;
		}

		if(currentEnemy.tag == "Player")
		{
			if((playerPosition - transform.position).magnitude >= 3)
			{
				movement.destination = playerPosition;
				movement.speed = 1;
				movement.isMoving = true;
			}
			else
			{
				if(!seesPlayer)
				{
					state = States.idling;
				}

				movement.destination = playerPosition;
				movement.speed = 0;
				movement.isMoving = true;
			}
		}
		else
		{
			if((friendPosition - transform.position).magnitude >= 3)
			{
				movement.destination = friendPosition;
				movement.speed = 1;
				movement.isMoving = true;
			}
			else
			{
				if(!seesFriend)
				{
					state = States.idling;
				}
				
				movement.destination = playerPosition;
				movement.speed = 0;
				movement.isMoving = true;
			}
		}
	}

	void standoff()
	{
		if(shotsFell)
		{
			combatState = CombatStates.takesCover;
		}

		movement.rotate(currentEnemy.transform.position - transform.position);
		movement.isMoving = true;
		movement.speed = 0f;
		movement.aiming = true;
	}

	void takesCover()
	{
		GameObject[] covers = GameObject.FindGameObjectsWithTag("Cover");
		List<GameObject> safeCovers = new List<GameObject>();

		for(int i = 0; i <= covers.Length - 1; i++)
		{
			CoverNode node = covers[i].GetComponent<CoverNode>();
			if(node.personInCover == gameObject)
			{
				node.personInCover = null;
			}

			if(node.safe && (node.personInCover == null))
			{
				safeCovers.Add(covers[i]);
			}
		}

		covers = safeCovers.ToArray();

		if(covers.Length == 0)
		{
			combatState = CombatStates.attacks;
			return;
		}

		cover = covers[0];

		for(int i = 0; i <= covers.Length - 1; i++)
		{
			if((cover.transform.position - transform.position).magnitude > (covers[i].transform.position - transform.position).magnitude)
			{
				cover = covers[i];
			}
		}

		cover.GetComponent<CoverNode>().personInCover = gameObject;
		
		movement.destination = cover.transform.position;
		movement.rotate(currentEnemy.transform.position - transform.position);
		movement.isMoving = true;
		movement.speed = 1f;

		if(currentEnemy.tag == "Player")
		{
			if(seesPlayer)
			{
				shooting.shoot();
			}
		}
		else if(currentEnemy.tag == "Friend")
		{
			if(seesFriend)
			{
				shooting.shoot();
			}
		}

		if((cover.transform.position - transform.position).magnitude <= 1)
		{
			combatState = CombatStates.behindCover;
		}
	}

	bool isShootingFromCover;
	float timeBehindCover;

	void behindCover()
	{
		if(!cover.GetComponent<CoverNode>().safe)
		{
			combatState = CombatStates.takesCover;
		}

		CoverNode node = cover.GetComponent<CoverNode>();

		timeBehindCover += Time.deltaTime;
		if(timeBehindCover >= Random.Range(1,7))
		{
			isShootingFromCover = !isShootingFromCover;
			timeBehindCover = 0f;
		}

		if(!isShootingFromCover)
		{
			movement.isMoving = false;
			transform.position = Vector3.Lerp(transform.position, node.coverPosition.position, 1);
			transform.rotation = Quaternion.Lerp(transform.rotation, node.coverPosition.rotation, 1);
		}
		else
		{
			movement.isMoving = true;
			movement.speed = 0;
			transform.position = Vector3.Lerp(transform.position, node.shootingPosition.position, 1);
			if(seesPlayer)
			{
				movement.rotate(currentEnemy.transform.position - transform.position);
				shooting.shoot();
			}
			else 
			{
				movement.rotate(-node.shootingPosition.transform.forward);
			}
		}

	}

	void attacks()
	{
		if(currentEnemy.GetComponent<Animator>().GetBool("Dead"))
		{
			state = States.idling;
		}

		movement.rotate(currentEnemy.transform.position - transform.position );
		movement.isMoving = true;
		movement.speed = 0f;
		movement.aiming = true;

		shooting.shoot();
	}
}
