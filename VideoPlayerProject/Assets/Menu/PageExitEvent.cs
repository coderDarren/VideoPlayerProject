using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class PageExitEvent : ButtonEvent {

	public PageType pageType;

	PageManager pageManager;

	void Start() {
		pageManager = PageManager.Instance;
	}

	public override void OnItemExit() {
		pageManager.TurnOffPage(pageType, PageType.NONE);
	}
}
