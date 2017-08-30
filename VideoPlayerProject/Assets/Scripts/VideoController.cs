using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;

public class VideoController : MonoBehaviour {

	public VideoPlayer video;
	public Slider slider;

	//properties of the video player
	bool isDone;

	public bool IsPlaying {
		get { return video.isPlaying; }
	}

	public bool IsLooping {
		get { return video.isLooping; }
	}

	public bool IsPrepared {
		get { return video.isPrepared; }
	}

	public bool IsDone {
		get { return isDone; }
	}

	public double Time {
		get { return video.time; }
	}

	public ulong Duration {
		get { return (ulong)(video.frameCount / video.frameRate); }
	}

	public double NTime {
		get { return Time / Duration; }
	}

	void OnEnable() {
		video.errorReceived += errorReceived;
		video.frameReady += frameReady;
		video.loopPointReached += loopPointReached;
		video.prepareCompleted += prepareCompleted;
		video.seekCompleted += seekCompleted;
		video.started += started;
	}

	void OnDisable() {
		video.errorReceived -= errorReceived;
		video.frameReady -= frameReady;
		video.loopPointReached -= loopPointReached;
		video.prepareCompleted -= prepareCompleted;
		video.seekCompleted -= seekCompleted;
		video.started -= started;
	}

	void errorReceived(VideoPlayer v, string msg) {
		Debug.Log("video player error: "+msg);
	}

	void frameReady(VideoPlayer v, long frame) {
		//cpu tax is heavy
	}

	void loopPointReached(VideoPlayer v) {
		Debug.Log("video player loop point reached");
		isDone = true;
	}

	void prepareCompleted(VideoPlayer v) {
		Debug.Log("video player finished preparing");
		isDone = false;
	}

	void seekCompleted(VideoPlayer v) {
		Debug.Log("video player finished seeking");
		isDone = false;
	}

	void started(VideoPlayer v) {
		Debug.Log("video player started");
	}

	void Update() {
		if (!IsPrepared) return;

		slider.value = (float)NTime;
	}

	public void LoadVideo(string name) {
		string temp = Application.dataPath + "/Videos/" + name; /*.mp4,.avi,.mov*/
		if (video.url == temp) return;

		video.url = temp;
		video.Prepare();

		Debug.Log("can set direct audio volume: " +video.canSetDirectAudioVolume);
		Debug.Log("can set playback speed: "+video.canSetPlaybackSpeed);
		Debug.Log("can set skip on drop: "+video.canSetSkipOnDrop);
		Debug.Log("can set time: "+video.canSetTime);
		Debug.Log("can step: "+video.canStep);
	}

	public void PlayVideo() {
		if (!IsPrepared) return;
		video.Play();
	}

	public void PauseVideo() {
		if (!IsPlaying) return;
		video.Pause();
	}

	public void RestartVideo() {
		if (!IsPrepared) return;
		PauseVideo();
		Seek(0);
	}

	public void LoopVideo(bool toggle) {
		if (!IsPrepared) return;
		video.isLooping = toggle;
	}

	public void Seek(float nTime) {
		if (!video.canSetTime) return;
		if (!IsPrepared) return;
		nTime = Mathf.Clamp(nTime, 0, 1);
		video.time = nTime * Duration;
	}

	public void IncrementPlaybackSpeed() {
		if (!video.canSetPlaybackSpeed) return;

		video.playbackSpeed += 1; 
		video.playbackSpeed = Mathf.Clamp(video.playbackSpeed, 0, 10);
	}

	public void DecrementPlaybackSpeed() {
		if (!video.canSetPlaybackSpeed) return;

		video.playbackSpeed -= 1; 
		video.playbackSpeed = Mathf.Clamp(video.playbackSpeed, 0, 10);
	}
}
