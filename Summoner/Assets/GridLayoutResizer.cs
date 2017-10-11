using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutResizer : MonoBehaviour {
	
	void OnEnabled () {
		RectTransform parent = GetComponent<RectTransform> ();
		GridLayoutGroup grid = GetComponent<GridLayoutGroup> ();

		int columns = grid.constraintCount;

		Debug.Log (columns);

		Debug.Log (parent.rect.width);

		grid.cellSize = new Vector2 (parent.rect.width / columns, parent.rect.height / columns);
	}
}
