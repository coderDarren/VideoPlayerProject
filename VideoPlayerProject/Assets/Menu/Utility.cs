using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;

namespace Util {

	/// All helper functions here

	public class Utility {


		#region --------------- USER INTERFACE ---------------

		public static void SetRectHeight(ref RectTransform _rect, float _height)
		{
			Vector2 _sizeDelta = _rect.sizeDelta;
			_sizeDelta.y = _height;
			_rect.sizeDelta = _sizeDelta;
		}

		public static void SetRectWidth(ref RectTransform _rect, float _width)
		{
			Vector2 _sizeDelta = _rect.sizeDelta;
			_sizeDelta.x = _width;
			_rect.sizeDelta = _sizeDelta;
		}

		public static void SetRectSize(ref RectTransform _rect, Vector2 _size)
		{
			_rect.sizeDelta = _size;
		}

		/// <summary>
		/// Spawns, positions and resizes a scroll view item based on a grid defined by _dim and _padding
		/// </summary>
		public static void InitScrollViewItemRect(ref RectTransform _rect, RectTransform _containerRect, Vector2 _dim, Vector2 _size, float _padding, int i)
		{
			int _cols = (int)_dim.y;
			//int _rows = (int)_dim.x;
			int _currentRow = i / _cols;
			int _currentCol = i -  _currentRow * _cols;
			//float _totalPadding = (_cols + 1) * (_padding / 2);

			_rect.SetParent(_containerRect);
			_rect.localScale = Vector3.one;
			_rect.offsetMin = Vector2.one;
			_rect.offsetMax = Vector2.one;

			//Vector2 _size;
			//_size.x = (_containerRect.rect.size.x / _cols) - _totalPadding;
			//_size.y = _size.x;

			SetRectWidth(ref _rect, _size.x);
			SetRectHeight(ref _rect, _size.y);
			SetRectWidth(ref _containerRect, _size.x * _cols + (_cols + 1) * _padding);
			SetRectHeight(ref _containerRect, _size.y * (_currentRow + 1) + (_currentRow + 2) * _padding);

			Vector2 _pos;
			_pos.x = _padding + (_size.x * _currentCol) + (_currentCol * _padding);
			_pos.y = _padding + (_size.y * _currentRow) + (_currentRow * _padding);
			_pos.y *= -1;
			
			_rect.localPosition = _pos;
		}

		#endregion

		#region --------------- COLOR ---------------

		public static Gradient LerpGradient(Gradient curr, Gradient target, float speed)
		{
			if (curr.colorKeys.Length != target.colorKeys.Length ||
				curr.alphaKeys.Length != target.alphaKeys.Length)
			{
				//Debugger.LogWarning("Unable to lerp gradients. Make sure 'curr' and 'target' have the same number of alpha and color keys.");
				return curr;
			}

			GradientColorKey[] currColorKeys = curr.colorKeys;
			//GradientAlphaKey[] currAlphaKeys = curr.alphaKeys;
			GradientColorKey[] targetColorKeys = target.colorKeys;
			//GradientAlphaKey[] targetAlphaKeys = target.alphaKeys;
			Gradient ret = new Gradient();

			for (int i = 0; i < currColorKeys.Length; i++)
			{
				currColorKeys[i].color = Color.Lerp(currColorKeys[i].color, targetColorKeys[i].color, speed);
			}

			ret.SetKeys(currColorKeys, curr.alphaKeys);
			return ret;
		}

		
		#endregion

		#region --------------- EQUALITY CHECKS ---------------

		public static bool ColorsMatch(Color _c1, Color _c2)
		{
			if (Mathf.Abs(_c1.r - _c2.r) < 0.005f &&
				Mathf.Abs(_c1.g - _c2.g) < 0.005f &&
				Mathf.Abs(_c1.b - _c2.b) < 0.005f &&
				Mathf.Abs(_c1.a - _c2.a) < 0.005f)
				return true;
			return false;
		}

		public static bool VectorsMatch(Vector3 _v1, Vector3 _v2)
		{
			if (Mathf.Abs(_v1.x - _v2.x) < 0.005f &&
				Mathf.Abs(_v1.y - _v2.y) < 0.005f &&
				Mathf.Abs(_v1.z - _v2.z) < 0.005f)
				return true;
			return false;
		}

		#endregion

	}
}