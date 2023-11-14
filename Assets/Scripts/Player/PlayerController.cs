using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Routine routine;
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private GameObject criticalDamageEffect;
	[SerializeField] private RopeController ropeController;
	private int currentLifes;
	
	public void Initialize()
	{
		StopAllCoroutines();
		PlayerSaves.LoadSaves();
		currentLifes = PlayerSaves.lifes;
		movement.Initialize();
		
		spriteRenderer.color = new Color(1, 1, 1, 1);
		ropeController.InitializeRope();
	}
	
	public void GoPlay()
	{
		movement.Enabled = true;
		ropeController.Enabled = true;
	}
	
	public void Disable()
	{
		movement.Enabled = false;
		ropeController.Enabled = false;
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<CoinRenderer>(out CoinRenderer coin))
		{
			if (coin.dead) return;
			coin.dead = true;
			StartCoroutine(coin.Pop());
			routine.PlayerCoin();
		}
		
		if (collider.TryGetComponent<RotatingSpike>(out RotatingSpike spike))
		{
			TakeDamage();
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision2D)
	{
		if (collision2D.transform.parent.TryGetComponent<GameBorders>(out GameBorders GameBorders))
		{
			TakeDamage();
		}
	}
	
	
	private void TakeDamage()
	{
		currentLifes--;
		routine.PlayerDamage(currentLifes);
		
		if (currentLifes <= 0)
		{
			currentLifes = 0;
			movement.Enabled = false;
			ropeController.Enabled = false;
			StartCoroutine(TakeCriticalDamageRoutine());
		}
		else
		{
			StartCoroutine(TakePartialDamage());
		}
	}
	
	private IEnumerator TakeCriticalDamageRoutine()
	{
		spriteRenderer.color = new Color(1, 1, 1, 0);
		var effect = Instantiate(criticalDamageEffect, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(effect.gameObject);
	}
	
	private IEnumerator TakePartialDamage()
	{
		int pointer = 0;
		
		while (pointer < 10)
		{
			spriteRenderer.color = new Color(1, 1, 1, 0);
			yield return new WaitForSeconds(0.15f);
			spriteRenderer.color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds(0.15f);
			pointer++;
		}
		
	}
}
