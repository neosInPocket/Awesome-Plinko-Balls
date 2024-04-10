using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Routine routine;
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private GameObject criticalDamageEffect;
	[SerializeField] private RopeController ropeController;
	private int currentLifes;

	public void InitializePlayer()
	{
		StopAllCoroutines();
		PlayerSaves.LoadCurrentParameters();
		currentLifes = PlayerSaves.lifesCounUpgrade;
		movement.Initialize();

		spriteRenderer.color = new Color(1, 1, 1, 1);
		ropeController.SetInitialRope();
	}

	public void StartPlayer()
	{
		movement.Enabled = true;
		ropeController.Enabled = true;
	}

	public void SetDisactiveState()
	{
		movement.Enabled = false;
		ropeController.Enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D objectCollider)
	{
		if (objectCollider.TryGetComponent<CoinRenderer>(out CoinRenderer coin))
		{
			if (coin.dead) return;
			coin.dead = true;
			StartCoroutine(coin.Pop());
			routine.CoinGained();
		}

		if (objectCollider.TryGetComponent<RotatingSpike>(out RotatingSpike spike))
		{
			Damaged();
		}
	}

	private void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.transform.parent.TryGetComponent<GameBorders>(out GameBorders GameBorders))
		{
			Damaged();
		}
	}


	private void Damaged()
	{
		currentLifes--;
		routine.DamagedPlayer(currentLifes);

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
