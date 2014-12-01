using UnityEngine;
using System.Collections;

public class FinishGatherSupliesQuest : MonoBehaviour 
{
	public Inventory inventory;

	public DoorOpen doorToUnlock;

	public EnableQuest enableQuest;

	void Update () 
	{
		if(inventory.hasBackpack && inventory.secondaryGun != null)
		{
			enableQuest.enabelQuest(2);
			doorToUnlock.closed = false;

			gameObject.GetComponent<FinishGatherSupliesQuest>().enabled = false;
		}
	}
}
