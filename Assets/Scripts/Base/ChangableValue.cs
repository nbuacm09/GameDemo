using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class ChangableValue<T>
{
	public enum ChangeType
	{
		MULTI,
		ADD
	};
	public struct ChangeMethod
	{
		public WeakReference obj;
		public ChangeType type;
		public double changedVal;
		public void Operate(ref double val)
		{
			switch(type)
			{
			case ChangeType.ADD:
				val += changedVal;
				break;
			case ChangeType.MULTI:
				val *= changedVal;
				break;
			}
		}
	}

	public BaseDelegateV<T> onValueChanged;

	LinkedList<ChangeMethod> changes = new LinkedList<ChangeMethod>();
	bool dirty = true;
	T val;
	T finalVal;

	public void Init(T val)
	{
		ClearChange ();
		Set (val);
	}

	protected abstract double ToDouble (T val);
	protected abstract T FromDouble (double val);

	public void Set(T val)
	{
		this.val = val;
		dirty = true;
	}

	public void AddChangeMethod(ChangeMethod method)
	{
		changes.AddLast(method);
		dirty = true;
	}

	public void ClearChange()
	{
		changes.Clear ();
		dirty = true;
	}

	public T OriginalValue {
		get{
			return val;
		}
	}

	public T Value {
		get{
			if (dirty) {
				double ret = ToDouble (val);
				var it = changes.First;
				while (it != null) {
					var cur = it;
					it = it.Next;
					var method = cur.Value;
					if (method.obj.IsAlive == false) {
						changes.Remove (cur);
					} else {
						method.Operate (ref ret);
					}
				}
				finalVal = FromDouble (ret);
				dirty = false;
				if (onValueChanged != null) {
					onValueChanged (finalVal);
				}
			}
			return finalVal;
		}
	}
}


public class ChangableInt : ChangableValue<int> {
	protected override double ToDouble (int val) {
		return (double)val;
	}
	protected override int FromDouble (double val) {
		return (int)val;
	}
}

public class ChangableLong : ChangableValue<long> {
	protected override double ToDouble (long val) {
		return (double)val;
	}
	protected override long FromDouble (double val) {
		return (long)val;
	}
}