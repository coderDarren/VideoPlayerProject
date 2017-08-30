using UnityEngine;
using System.Collections.Generic;

namespace Menu
{
	public class PageManager : MonoBehaviour {

		public static PageManager Instance;

		public List<PageController> pagePrefabs;
		public PageType entryPage;
		
		List<PageController> _activePages;
		List<PageController> _inactivePages;

		void Awake()
		{
			Instance = this;
			_activePages = new List<PageController>();
			_inactivePages = new List<PageController>();
			LoadPage(entryPage); //first page to load in the scene
		}

		/// <summary>
		/// Creates the page of type _pageType if it does not exist already
		/// This page is placed into _activePages
		/// </summary>
		public void LoadPage(PageType _pageType)
		{
			if (GetPage(_pageType) != null)
			{
				//Debugger.Log("Trying to load a page that is already active.", DebugFlag.TASK);
				return; //this page already exists
			}

			for (int i = 0; i < pagePrefabs.Count; i++)
			{
				if (_pageType == pagePrefabs[i].pageType)
				{
					GameObject _go = Instantiate(pagePrefabs[i].gameObject) as GameObject;
					PageController _page = _go.GetComponent<PageController>();
					RectTransform _rect = _go.GetComponent<RectTransform>();
					_rect.SetParent(GetComponent<RectTransform>());
					_rect.localScale = Vector2.one;
					_rect.anchoredPosition = Vector2.zero;
					_rect.offsetMin = Vector2.zero;
					_rect.offsetMax = Vector2.zero;
					_activePages.Add(_page);
					return; //already found the page to load
				}
			}
		}

		/// <summary>
		/// Deactivates the page of type _pageType and prepares it to animate out
		/// This page is placed into _inactivePages
		/// </summary>
		public void TurnOffPage(PageType _pageType, PageType _nextPage)
		{
			PageController _page = GetPage(_pageType);
			if (!_page)
			{
				//Debugger.LogWarning("Attempting to turn off a page that does not exist.");
				return;
			}
			_activePages.Remove(_page);
			_inactivePages.Add(_page);
			_page.ExitView(_nextPage);
		}

		/// <summary>
		/// Destroys the page of type _pageType and removes it from _inactivePages
		/// Called via PageController
		/// </summary>
		public void DestroyPage(PageType _pageType)
		{
			for (int i = _inactivePages.Count - 1; i >= 0; i--)
			{
				if (_inactivePages[i].pageType == _pageType)
				{
					PageController _p = _inactivePages[i];
					_inactivePages.RemoveAt(i);
					Destroy(_p.gameObject);
					//Debugger.Log(_pageType+" page was destroyed.", DebugFlag.TASK);
					return;
				}
			}
		}

		public bool PageIsExiting(PageType pageType)
		{
			for (int i = _inactivePages.Count - 1; i >= 0; i--)
			{
				if (pageType == _inactivePages[i].pageType)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Looks for the page of type _pageType inside _activePages and returns the PageController associated with it
		/// Returns null if the page of type _pageType could not be found
		/// </summary>
		PageController GetPage(PageType _pageType)
		{
			for (int i = 0; i < _activePages.Count; i++)
			{
				if (_pageType == _activePages[i].pageType)
				{
					return _activePages[i];
				}
			}
			return null;
		}
	}
}
