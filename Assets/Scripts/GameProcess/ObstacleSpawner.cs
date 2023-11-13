using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	[SerializeField] private RotatingSpike spikePrefab;
	[SerializeField] private CoinRenderer coinPrefab;
	[SerializeField] private Transform player;
	[SerializeField] private float coinSpawnChance;
	[SerializeField] private float obstacleSpawnDistance;
	[SerializeField] private Transform lastSpike;
	private Transform lastObstacle;
	private Vector2 screenSize;
	
	public void Init()
	{
		Delete();
		screenSize = CustomExtensions.GetScreenWorldSize();
		lastObstacle = lastSpike;
	}
	
	private void Update()
	{
		if (player.position.y + 3 * obstacleSpawnDistance > lastObstacle.transform.position.y)
		{
			SpawnObject();
		}
	}
	
	private void SpawnObject()
	{
		var rndX = Random.Range(-screenSize.x + coinPrefab.SpriteRenderer.size.x / 2, screenSize.x - coinPrefab.SpriteRenderer.size.x / 2);
		var position = new Vector2(rndX, lastObstacle.position.y + obstacleSpawnDistance);
		
		if (Random.Range(0, 1f) < coinSpawnChance)
		{
			lastObstacle = Instantiate(coinPrefab, position, Quaternion.identity, transform).transform;
		}
		else
		{
			var spike = Instantiate(spikePrefab, position, Quaternion.identity, transform);
			spike.Init(lastObstacle.position.y, obstacleSpawnDistance);
			lastObstacle = spike.transform;
		}
	}
	
	public void Delete()
	{
		foreach (Transform obj in transform)
		{
			Destroy(obj.gameObject);
		}
	}
}
