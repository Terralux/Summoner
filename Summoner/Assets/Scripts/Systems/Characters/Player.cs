using System.Collections;

[System.Serializable]
public class Player {
	public CharacterStats stats;
	public Weapon equipped;

	public Player(){
		stats = new CharacterStats();
		equipped = Weapon.none;
	}
}