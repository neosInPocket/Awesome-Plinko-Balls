using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private Transform startPosition;
	[SerializeField] private RopeController ropeController;
	[SerializeField] private AudioSource ballSource;

	[SerializeField] private TrailRenderer trailRenderer;
	private bool isGrounded;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}

	private bool isEnabled;

	private void Start()
	{
		PlayerSaves.LoadCurrentParameters();

		switch (PlayerSaves.gravityUpdate)
		{
			case 0:
				Physics2D.gravity = new Vector2(9.81f / 4, 0);
				break;

			case 1:
				Physics2D.gravity = new Vector2(9.81f / 3, 0);
				break;

			case 2:
				Physics2D.gravity = new Vector2(9.81f / 2, 0);
				break;

			case 3:
				Physics2D.gravity = new Vector2(9.81f, 0);
				break;
		}

		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		Initialize();
	}

	public void Initialize()
	{
		transform.position = startPosition.position;
		trailRenderer.Clear();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isEnabled) return;

		if (collision.gameObject.TryGetComponent<RopeController>(out RopeController rope))
		{
			isGrounded = true;

			if (!soundPlaying)
			{
				soundPlaying = true;
				StartCoroutine(BallSource());
			}
		}
	}

	public bool soundPlaying;

	private IEnumerator BallSource()
	{
		ballSource.Stop();
		ballSource.Play();

		yield return new WaitForSeconds(0.1f);
		soundPlaying = false;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!isGrounded) return;
		rigid.velocity = Vector2.up * speed;

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<RopeController>(out RopeController rope))
		{
			isGrounded = false;
		}
	}

	private void Enable()
	{
		rigid.constraints = RigidbodyConstraints2D.None;
		rigid.velocity = Vector2.up * speed;
	}

	private void Disable()
	{
		rigid.velocity = Vector2.zero;
		rigid.angularVelocity = 0;
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
	}
}

