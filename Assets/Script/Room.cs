using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//From / Inspired From : https://www.youtube.com/watch?v=eK2SlZxNjiU
public class Room : MonoBehaviour
{
    [SerializeField] Door topDoor;
    [SerializeField] Door bottomDoor;
    [SerializeField] Door leftDoor;
    [SerializeField] Door rigthDoor;
    [SerializeField] Camera roomCamera;
    public Vector2Int RoomIndex { get; set; }
    public Door TopDoor => topDoor;
    public Door BottomDoor => bottomDoor;
    public Door LeftDoor => leftDoor;
    public Door RigthDoor => rigthDoor;

    List<Renderer> WallsRenderer;
    private void Awake()
    {
        WallsRenderer = new();
        foreach (Transform child in transform.GetChild(0))
        {
            if (child.gameObject.layer == 6)
            {
                WallsRenderer.Add(child.GetComponent<Renderer>());
            }
        }
    }
    public void PlaceDoor(Vector2Int direction, Door fromDoor)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.gameObject.SetActive(true);
            topDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.gameObject.SetActive(true);
            bottomDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.gameObject.SetActive(true);
            leftDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.right)
        {
            rigthDoor.gameObject.SetActive(true);
            rigthDoor.ConnectTo(fromDoor);
        }
    }
    public void PaintWall(Color color) => WallsRenderer.ForEach(wall => wall.material.color = color);

    public void ActivateRoom() => roomCamera.gameObject.SetActive(true);
    public void DesactivateRoom() => roomCamera.gameObject.SetActive(false);
}
