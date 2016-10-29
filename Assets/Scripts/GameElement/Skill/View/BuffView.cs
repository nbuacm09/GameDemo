using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuffView : MonoBehaviour {
	[SerializeField] Image icon;
	[SerializeField] Image mask;
	[SerializeField] Text stackedCountText;
	BuffBase buff;
	public BuffBase Buff{
		get{
			return buff;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (buff == null) {
			return;
		}

		SetTimeLeftPercent ((float)buff.PassedTime / (buff.TimeLeft + buff.PassedTime));
	}

	protected virtual void OnDestroy () {
		if (buff != null) {
			this.buff.onStackedCountChanged -= OnStackedCountChanged;
			this.buff.onBuffRemoved -= OnBuffRemoved;
		}
	}

	public void Init (BuffBase buff) {
		this.buff = buff;
		InitUI ();
		this.buff.onStackedCountChanged += OnStackedCountChanged;
		this.buff.onBuffRemoved += OnBuffRemoved;
	}

	void InitUI () {
		SetStackedCount (buff.StackedCount);
	}

	void OnStackedCountChanged (int stackedCount) {
		SetStackedCount (stackedCount);
	}

	void OnBuffRemoved (bool timeOver) {
		Destroy (gameObject);
	}

	void SetTimeLeftPercent (float percent) {
		mask.fillAmount = percent;
	}

	void SetStackedCount (int stackedCount) {
		stackedCountText.text = stackedCount.ToString();
	}
}
