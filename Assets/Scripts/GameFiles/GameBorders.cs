using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBorders : MonoBehaviour
{
	[SerializeField] SpriteRenderer leftBorder;
	[SerializeField] SpriteRenderer rightBorder;
	[SerializeField] private ParticleSystem zonePrefab;

	private void Start()
	{
		var screenSize = CustomExtensions.sizeOfTheScreen;

		leftBorder.size = new Vector2(leftBorder.size.x, screenSize.y * 2);
		leftBorder.transform.position = new Vector2(-screenSize.x - leftBorder.size.x / 2, Camera.main.transform.position.y);

		rightBorder.size = new Vector2(rightBorder.size.x, screenSize.y * 2);
		rightBorder.transform.position = new Vector2(screenSize.x + leftBorder.size.x / 2, Camera.main.transform.position.y);

		SetZoneActive();
	}

	private void SetZoneActive()
	{
		Vector2 size = CustomExtensions.sizeOfTheScreen;

		float yPos = -size.y + 0.5f + Camera.main.transform.position.y;

		var count = Mathf.Ceil(2 * size.y / 1);
		for (int i = 0; i < count; i++)
		{
			Instantiate(zonePrefab, new Vector2(-size.x, yPos), Quaternion.Euler(0, 90, -90), transform);
			yPos += 1;
		}

		yPos = -size.y + 0.5f + Camera.main.transform.position.y;
		for (int i = 0; i < count; i++)
		{
			Instantiate(zonePrefab, new Vector2(size.x, yPos), Quaternion.Euler(180, 90, -90), transform);
			yPos += 1;
		}
	}
}
