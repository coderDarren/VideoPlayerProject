using UnityEngine;
using UnityEngine.UI;

public class VideoTime : MonoBehaviour {

	public VideoController video;
	public Text time;
	public Text duration;

	int secondsPassed;
	int minutesPassed;
	int totalSeconds;
	int totalMinutes;
	
	void Update() {
		if (video.IsPrepared) {
			totalMinutes = (int)video.Duration / 60;
			totalSeconds = (int)video.Duration - totalMinutes * 60;
			minutesPassed = (int)video.Time / 60;
			secondsPassed = (int)video.Time - minutesPassed * 60;

			time.text = string.Format("{0:00}:{1:00}", minutesPassed, secondsPassed);
			duration.text = string.Format("{0:00}:{1:00}", totalMinutes, totalSeconds);
		}
	}
}
