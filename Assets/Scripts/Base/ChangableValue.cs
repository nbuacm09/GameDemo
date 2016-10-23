using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum ChangeType {
	MULTI,
	ADD
};

public struct ChangeMethod {
	public WeakReference refObj;
	public ChangeType type;
	public double changedVal;

	public ChangeMethod (ChangeType type, double changedVal, object refObj = null) {
		this.type = type;
		this.changedVal = changedVal;
		this.refObj = refObj == null ? null : new WeakReference(refObj);
	}

	public void Operate(ref double val) {
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

public abstract class ChangableValue<T> {
	public BaseDelegateV<T> onValueChanged;

	LinkedList<ChangeMethod> changes = new LinkedList<ChangeMethod>();
	bool dirty = true;
	T val;
	T finalVal;

	public ChangableValue () {

	}

	public ChangableValue (T val) {
		Init (val);
	}

	public void Init(T val) {
		ClearChange ();
		Set (val);
	}

	protected abstract double ToDouble (T val);
	protected abstract T FromDouble (double val);

	public void Set(T val) {
		this.val = val;
		dirty = true;
	}

	public void AddChangeMethod(ChangeMethod method) {
		changes.AddLast(method);
		dirty = true;
	}

	public void ClearChange() {
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
					if (method.refObj != null && method.refObj.IsAlive == false) {
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
	public ChangableInt () : base() {

	}

	public ChangableInt (int val) : base(val) {
		
	}

	protected override double ToDouble (int val) {
		return (double)val;
	}
	protected override int FromDouble (double val) {
		return (int)val;
	}
}

public class ChangableLong : ChangableValue<long> {
	public ChangableLong () : base() {

	}

	public ChangableLong (long val) : base(val) {

	}

	protected override double ToDouble (long val) {
		return (double)val;
	}
	protected override long FromDouble (double val) {
		return (long)val;
	}
}

public class ChangableDouble : ChangableValue<double> {
	public ChangableDouble () : base() {
	
	}
	public ChangableDouble (double val) : base(val) {

	}

	protected override double ToDouble (double val) {
		return val;
	}
	protected override double FromDouble (double val) {
		return val;
	}
}