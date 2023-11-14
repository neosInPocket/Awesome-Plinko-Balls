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
		var screenSize = CustomExtensions.screenSize;
		
		leftBorder.size = new Vector2(leftBorder.size.x, screenSize.y * 2);
		leftBorder.transform.position = new Vector2(-screenSize.x - leftBorder.size.x / 2, 0);
		
		rightBorder.size = new Vector2(rightBorder.size.x, screenSize.y * 2);
		rightBorder.transform.position = new Vector2(screenSize.x + leftBorder.size.x / 2, 0);
		
		InitializeZone(screenSize);
	}
	
	private void InitializeZone(Vector2 screenSize)
	{
		float yPos = -screenSize.y + 0.5f;
		
		var zonesCount = Mathf.Ceil(2 * screenSize.y / 1);
		for (int i = 0; i < zonesCount; i++)
		{
			Instantiate(zonePrefab, new Vector2(-screenSize.x, yPos), Quaternion.Euler(0, 90, -90), transform);
			yPos += 1;
		} 
		
		yPos = -screenSize.y + 0.5f;
		for (int i = 0; i < zonesCount; i++)
		{
			Instantiate(zonePrefab, new Vector2(screenSize.x, yPos), Quaternion.Euler(180, 90, -90), transform);
			yPos += 1;
		} 
	}
}
