using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoJumpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public VideoController video;
	public Image bar;

	bool hovering;
	float pos;

	void Update() {
		if (!hovering) return;

		pos = Input.mousePosition.x / Screen.width;
		bar.fillAmount = pos;
	}

	public void OnPointerEnter(PointerEventData ped) {
		hovering = true;
	}
	
	public void OnPointerExit(PointerEventData ped) {
		bar.fillAmount = 0;
		hovering = false;
	}

	public void OnPointerClick(PointerEventData ped) {
		video.Seek(pos);
	}

}
