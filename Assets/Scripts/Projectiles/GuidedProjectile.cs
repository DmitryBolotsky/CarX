using UnityEngine;

public class GuidedProjectile : MonoBehaviour {
	public GameObject m_target;
	public float m_speed = 0.2f;
	public int m_damage = 10;

	void FixedUpdate () {
		ProjectileUpdate();
	}

	private void ProjectileUpdate()
	{
		if (!m_target.gameObject.active) {
			gameObject.SetActive(false);
			return;
		}

		var translation = m_target.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}

	//вместо уничтожения объектов делаем их неактивными
	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		

		monster.m_hp -= m_damage;
		gameObject.SetActive(false);
	}
}
