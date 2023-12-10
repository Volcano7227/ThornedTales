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
    [SerializeField] Transform AnchorCam;

    public float timeForTransitionCam { get; private set; } = 1.5f;
    public Vector2Int RoomIndex { get; set; }
    public Door TopDoor => topDoor;
    public Door BottomDoor => bottomDoor;
    public Door LeftDoor => leftDoor;
    public Door RigthDoor => rigthDoor;

    Camera mainCamera;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        mainCamera = Camera.main;
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
    public void MoveCamToRoom() => StartCoroutine(MoveCam());
    IEnumerator MoveCam()
    {
        playerMovement.FreezeMovement();
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(mainCamera.transform.position, AnchorCam.position);
        float speed = journeyLength / timeForTransitionCam;

        while (mainCamera.transform.position != AnchorCam.position)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, AnchorCam.position, fractionOfJourney);
            yield return null;
        }
        playerMovement.UnFreezeMovement();
        Debug.Log("Arrived At Destination");
    }

}
