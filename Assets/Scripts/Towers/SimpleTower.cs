using UnityEngine;

public class SimpleTower : Tower {
	
	[SerializeField]
	private int m_poolCount = 4;
	private bool m_autoExpand = true;
	private PoolMono<GuidedProjectile> m_pool;
	[SerializeField]
	private GuidedProjectile m_projectilePrefab;

	
	//создания пула снарядов для оптимизации создания объектов(cм класс PoolMono)
	private void Start()
	{
        
		m_pool = new PoolMono<GuidedProjectile>(m_projectilePrefab, m_poolCount, transform);
		m_pool.autoExpand = m_autoExpand;
        
	}
	void FixedUpdate () {
		Aim();
	}

	//поиск монстра и проверка кд пушки для оптимизации поиск ведем по коллайдеру(см. класс Tower)
	private void Aim()
	{
		if (m_projectilePrefab == null)
			return;
		if (IsAcquireTarget() || IsTargetTracked())
		{
			if (m_lastShotTime + m_shootInterval <= Time.time)
			{
				Shoot(monster);
				m_lastShotTime = Time.time;
			}
		}
	}

	//выстрел пушки (вместо создания объекта просто делаем объект из пула активным
	private void Shoot(Monster monster)
	{
		var projectile = m_pool.GetFreeElement();
		projectile.transform.position = transform.position;
		var projectileBeh = projectile.GetComponent<GuidedProjectile> ();
		projectileBeh.m_target = monster.gameObject;

		m_lastShotTime = Time.time;
	}
}
