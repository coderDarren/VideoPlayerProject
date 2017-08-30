using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Menu {

	public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

		//--------------- PROTECTED MEMBERS ---------------
		protected Image _image;

		void Start()
		{
			InitButton();
		}

		protected void InitButton()
		{
			_image = GetComponent<Image>();
		}

		#region --------------- VIRTUAL MEMBERS ---------------


		/// Represents a click event, when the user releases input on a button
		/// If the user is not on the item, no click logic will execute
		public virtual void OnItemUp()
		{}

		public virtual void OnItemEnter()
		{}

		public virtual void OnItemExit()
		{}

		public virtual void OnItemDown()
		{}

		#endregion


		#region --------------- INTERFACE MEMBERS ---------------

		public void OnPointerUp(PointerEventData ped)
		{
			OnItemUp();
		}

		public void OnPointerEnter(PointerEventData ped)
		{
			OnItemEnter();
		}

		public void OnPointerExit(PointerEventData ped)
		{
			OnItemExit();
		}

		public void OnPointerDown(PointerEventData ped)
		{
			OnItemDown();
		}

		#endregion

	}
}
