using UnityEngine;
using System.Collections;

namespace Menu
{

	[RequireComponent(typeof(Animator))]
	public class PageController : MonoBehaviour {

		public PageType pageType;
		public PageType pageToLoadAfterEntry;

		PageType _nextPage; //the page this controller will allow the manager to load next

		bool _alive = true;
		bool _didDisable = false;
		Animator _animator;
		RectTransform _rect;

		public RectTransform Rect
		{
			get { return _rect; }
		}

		void Start()
		{
			Init();
		}

		void Update() {
			UpdatePage();
		}

		protected void Init()
		{
			_rect = GetComponent<RectTransform>();
			_animator = GetComponent<Animator>();
		}

		protected void UpdatePage()
		{
			if (!_animator)
			{
				return;
			}

			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
			{
				_alive = false;
			}
			if (_alive)
			{
				if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
				{
					if (!_didDisable)
					{
						_animator.enabled = false;
						_didDisable = true;

						if (pageToLoadAfterEntry != PageType.NONE)
						{
							PageManager.Instance.LoadPage(pageToLoadAfterEntry);
						}

						OnPageDidEnter();
					}
				}
			}
			else {
				if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
				{
					if (_nextPage != PageType.NONE)
					{
						PageManager.Instance.LoadPage(_nextPage);
					}
					OnPageDidExit();
					PageManager.Instance.DestroyPage(pageType);
				}
			}
		}

		/// <summary>
		/// Start this page's exit animation if one exists
		/// Called via Page Manager
		/// </summary>
		public void ExitView(PageType _nextPage)
		{
			this._nextPage = _nextPage;
			if (_animator)
			{
				_animator.enabled = true;
				_animator.SetBool("Exit", true);
			}
			else {
				if (_nextPage != PageType.NONE)
				{
					PageManager.Instance.LoadPage(_nextPage);
				}
				OnPageDidExit();
				PageManager.Instance.DestroyPage(pageType);
			}
		}

		public virtual void OnPageDidEnter() {}
		public virtual void OnPageDidExit() {}
	}

}
