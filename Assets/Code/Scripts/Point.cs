using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    public int PointNumber { get; set; }

    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private GameObject number;
    [SerializeField] private float fadeSpeed = 1.0f;

    private bool fadeOut = false;
    private LevelManager levelManager;
    private GameObject numberInstance;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Create textMesh for numbers
        Vector2 position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.2f);
        numberInstance = Instantiate(number, position, Quaternion.identity);
        numberInstance.GetComponent<TextMesh>().text = PointNumber.ToString();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit =
                Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if ((hit.collider != null))
            {
                GameObject hitPoint = hit.collider.gameObject;
                if (hitPoint == gameObject && levelManager.CurrentPoint == PointNumber)
                {
                    spriteRenderer.sprite = selectedSprite;
                    if (levelManager.CurrentPoint > 1)
                    {
                        //Draw the line from the previous point
                        hitPoint.GetComponent<Rope>().BeginDrawing(levelManager.GetPoint(levelManager.CurrentPoint - 2));
                    }
                    levelManager.CurrentPoint++;
                    fadeOut = true;
                }
            }
        }

        if (fadeOut)
        {
            Color color = numberInstance.GetComponent<Renderer>().material.color;
            float fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            numberInstance.GetComponent<Renderer>().material.color = color;

            if (color.a <= 0)
            {
                fadeOut = false;
            }
        }
    }
    public void SetLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

}
