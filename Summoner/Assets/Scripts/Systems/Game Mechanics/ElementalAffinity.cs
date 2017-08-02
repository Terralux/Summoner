using System.Collections;

[System.Serializable]
public class ElementalAffinity {
	public Elementals targetElement;
	public int value;

	public ElementalAffinity(Elementals e, int value){
		targetElement = e;
		this.value = value;
	}
}