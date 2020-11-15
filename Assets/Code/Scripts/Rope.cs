using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rope : MonoBehaviour
{

    public enum drawingStates { NotStarted, BeginDrawing, Drawing, Finished, GameFinished };
    public drawingStates DrawingState { get; set; }

    public LineRenderer LineRenderer { get; set; }
    [SerializeField] private Material material;
    [SerializeField] private float drawSpeed = 2f;

    private GameObject drawFrom;
    private LevelManager levelManager;

    private void Start()
    {
        DrawingState = drawingStates.NotStarted;
        var test = GameObject.Find("LevelManager");
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        LineRenderer = gameObject.AddComponent<LineRenderer>();
        LineRenderer.textureMode = LineTextureMode.Tile;
        LineRenderer.material = material;
        LineRenderer.startWidth = 0.2f;
        LineRenderer.endWidth = 0.2f;
        LineRenderer.sortingLayerName = "Default";

    }


    private void Update()
    {
        if (drawFrom != null)
        {
            if (CanDrawRope())
            {
                DrawingState = drawingStates.Drawing;
                StartCoroutine(LineDraw(drawFrom, gameObject));
            }
        }

    }

    /// <summary>
    /// Draw the line between points
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private IEnumerator LineDraw(GameObject from, GameObject to)
    {
        float time = (to.transform.position - from.transform.position).magnitude / drawSpeed;
        from.GetComponent<Rope>().LineRenderer.SetPosition(0, from.transform.position);

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            Vector2 newPosition = Vector2.Lerp(from.transform.position, to.transform.position, t / time);
            from.GetComponent<Rope>().LineRenderer.SetPosition(1, newPosition);
            yield return null;
        }

        DrawingState = drawingStates.Finished;

        if (levelManager.IsPointLast(gameObject))
        {
            levelManager.GetFirstPoint().GetComponent<Rope>().BeginDrawing(gameObject);
        }
    }

    public void BeginDrawing(GameObject drawFrom)
    {
        this.drawFrom = drawFrom;
        DrawingState = drawingStates.BeginDrawing;
    }

    /// <summary>
    /// Checks if current drawing state allows drawing and 
    /// if the previous point has finished drawing or
    /// if the previous point was the first point
    /// </summary>
    /// <returns></returns>
    public bool CanDrawRope()
    {
        return (DrawingState == drawingStates.BeginDrawing) && (drawFrom.GetComponent<Rope>().DrawingState == drawingStates.Finished || levelManager.GetFirstPoint() == drawFrom);
    }
}
