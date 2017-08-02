using System.Collections;

public struct ElementalAffinity {
	public Elementals targetElement;
	public int value;

	public ElementalAffinity(Elementals e, int value){
		targetElement = e;
		this.value = value;
	}
}