using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class VideoEvent : ButtonEvent {

	public enum EventType {
		Play,
		Pause,
		Replay,
		LoopOn,
		LoopOff,
		SpeedUp,
		SlowDown,
		Load
	}
	public EventType eventType;
	public string videoName;
	public Sprite playImage;
	public Sprite pauseImage;
	public Sprite restartImage;
	public Color toggleOnColor;
	public Color toggleOffColor;

	VideoController video;

	void Start() {
		InitButton();
		video = GameObject.FindObjectOfType<VideoController>();
		Configure();	
	}

	void Configure() {
		switch (eventType) {
			case EventType.Play:
			case EventType.Pause:
			case EventType.Replay:
				if (video.IsDone) {
					_image.sprite = restartImage;
					eventType = EventType.Replay;
				}
				else if (video.IsPlaying) {
					_image.sprite = pauseImage;
					eventType = EventType.Pause;
				}
				break;
			case EventType.LoopOn:
			case EventType.LoopOff:
				if (video.IsLooping) {
					_image.color = toggleOnColor;
					eventType = EventType.LoopOff;
				}
				break;
		}
	}

	public override void OnItemDown() {
		base.OnItemDown();

		switch (eventType) {
			case EventType.Play:
				video.PlayVideo();
				eventType = EventType.Pause;
				_image.sprite = pauseImage;
				break;
			case EventType.Pause:
				video.PauseVideo();
				eventType = EventType.Play;
				_image.sprite = playImage;
				break;
			case EventType.Replay:
				video.Seek(0);
				video.PlayVideo();
				eventType = EventType.Pause;
				_image.sprite = pauseImage;
				break;
			case EventType.LoopOn:
				video.LoopVideo(true);
				eventType = EventType.LoopOff;
				_image.color = toggleOnColor; 
				break;
			case EventType.LoopOff:
				video.LoopVideo(false);
				eventType = EventType.LoopOn;
				_image.color = toggleOffColor; 
				break;
			case EventType.SpeedUp:
				video.IncrementPlaybackSpeed();
				break;
			case EventType.SlowDown:
				video.DecrementPlaybackSpeed();
				break;
			case EventType.Load:
				video.LoadVideo(videoName);
				break;
		}
	}
}
