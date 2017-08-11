using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalManager : Singleton<GlobalManager>
{
	readonly static bool DEBUG_ENABLED = false;

	protected GlobalManager() {

	} 

	static bool applicationIsQuitting = false;
	Dictionary<System.Type, Component> registeredComponents = new Dictionary<System.Type, Component>();

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void OnApplicationQuit() {
		applicationIsQuitting = true;
	}

	/// <summary>
	/// Tries to find a component of the specified type.
	/// Returns the component if found, otherwise returns null.
	/// </summary>
	/// <returns>The component.</returns>
	/// <typeparam name="T">The Component type</typeparam>
	static public T FindRequiredComponent<T>() where T : Component {
		T component = FindComponent<T>();
		if(!applicationIsQuitting && component == null) {
			Debug.LogErrorFormat("<color=red>[Toolbox] Missing required Toolbox component: {0}</color>", typeof(T));
		}
		return component;
	}

	static public T FindComponent<T>() where T : Component {
		Component component;
		if(!applicationIsQuitting && Instance.registeredComponents.TryGetValue(typeof(T), out component)) {
			if(DEBUG_ENABLED) Debug.LogFormat("[Toolbox] Found registered Component: {0}", component);
			return (T)component;
		}
		return null;
	}

	/// <summary>
	/// Registers a Component at runtime, adding a reference to the Component in the Toolbox.
	/// </summary>
	/// <returns>The component.</returns>
	/// <typeparam name="T">The Component class</typeparam>
	static public T RegisterComponent<T>(Component component) where T : Component {
		if (Instance.registeredComponents.ContainsKey(typeof(T))) {
			if(DEBUG_ENABLED) Debug.LogWarningFormat("[Toolbox] Cannot register another component of type {0}, because another one has already been registered beforehand", typeof(T));
		} else {
			Instance.registeredComponents.Add(typeof(T), component);
			var unregisterComponent = AddAutomaticUnregisterComponent(component);
			unregisterComponent.typeToUnregister = typeof(T);
			if(DEBUG_ENABLED) Debug.LogFormat("[Toolbox] Registering Component: {0}", component);
		}
		return (T)component;
	}

	/// <summary>
	/// Unregisters the component of the given type, if able.
	/// Also removes the automatic unregister component from the registered component, if able.
	/// </summary>
	/// <returns><c>true</c>, if component was unregistered, <c>false</c> otherwise.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	static public bool UnregisterComponent<T>() where T : Component {
		return UnregisterComponent(typeof(T));
	}

	/// <summary>
	/// Unregisters the component of the given type, if able.
	/// Also removes the automatic unregister component from the registered component, if able.
	/// </summary>
	/// <returns><c>true</c>, if component was unregistered, <c>false</c> otherwise.</returns>
	/// <param name="type">Type.</param>
	static public bool UnregisterComponent(System.Type type) {
		Component component;
		if (Instance.registeredComponents.TryGetValue(type, out component)) {
			RemoveAutomaticUnregisterComponent(component);
			Instance.registeredComponents.Remove(type);
			if(DEBUG_ENABLED) Debug.LogFormat("[Toolbox] Unregistering Component of type: {0}", type);
			return true;
		} else {
			if(DEBUG_ENABLED) Debug.LogFormat("[Toolbox] Cannot unregister Component of type: {0}, because no Components of that type have been registered beforehand", type);
			return false;
		}
	}

	/// <summary>
	/// Adds a component that automatically calls Toolbox.UnregisterComponent() when its gameobject is destroyed
	/// </summary>
	/// <returns>The automatic unregister component.</returns>
	/// <param name="component">Component.</param>
	static ToolboxAutomaticUnregister AddAutomaticUnregisterComponent(Component component) {
		return component.gameObject.AddComponent<ToolboxAutomaticUnregister>();
	}

	/// <summary>
	/// Removes the automatic unregister component
	/// </summary>
	/// <param name="component">Component.</param>
	static void RemoveAutomaticUnregisterComponent(Component component) {
		var unregisterComponent = component.GetComponent<ToolboxAutomaticUnregister>();
		if (unregisterComponent != null) {
			Destroy(unregisterComponent);
		}
	}
}