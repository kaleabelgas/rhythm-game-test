using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectLists
{
    up,
    down,
    left,
    right
}
/// <summary>
/// The GameManager class handles a Dictionary of Lists.
/// <remarks>
/// Make sure to access the Instance instead of the class.
/// </remarks>
/// </summary>
public class GameManager : MonoBehaviour
{
    private readonly Dictionary<ObjectLists, List<GameObject>> listOfObjects = new Dictionary<ObjectLists, List<GameObject>>();

    /// <summary>
    /// The instance of the GameManager.
    /// </summary>
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // Create an new List of type GameObject for each value in ObjectLists, adding it to the Dictionary with key ObjectList
        foreach (var objectList in (ObjectLists[])Enum.GetValues(typeof(ObjectLists)))
        {
            List<GameObject> gameobjects = new List<GameObject>();
            listOfObjects.Add(objectList, gameobjects);
        }
    }

    /// <summary>
    /// Adds <paramref name="gameObject"/> to <paramref name="list"/>.
    /// </summary>
    /// <param name="list">The ObjectList where the GameObject will be added.</param>
    /// <param name="gameObject">The GameObject to add to the List.</param>
    public void AddObjectToList(ObjectLists list, GameObject gameObject) => listOfObjects[list].Add(gameObject);


    /// <summary>
    /// Removes <paramref name="gameObject"/> from <paramref name="list"/>.
    /// </summary>
    /// <remarks
    /// <param name="list">The ObjectList where the GameObject will be removed from.</param>
    /// <param name="gameObject">The GameObject to remove from the List.</param>
    public void RemoveObjectFromList(ObjectLists list, GameObject gameObject) => listOfObjects[list].Remove(gameObject);


    /// <summary>
    /// Returns a list of GameObjects from the specified list.
    /// </summary>
    /// <param name="list">The ObjectList to get from.</param>
    /// <returns>List of GameObjects</returns>
    public List<GameObject> GetAllObjectsAsList(ObjectLists list) => listOfObjects[list];


    /// <summary>
    /// Checks if objects exist in the given list.
    /// </summary>
    /// <param name="list">The ObjectList to get from.</param>
    /// <returns>bool</returns>
    public bool CheckObjectsInList(ObjectLists list) => listOfObjects[list].Count > 0;


    /// <summary>
    /// Gets the closest object in <paramref name="list"/> to <paramref name="position"/>.
    /// </summary>
    /// <param name="list">The ObjectList to get from.</param>
    /// <param name="position">The position to compare from.</param>
    /// <param name="searchRadius">If given, the distance to search for an object. Defaults to float.MaxValue.</param>
    /// <returns>The closest GameObject.</returns>
    public GameObject GetClosestObject(ObjectLists list, Vector3 position, out float distance, float searchRadius = float.MaxValue)
    {
        if (!CheckObjectsInList(list))
        {
            distance = -1;
            Debug.LogError("No objects in list!");
            return null;
        }
        GameObject _targetTransform = null;

        float _distanceToObject = 0;

        float _shortestDistance = searchRadius;

        for (int i = 0; i < listOfObjects[list].Count; i++)
        {
            _distanceToObject = Vector2.Distance(listOfObjects[list][i].transform.position, position);

            if (_distanceToObject < _shortestDistance)
            {
                _shortestDistance = _distanceToObject;
                _targetTransform = listOfObjects[list][i];
            }
        }

        //Debug.DrawLine(position, _targetTransform == null ? Vector3.zero : _targetTransform.transform.position, Color.white, Mathf.Infinity);
        distance = _distanceToObject;
        return _targetTransform;
    }

}
