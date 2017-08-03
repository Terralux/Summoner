using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Resource", menuName = "Trailblazer/Items/Resource", order=0)]
public class Resource : BaseItem {
	public ElementalAffinity myAffinity = new ElementalAffinity(Elementals.NONE, 0);
}