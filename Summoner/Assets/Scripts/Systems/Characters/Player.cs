using System.Collections;

[System.Serializable]
public class Player : Entity {
	public CharacterStats stats;
	public bool hasWeapon;

	public Weapon equipped;

	public Player(){
		stats = new CharacterStats();
		hasWeapon = equipped != null;
	}
}