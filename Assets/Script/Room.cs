using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//From / Inspired From : https://www.youtube.com/watch?v=eK2SlZxNjiU
public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rigthDoor;

    public bool StartingDoor = false;
    public bool EndingDoor = false;
    public Vector2Int RoomIndex { get; set; }
    List<Renderer> WallsRenderer;
    private void Awake()
    {
        WallsRenderer = new();
        foreach (Transform child in transform.GetChild(0))
        {
            if(child.gameObject.layer == 6)
            {
                WallsRenderer.Add(child.GetComponent<Renderer>());
            }
        }
    }
    public void PlaceDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up) 
            topDoor.SetActive(true);

        if (direction == Vector2Int.down)
            bottomDoor.SetActive(true);

        if (direction == Vector2Int.left)
            leftDoor.SetActive(true);

        if (direction == Vector2Int.right)
            rigthDoor.SetActive(true);
    }
    public void PaintWall(Color color) => WallsRenderer.ForEach(wall => wall.material.color = color);
    
}
