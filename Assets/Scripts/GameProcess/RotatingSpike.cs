using UnityEngine;

public class RotatingSpike : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Vector2 spriteSizes = new Vector2(0.2f, 0.4f);
	[SerializeField] private Vector2 rotateSpeedSizes;
	private int rotationMultiplier;
	private float rotateSpeed;
	
	public void Init(float playerPosY, float obstacleSpawnDistance)
	{
		var screenSize = CustomExtensions.screenSize;
		
		var size = Random.Range(spriteSizes.x, spriteSizes.y);
		spriteRenderer.size = new Vector2(size, size);
		
		circleCollider2D.radius = size / 2;
		
		var rndX = Random.Range(-screenSize.x + spriteRenderer.size.x / 2, screenSize.x - spriteRenderer.size.x / 2);
		transform.position = new Vector2(rndX, playerPosY + obstacleSpawnDistance);
	}
	
	private void Start()
	{
		var rnd = Random.Range(0, 2);
		if (rnd > 0)
		{
			rotationMultiplier = 1;
		}
		else
		{
			rotationMultiplier = -1;
		}
		
		rotateSpeed = Random.Range(rotateSpeedSizes.x, rotateSpeedSizes.y); 
	}
	
	private void Update()
	{
		var angle = rotateSpeed * rotationMultiplier;
			
		var a11 = Mathf.Cos(angle);
		var a12 = -Mathf.Sin(angle);
		var a21 = Mathf.Sin(angle);
		var a22 = Mathf.Cos(angle);
			
		transform.right = new Vector2(a11 * transform.right.x + a12 * transform.right.y, a21 * transform.right.x + a22 * transform.right.y);
	}
}
