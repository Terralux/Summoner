using System.Collections;

[System.Serializable]
public class Player : Entity {
	public CharacterStats stats;
	private bool ValidateWeaponSlot{
		get{
			return equipped != null;
		}
		set{
			hasWeapon = value;
		}
	}
	private bool hasWeapon;

	public Weapon equipped;

	public Player(){
		stats = new CharacterStats();
		hasWeapon = equipped != null;
	}
}