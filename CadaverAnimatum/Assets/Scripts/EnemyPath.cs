using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPath : MonoBehaviour 
{
	public Transform[] pathNodes;
	public bool loops;
	public bool isACircle;

	private bool ascending;

	public Transform returnNextPosition(Transform currentPosition)
	{
		int index = System.Array.IndexOf(pathNodes, currentPosition);
		if(ascending)
		{
			if(index != pathNodes.Length - 1)
			{
				return pathNodes[index + 1];
			}
			else 
			{
				if(loops)
				{
					if(!isACircle)
					{
						ascending = false;
						return pathNodes[index - 1];
					}
					else
					{
						return pathNodes[0];
					}
				}
				else
				{
					return pathNodes[index];
				}
			}
		}
		else
		{
			if(index != 0)
			{
				return pathNodes[index - 1];
			}
			else 
			{
				if(loops)
				{
					ascending = true;
					return pathNodes[index + 1];
				}
				else
				{
					return pathNodes[index];
				}	
			}
		}
	}
}
