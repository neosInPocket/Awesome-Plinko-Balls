using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRenderer : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private ParticleSystem glowEffect;
	[SerializeField] private GameObject collected;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public bool dead;
	
	
	public IEnumerator Pop()
	{
		glowEffect.gameObject.SetActive(false);
		spriteRenderer.color = new Color(1, 1, 1, 0);
		var effect = Instantiate(collected, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
