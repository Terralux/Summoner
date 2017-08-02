using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Resource", menuName = "Trailblazer/Items/Resource", order=0)]
public class Resource : BaseItem {
	public ElementalAffinity myAffinity;

	public static new Resource CreateInstance() {
		Resource matComp = ScriptableObject.CreateInstance<Resource>();
		matComp.Init();
		return matComp;
	}

	private void Init (){
		myAffinity = new ElementalAffinity(Elementals.NONE, 0);
		isStackable = true;
	}
}

[System.Serializable]
public class KeyItem : BaseItem {
	public static new KeyItem CreateInstance() {
		KeyItem k = ScriptableObject.CreateInstance<KeyItem>();
		k.isStackable = false;
		return k;
	}
}

[System.Serializable]
public class BaseItem : ScriptableObject {
	public Sprite image;
	public string itemName;
	public string description;

	[HideInInspector]
	public bool isStackable;

	public static BaseItem CreateInstance() {
		BaseItem b = ScriptableObject.CreateInstance<BaseItem>();
		b.isStackable = false;
		return b;
	}
}