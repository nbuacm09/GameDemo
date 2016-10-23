using UnityEngine;
using System.Collections;

public abstract class BaseObject
{
	static long idCreator;
	long id;
	public long Id
	{
		get
		{
			return id;
		}
	}
	public BaseObject()
	{
		id = idCreator++;
	}

	public virtual void Update(long delteTime)
	{
		
	}
}
