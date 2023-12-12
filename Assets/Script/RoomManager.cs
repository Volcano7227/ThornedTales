using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

//Inspired From : https://www.youtube.com/watch?v=eK2SlZxNjiU
public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] int maxRooms = 10;
    [SerializeField] int minRooms = 6;
    [SerializeField]int gridSizeX = 20;
    [SerializeField]int gridSizeY = 20;
    [SerializeField] int roomWidth = 20;
    [SerializeField] int roomHeight = 12;

    GameObject Dungeon;

    List<GameObject> roomList = new();

    Queue<Vector2Int> roomQueue = new();

    int[,] roomGrid;
    int roomCount;
    Vector2Int startingRoomIndex => new Vector2Int(gridSizeX / 2, gridSizeY / 2);

    bool generationComplete = false;
    private void Awake()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new();
    }
    public void StartGeneration()
    {
        ResetGeneration();
        StartCoroutine(GenerateDungeon());
    }
    IEnumerator GenerateDungeon()
    {
        while (!generationComplete)
        {
            if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
            {
                Vector2Int roomIndex = roomQueue.Dequeue();
                int gridX = roomIndex.x;
                int gridY = roomIndex.y;

                TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
                TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
                TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
                TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            }
            else if (roomCount < minRooms)
            {
                Debug.Log("Generation failed to meet Min - Retrying..");
                ResetGeneration();
            }
            else
            {
                generationComplete = true;
            }
            yield return null;
        }
        
        Dungeon.transform.GetChild(Dungeon.transform.childCount - 1).TryGetComponent(out Room LastRoom);
        LastRoom.PaintWall(Color.red);
        Debug.Log($"Generation completed - {roomCount} rooms generated");
    }
    public bool TryGetRoomAt(Vector2Int id, out Room room)
    {
        GameObject roomObject = roomList.Find(r => r.GetComponent<Room>().RoomIndex == id);

        if(roomObject != null)
            return roomObject.TryGetComponent(out room);

        room = null;
        return false;
    }
    void StartRoomGenerationFromRoom(Vector2Int startingRoomId)
    {
        roomQueue.Enqueue(startingRoomId);
        int x = startingRoomId.x;
        int y = startingRoomId.y;

        roomGrid[x, y] = 1;
        roomCount++;

        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(startingRoomId), Quaternion.identity, Dungeon.transform);

        initialRoom.name = $"StartingRoom-{roomCount}";
        Room startingRoom = initialRoom.GetComponent<Room>();
        startingRoom.RoomIndex = startingRoomId;
        startingRoom.StartingDoor = true;
        startingRoom.PaintWall(Color.green);
        roomList.Add(initialRoom);
    }
    bool TryGenerateRoom(Vector2Int roomId)
    {
        int x = roomId.x;
        int y = roomId.y;

        if(roomCount >= maxRooms)
            return false;

        if (Random.value < 0.5f && roomId != Vector2Int.zero)
            return false;
        
        if (roomGrid[x, y] != 0)
            return false;

        if (CountAdjacentRooms(roomId) > 1)
            return false;



        roomQueue.Enqueue(roomId);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomId), Quaternion.identity, Dungeon.transform);
        newRoom.GetComponent<Room>().RoomIndex = roomId;
        newRoom.name = $"Room-{roomCount}";
        roomList.Add(newRoom);

        PlaceDoors(newRoom, x, y);

        return true;
    }
    void ResetGeneration()
    {
        if (Dungeon != null) Destroy(Dungeon);
        Dungeon = new GameObject("Dungeon");

        roomList.ForEach(Destroy);
        roomList.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        StartRoomGenerationFromRoom(startingRoomIndex);
    }
    Vector3 GetPositionFromGridIndex(Vector2Int gridId)
    {
        int gridX = gridId.x;
        int gridY = gridId.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2),roomHeight * (gridY - gridSizeY / 2));
    }
    int CountAdjacentRooms(Vector2Int roomId)
    {
        int x = roomId.x;
        int y = roomId.y;
        int count = 0; 

        if (x > 0 && roomGrid[x - 1,y] != 0) // left
            count++;
        if (x < gridSizeX -1 && roomGrid[x + 1, y] != 0) // rigth
            count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) // bottom
            count++;
        if (y < gridSizeY -1 && roomGrid[x, y + 1] != 0) // top
            count++;

        return count;

    }
    void PlaceDoors(GameObject room, int x, int y)
    {
        Room currentRoom = room.GetComponent<Room>();

        if (TryGetRoomAt(new Vector2Int(x + 1, y), out Room rigthRoom) && roomGrid[x + 1, y] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.right);
            rigthRoom.PlaceDoor(Vector2Int.left);
        }
        if (TryGetRoomAt(new Vector2Int(x - 1,y),out Room leftRoom) && roomGrid[x - 1 ,y] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.left);
            leftRoom.PlaceDoor(Vector2Int.right);
        }
        if (TryGetRoomAt(new Vector2Int(x, y + 1), out Room TopRoom) && roomGrid[x, y + 1] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.up);
            TopRoom.PlaceDoor(Vector2Int.down);
        }
        if (TryGetRoomAt(new Vector2Int(x, y - 1 ), out Room BottomRoom) && roomGrid[x, y - 1] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.down);
            BottomRoom.PlaceDoor(Vector2Int.up);
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,1,0.5f);

        //DrawGrid
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Gizmos.DrawWireCube(GetPositionFromGridIndex(new Vector2Int(x, y)), new Vector3(roomWidth, roomHeight));
            }
        }
    }*/

}
