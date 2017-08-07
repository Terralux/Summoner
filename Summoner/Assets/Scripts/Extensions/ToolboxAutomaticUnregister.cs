using UnityEngine;
using System.Collections;

public class ToolboxAutomaticUnregister : MonoBehaviour
{
	public System.Type typeToUnregister;

	void OnDestroy() {
		var instance = GlobalManager.Instance;
		if (instance) {
			GlobalManager.UnregisterComponent(typeToUnregister);
		}
	}
}