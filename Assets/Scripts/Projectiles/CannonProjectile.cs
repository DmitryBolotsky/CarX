using UnityEngine;

public class CannonProjectile : MonoBehaviour {
	public float m_speed = 0.1f;
	public int m_damage = 10;
	
	//вместо уничтожения объектов делаем их неактивными
	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

		monster.m_hp -= m_damage;
		if (monster.m_hp <= 0) {
			monster.gameObject.SetActive(false);
			monster.transform.position = monster.m_spawnPoint.position;
		}
		gameObject.SetActive(false);
	}
}
