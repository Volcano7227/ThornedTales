using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System;

//Inspired From : https://www.youtube.com/watch?v=eK2SlZxNjiU
//To-Do 
/*
 * List Room (Randomize Layout)
 * Boss Room
  */
public class RoomManager : MonoBehaviour
{
    enum Direction { Up, Down, Left, Rigth }
    [SerializeField] GameObject defaultRoom;
    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] GameObject[] VariantRoomPrefabList;
    [SerializeField] int maxRooms = 10;
    [SerializeField] int minRooms = 6;
    [SerializeField] int gridSizeX = 20;
    [SerializeField] int gridSizeY = 20;
    [SerializeField] int roomWidth = 20;
    [SerializeField] int roomHeight = 12;

    GameObject Dungeon;

    System.Random random = new();
    List<GameObject> roomList = new();

    Queue<Vector2Int> roomQueue = new();

    // 0 : Vide , 1 : Room , 2 : BossRoom
    int[,] roomGrid;

    int roomCount;
    Vector2Int startingRoomIndex => new Vector2Int(gridSizeX / 2, gridSizeY / 2);
    Room LastRoom => Dungeon.transform.GetChild(Dungeon.transform.childCount - 1).GetComponent<Room>();

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

        int i = 0;
        while (!generationComplete)
        {
            if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
            {
                Vector2Int roomIndex = roomQueue.Dequeue();
                int gridX = roomIndex.x;
                int gridY = roomIndex.y;

                TryGenerateRoom(new Vector2Int(gridX - 1, gridY), i % 2 == 0 ? VariantRoomPrefabList[random.Next(VariantRoomPrefabList.Length)] : defaultRoom);
                TryGenerateRoom(new Vector2Int(gridX + 1, gridY), i % 4 == 0 ? VariantRoomPrefabList[random.Next(VariantRoomPrefabList.Length)] : defaultRoom);
                TryGenerateRoom(new Vector2Int(gridX, gridY - 1), i % 4 == 0 ? VariantRoomPrefabList[random.Next(VariantRoomPrefabList.Length)] : defaultRoom);
                TryGenerateRoom(new Vector2Int(gridX, gridY + 1), i % 2 == 0 ? VariantRoomPrefabList[random.Next(VariantRoomPrefabList.Length)] : defaultRoom);
                i++;
            }
            else if (roomCount < minRooms)
            {
                Debug.Log("Generation failed to meet Min - Retrying..");
                ResetGeneration();
            }
            else
            {
                GenerateBossRoom();
                generationComplete = true;
            }
            yield return null;
        }

        Debug.Log($"Generation completed - {roomCount} rooms generated");
    }

    Vector2Int GetGridIdFromDirection(Direction direction, Vector2Int posInit)
    {
        int gridX = posInit.x;
        int gridY = posInit.y;

        switch (direction)
        {
            case Direction.Up:
                return new Vector2Int(gridX, gridY + 1);
            case Direction.Down:
                return new Vector2Int(gridX, gridY - 1);
            case Direction.Left:
                return new Vector2Int(gridX - 1, gridY);
            case Direction.Rigth:
                return new Vector2Int(gridX + 1, gridY);
            default:
                throw new System.Exception("Wtf how You stupid");
        }
    }
    void GenerateBossRoom()
    {
        bool generated = false;
        Room currentRoom = LastRoom;
        int currentRoomListId = Dungeon.transform.childCount - 1;
        int directionI = 0;

        while (!generated)
        {
            generated = TryGenerateRoom(GetGridIdFromDirection((Direction)directionI, currentRoom.RoomIndex), bossRoomPrefab, false, false, true, false);

            directionI++;
            if (directionI == Enum.GetNames(typeof(Direction)).Length)
            {
                directionI = 0;
                currentRoomListId -= 1;
                Dungeon.transform.GetChild(currentRoomListId).TryGetComponent(out currentRoom);
            }
        }
    }
    public bool TryGetRoomAt(Vector2Int id, out Room room)
    {
        GameObject roomObject = roomList.Find(r => r.GetComponent<Room>().RoomIndex == id);

        if (roomObject != null)
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

        var initialRoom = Instantiate(defaultRoom, GetPositionFromGridIndex(startingRoomId), Quaternion.identity, Dungeon.transform);

        initialRoom.name = $"StartingRoom-{roomCount}";
        Room startingRoom = initialRoom.GetComponent<Room>();
        startingRoom.RoomIndex = startingRoomId;
        startingRoom.tag = "StartingRoom";
        startingRoom.ClearRoom();
        roomList.Add(initialRoom);
    }
    bool TryGenerateRoom(Vector2Int roomId, GameObject RoomTypePrefab = null, bool randomzied = true, bool sizeLimited = true, bool adjacentFiltered = true, bool adjacentToNoRoom = false)
    {
        if (RoomTypePrefab == null)
        {
            RoomTypePrefab = defaultRoom;
        }

        int x = roomId.x;
        int y = roomId.y;
        int adjacentNb = adjacentToNoRoom ? 0 : 1;
        if (roomCount >= maxRooms && sizeLimited)
            return false;

        if (UnityEngine.Random.value < 0.5f && roomId != Vector2Int.zero && randomzied)
            return false;

        if (roomGrid[x, y] != 0)
            return false;

        if (CountAdjacentRooms(roomId) > adjacentNb && adjacentFiltered)
            return false;

        roomQueue.Enqueue(roomId);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(RoomTypePrefab, GetPositionFromGridIndex(roomId), Quaternion.identity, Dungeon.transform);

        Room roomComponent = newRoom.GetComponent<Room>();
        int random = UnityEngine.Random.Range(1, 5);

        roomComponent.RoomIndex = roomId;
        roomComponent.Difficulty = random;
        roomComponent.RoomType = (RoomType)random-1;

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
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }
    int CountAdjacentRooms(Vector2Int roomId)
    {
        int x = roomId.x;
        int y = roomId.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) // left
            count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) // rigth
            count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) // bottom
            count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) // top
            count++;

        return count;

    }
    void PlaceDoors(GameObject room, int x, int y)
    {
        Room currentRoom = room.GetComponent<Room>();

        if (TryGetRoomAt(new Vector2Int(x + 1, y), out Room rigthRoom) && roomGrid[x + 1, y] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.right, rigthRoom.LeftDoor);
            rigthRoom.PlaceDoor(Vector2Int.left, currentRoom.RigthDoor);
        }
        if (TryGetRoomAt(new Vector2Int(x - 1, y), out Room leftRoom) && roomGrid[x - 1, y] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.left, leftRoom.RigthDoor);
            leftRoom.PlaceDoor(Vector2Int.right, currentRoom.LeftDoor);
        }
        if (TryGetRoomAt(new Vector2Int(x, y + 1), out Room TopRoom) && roomGrid[x, y + 1] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.up, TopRoom.BottomDoor);
            TopRoom.PlaceDoor(Vector2Int.down, currentRoom.TopDoor);
        }
        if (TryGetRoomAt(new Vector2Int(x, y - 1), out Room BottomRoom) && roomGrid[x, y - 1] != 0)
        {
            currentRoom.PlaceDoor(Vector2Int.down, BottomRoom.TopDoor);
            BottomRoom.PlaceDoor(Vector2Int.up, currentRoom.BottomDoor);
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
