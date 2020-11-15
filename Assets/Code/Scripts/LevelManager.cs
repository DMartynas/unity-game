using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public int CurrentPoint { get; set; } //which point should be pressed next

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	[SerializeField] private GameObject beatLevelCanvas;
	[SerializeField] private GameObject pointPrefab;
	private GameObject[] points;
	private int lastIndex;


	private void Start()
	{
		CurrentPoint = 1;
		string[] coordinates = FindObjectOfType<LevelData>().GetCoordinates();

		lastIndex = (coordinates.Length / 2) - 1;
		points = new GameObject[coordinates.Length];

		for (int i = 1; i <= coordinates.Length; i += 2)
		{
			//Create vector and convert position to be from 0 to 1
			Vector2 position = new Vector2(float.Parse(coordinates[i - 1])/1000, float.Parse(coordinates[i])/1000);

			//Make the position to be a screen point, scale it down and
			//take into account the aspect ratio
			position.x *=  Screen.width * 0.5f;
			position.y *=  -Screen.height * Camera.main.aspect * 0.5f;
			
			//Center the drawing
			position.x += Screen.width / 4;
			position.y += Screen.height;

			position = Camera.main.ScreenToWorldPoint(position);

			//Initialize point
			GameObject point = Instantiate(pointPrefab, position, Quaternion.identity);
			points.SetValue(point, i / 2);
			point.GetComponent<Point>().PointNumber = (i + 1) / 2;
			point.GetComponent<Point>().SetLevelManager(this);
		}
	}

	private void Update()
	{
		//Show menu if game is won
		if (points[0].GetComponent<Rope>().DrawingState == Rope.drawingStates.Finished && !beatLevelCanvas.activeSelf)
		{
			beatLevelCanvas.SetActive(true);
		}
	}

	public bool IsPointLast(GameObject point)
	{
		return points[lastIndex] == point;
	}

	public GameObject GetFirstPoint()
	{
		return points[0];
	}

	public GameObject GetPoint(int i)
	{
		return points[i];
	}
}
