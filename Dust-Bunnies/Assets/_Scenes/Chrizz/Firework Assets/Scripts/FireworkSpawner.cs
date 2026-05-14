using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ColorBank {
    [ColorUsage(true, true)] public Color HeadColor;
    [ColorUsage(true, true)] public Color TailColor;
}

public class FireworkSpawner : MonoBehaviour {
    [Header("Spawn Settings")]
    [SerializeField] private Firework fireworkPrefab;
    [SerializeField] private float tiltAmount = 10f;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float spawnRate = 2f;

    [Header("Audio")]
    [SerializeField] private AK.Wwise.Event fireworkEvent;
    [SerializeField] private AK.Wwise.Event finaleFireworkEvent;
    
    [Header("Color Bank")]
    [SerializeField] private List<ColorBank> colorBank;
    
    [Header("Finale")]
    [SerializeField] private Firework finale;
    [SerializeField] private bool debug = true;
    
    private Coroutine _spawnRoutine;
    private bool _inFinale = false;

    private void OnEnable() {
        _spawnRoutine = StartCoroutine(SpawnLoop());
    }
    private void OnDisable() {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
    }

    public void PlayFinale() {
        _inFinale = true;
        SpawnFirework(finale, true);
    }
    
    //Calls automatically on loop. Stops during the finale.
    private IEnumerator SpawnLoop() {
        //while (!_inFinale) {
        while (true) {
            SpawnFirework(fireworkPrefab, false);

            float delay = 1f / spawnRate;
            yield return new WaitForSeconds(delay);
        }
        //}
    }

    private void SpawnFirework(Firework firework, bool isFinale) {
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = transform.position + new Vector3(circle.x, 0f, circle.y);

        Quaternion rotation = Quaternion.Euler(
            Random.Range(-tiltAmount, tiltAmount),
            Random.Range(0f, 360f),
            0f
        );
        
        Firework fw = Instantiate(firework, spawnPos, rotation, transform);

        AK.Wwise.Event eventToPost = isFinale ? finaleFireworkEvent : fireworkEvent;
        eventToPost?.Post(fw.gameObject);

        int index = Random.Range(0, colorBank.Count);
        fw.Activate(colorBank[index].HeadColor, colorBank[index].TailColor);
    }
}
