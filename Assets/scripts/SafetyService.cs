using UnityEngine;
using System.Collections.Generic;

public class SafetyService : MonoBehaviour
{
    public GameObject target;
    public GameObject objectSpace;
    // List of prefabs to instantiate
    public List<GameObject> prefabList;

    private List<ObjectData> objectDataList;

    public void SetObjectlist(List<ObjectData> objectDataList){
        DestroyAllObjects();
        this.objectDataList = objectDataList;
        Debug.Log("Received object: " + objectDataList.Count);
        // DestroyAllObjects();
        // foreach (var objData in objectDataList)
        // {
        //     SpawnOrUpdateObject(objData, target);
        //     Debug.Log($"Class: {objData.Class}, ID: {objData.Id}, Position: {objData.Position}, Scale: {objData.Scale}, Orientation: {objData.Orientation}, Time: {objData.Time}");
        // }
    }

    public void Start(){
        // Create a dummy list of ObjectData
       
        
    }



    public void Update(){
    //   refresh the object space every 5 seconds

        
        int time = (int)(Time.time % 1);
        if(time == 0){
            
            foreach (var objData in objectDataList)
            {
                SpawnOrUpdateObject(objData, target);
                Debug.Log($"Class: {objData.Class}, ID: {objData.Id}, Position: {objData.Position}, Scale: {objData.Scale}, Orientation: {objData.Orientation}, Time: {objData.Time}");
            }
        }
    }
    // A clean up method to destroy all objects in the object space
    public void DestroyAllObjects()
    {

        foreach (Transform child in objectSpace.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("All objects destroyed.");
    }

/// <summary>
/// Method to spawn or update an object relative to a target. If the target does not exist, it will be created.
/// </summary>
/// <param name="objectData">Data defining the object to spawn or update</param>
/// <param name="target">The target GameObject serving as the reference position (can be null)</param>
public void SpawnOrUpdateObject(ObjectData objectData, GameObject target)
{
    if (objectData == null)
    {
        Debug.LogError("Object data is null.");
        return;
    } 
    GameObject prefab = null;
    // Ensure a valid prefab exists in the list
    if (objectData.ClassID < 0 || objectData.ClassID >= prefabList.Count)
    {
        Debug.LogError("Prefab ID out of bounds.");
        prefab = prefabList[0];
    }else{
        prefab = prefabList[objectData.ClassID];
    }

    

    // Check if the object already exists
    string objectName = $"{objectData.Class}_{objectData.Id}";
    Transform existingObject = objectSpace.transform.Find(objectName);

    if (existingObject != null)
    {
        // Update the existing object's position, scale, and orientation
        Debug.Log($"Updating existing object '{objectName}' relative to target '{target.name}'.");
        existingObject.position = target.transform.position + objectData.Position;
        existingObject.localScale = objectData.Scale;
        existingObject.rotation = Quaternion.Euler(objectData.Orientation);
    }
    else
    {
        // Calculate the spawn position relative to the target
        Vector3 spawnPosition = target.transform.position + objectData.Position;
        Vector3 spawnScale = objectData.Scale;
        Quaternion spawnOrientation = Quaternion.Euler(objectData.Orientation);

        // Instantiate the prefab
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, spawnOrientation,objectSpace.transform);
        spawnedObject.transform.localScale = spawnScale;


        // Assign a meaningful name to the spawned object
        spawnedObject.name = objectName;

        Debug.Log($"Spawned object of class {objectData.Class} at {spawnPosition} relative to target '{target.name}'.");
    }
}

    /// <summary>
    /// Spawns the first object in the prefab list randomly within a rectangular area defined by a minimum and maximum distance from the target.
    /// </summary>
    /// <param name="target">The target GameObject serving as the reference position</param>
    /// <param name="minDistance">Minimum distance from the target (5 meters in this case)</param>
    /// <param name="maxDistance">Maximum distance from the target (e.g., 8 meters)</param>
    /// <param name="width">Width of the rectangular spawn area</param>
    /// <param name="height">Height (or depth) of the rectangular spawn area</param>
    public void SpawnRandomlyNearTarget(GameObject target)
    {
         float minDistance = 5f;
    float maxDistance = 15f;
    float width = 1.5f*30f;
    float height = 1.5f*8f;

    if (prefabList == null || prefabList.Count == 0)
    {
        Debug.LogError("Prefab list is empty or null.");
        return;
    }

    if (target == null)
    {
        Debug.LogError("Target is null.");
        return;
    }

    // Select the first prefab from the list
    GameObject prefab = prefabList[0];
    if (prefab == null)
    {
        Debug.LogError("The first prefab in the list is null.");
        return;
    }

    // Calculate the random spawn position within the rectangular bounds
    Vector3 randomDirection = new Vector3(
        Random.Range(-width / 2f, width / 2f),  // Random X position within the width
        0,                                      // Keep it on the horizontal plane
        Random.Range(-height / 2f, height / 2f) // Random Z position within the height
    );

    // Ensure the spawn position is at least minDistance away from the target
    float randomDistance = Random.Range(minDistance, maxDistance);

    // Scale the random direction and ensure it stays within the target's avoidance region
    Vector3 spawnPosition = target.transform.position + randomDirection.normalized * randomDistance;

    // Check if the calculated spawn position is within the target's avoidance zone and adjust
    float distanceToTarget = Vector3.Distance(target.transform.position, spawnPosition);

    // If too close to the target, adjust the position
    if (distanceToTarget < minDistance)
    {
        // Calculate a new random position that respects the minDistance constraint
        randomDistance = Random.Range(minDistance, maxDistance); // Recalculate distance
        spawnPosition = target.transform.position + randomDirection.normalized * randomDistance;
    }

    // Instantiate the prefab at the calculated position
    GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity,objectSpace.transform);

    Debug.Log($"Spawned object at {spawnPosition}");

    }


    /// <summary>
    /// Structure to define object data for spawning
    /// </summary>
    [System.Serializable]
    public class ObjectData
    {
        public string Class;           // Object class (e.g., "car")

        public int ClassID;           // Object class ID (e.g., 1 for car, 2 for truck, etc.)
        public int Id;                 // Prefab ID in the prefabList
        public Vector3 Position;       // Position relative to the target
        public Vector3 Scale;          // Scale of the object
        public Vector3 Orientation;    // Orientation (Euler angles)
        public float Time;             // Optional time property
    }
}