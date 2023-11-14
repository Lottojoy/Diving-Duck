using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        int randomIndex = Random.Range(0, prefabs.Length); // สุ่มเลือก index ของ prefab
        GameObject selectedPrefab = prefabs[randomIndex];  // เลือก prefab จาก array ตาม index ที่สุ่มได้
        GameObject pipes = Instantiate(selectedPrefab, transform.position, transform.rotation);
        pipes.transform.position += Vector3.left * Random.Range(minHeight, maxHeight);


        Destroy(pipes, 10f);
    }

}
