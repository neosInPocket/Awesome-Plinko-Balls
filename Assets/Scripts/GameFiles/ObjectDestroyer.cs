using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;

	private void Start()
	{
		var screenSize = CustomExtensions.sizeOfTheScreen;
		spriteRenderer.size = new Vector2(screenSize.x * 2, spriteRenderer.size.y);
		transform.localPosition = new Vector2(0, -screenSize.y);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<RotatingSpike>(out RotatingSpike spike))
		{
			Destroy(spike.gameObject);
		}

		if (collider.TryGetComponent<CoinRenderer>(out CoinRenderer coin))
		{
			Destroy(coin.gameObject);
		}
	}
}
