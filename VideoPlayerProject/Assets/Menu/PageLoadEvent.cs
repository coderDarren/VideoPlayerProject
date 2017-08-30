using UnityEngine;
using System.Collections;

namespace Menu {

	public class PageLoadEvent : ButtonEvent {

		public PageType pageToLoad;
		public PageType pageToRemove;
		public PageType[] additionalPagesToRemove;

		public override void OnItemEnter()
		{
			if (pageToRemove != PageType.NONE)
			{
				for (int i = 0; i < additionalPagesToRemove.Length; i++) {
					//Debugger.Log("Removing Page: "+additionalPagesToRemove[i], DebugFlag.TASK);
					PageManager.Instance.TurnOffPage(additionalPagesToRemove[i], PageType.NONE);
				}
				//Debugger.Log("Removing Page: "+pageToRemove, DebugFlag.TASK);
				PageManager.Instance.TurnOffPage(pageToRemove, pageToLoad);
			}
			else
			{
				PageManager.Instance.LoadPage(pageToLoad);
			}

			base.OnItemUp();
		}
	}
}
