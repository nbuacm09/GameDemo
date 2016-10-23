using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DurationBuffView : MonoBehaviour {
	[SerializeField] Text stackedCountText;
	[SerializeField] Text timeLeftText;
	[SerializeField] Text nameText;
	DurationBuffBase buff;
	public DurationBuffBase Buff{
		get{
			return buff;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void OnDestroy () {
		if (buff != null) {
			this.buff.onTimeLeftChanged -= OnTimeLeftChanged;
			this.buff.onStackedCountChanged -= OnStackedCountChanged;
			this.buff.onBuffRemoved -= OnBuffRemoved;
		}
	}

	public void Init (DurationBuffBase buff) {
		this.buff = buff;
		InitUI ();
		this.buff.onTimeLeftChanged += OnTimeLeftChanged;
		this.buff.onStackedCountChanged += OnStackedCountChanged;
		this.buff.onBuffRemoved += OnBuffRemoved;
	}

	void InitUI () {
		nameText.text = buff.BuffConfig.name;
		SetTimeLeft (buff.TimeLeft);
		SetStackedCount (buff.StackedCount);
	}

	void OnTimeLeftChanged (long timeLeft) {
		SetTimeLeft (timeLeft);
	}

	void OnStackedCountChanged (int stackedCount) {
		SetStackedCount (stackedCount);
	}

	void OnBuffRemoved (bool timeOver) {
		Destroy (gameObject);
	}

	void SetTimeLeft (long timeLeft) {
		timeLeftText.text = StringFunc.GetTimeStringMinLength (timeLeft);
	}


	void SetStackedCount (int stackedCount) {
		stackedCountText.text = stackedCount.ToString();
	}
}
