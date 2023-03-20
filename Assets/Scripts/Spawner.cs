using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnerPoints;

    public List<Transform> SpawnerPoints => _spawnerPoints;
}
