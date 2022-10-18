using UnityEngine;

public class Monster : MonoBehaviour {

	public GameObject m_moveTarget;
	public float m_speed = 0.1f;
	public int m_maxHP = 30;
	const float m_reachDistance = 0.3f;
	public Transform m_spawnPoint;
	public int m_hp;

	void Start() {
		m_hp = m_maxHP;
		m_spawnPoint = FindObjectOfType<Spawner>().transform;
	}

	void FixedUpdate () {
		if (m_moveTarget == null)
			return;
		if(!gameObject.active)
			return;
		if (Vector3.Distance (transform.position, m_moveTarget.transform.position) <= m_reachDistance) {
			gameObject.SetActive(false);
			//gameObject.transform.position = m_spawnPoint.position;
			return;
		}

		var translation = m_moveTarget.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}

		if (gameObject.GetComponent<Monster>().m_hp <= 0)
		{
			gameObject.SetActive(false);
		}

		transform.Translate (translation);
	}
}
