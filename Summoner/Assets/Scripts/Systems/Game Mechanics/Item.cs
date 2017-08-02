using UnityEngine;

[System.Serializable]
public class MaterialComponentItem : BaseItem {
	public ElementalAffinity myAffinity;

	public static new MaterialComponentItem CreateInstance() {
		MaterialComponentItem matComp = ScriptableObject.CreateInstance<MaterialComponentItem>();
		matComp.Init();
		return matComp;
	}

	private void Init (){
		myAffinity = new ElementalAffinity(Elementals.NONE, 0);
	}
}

[System.Serializable]
public class KeyItem : BaseItem {
	public static new KeyItem CreateInstance() {
		KeyItem k = ScriptableObject.CreateInstance<KeyItem>();
		return k;
	}
}

[System.Serializable]
public class BaseItem : ScriptableObject {
	public Sprite image;
	public string itemName;
	public string description;

	public static BaseItem CreateInstance() {
		BaseItem b = ScriptableObject.CreateInstance<BaseItem>();
		return b;
	}
}