using UnityEngine;

public class Spawner : MonoBehaviour {
	public float m_interval = 3;
	public GameObject m_moveTarget;
	public Monster m_monster;
	[SerializeField]
	private int m_poolCount = 10;
	private bool m_autoExpand = true;
	private PoolMono<Monster> m_pool;


	private float m_lastSpawn = -1;

	//создания пула монстров для оптимизации создания объектов(cм класс PoolMono)
	private void Start()
	{
		m_monster.m_moveTarget = m_moveTarget;
		m_pool = new PoolMono<Monster>(m_monster, m_poolCount, transform);
		m_pool.autoExpand = m_autoExpand;
	}

	void FixedUpdate () {
		Spawn();
	}

	private void Spawn()
	{
		if (Time.time > m_lastSpawn + m_interval) {
			var monster = m_pool.GetFreeElement();
			monster.transform.position = transform.position;
			monster.m_hp = monster.m_maxHP;
			m_lastSpawn = Time.time;
		}
	}
}
