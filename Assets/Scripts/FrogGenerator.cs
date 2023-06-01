using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class FrogGenerator : MonoBehaviour
{
   [SerializeField] private List<GameObject> _spawnPoints;
   [SerializeField] private GameObject _frogPrefab;
   [SerializeField] private int _maxSpawnTime;

   private Quaternion _transformRotation;
   private void Start()
   {
      StartCoroutine(SpawnFrog());
   }
   void Spawn()
   {
      _transformRotation = _frogPrefab.transform.rotation;
      _transformRotation.y += 180f;
      _frogPrefab.transform.rotation = _transformRotation;

      GameObject randSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            
      Instantiate(_frogPrefab, randSpawnPoint.transform.position, _transformRotation);
   }
   IEnumerator SpawnFrog()
   {
      int randDeltaTime = Random.Range(2, _maxSpawnTime);

      yield return new WaitForSeconds(randDeltaTime);
      Spawn();
      StartCoroutine(SpawnFrog());
   }
}
