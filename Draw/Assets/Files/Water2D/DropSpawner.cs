using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject DropPrefab; // префаб, который нужно создавать
    public float spawnInterval = 0.001f; // интервал между созданиями префаба
    public int spawnCount = 10; // количество экземпляров, которые нужно создать
    private float spawnCooldown; // задержка между созданием экземпляров
    private float timer = 0f; // таймер для отслеживания интервала
    private int spawnCounter = 0; // количество уже созданных экземпляров

    [SerializeField] private GameObject dropPosition;

    // метод, который нужно вызвать для запуска создания экземпляров
    public void StartSpawning(float cooldown)
    {
        spawnCooldown = cooldown;
        InvokeRepeating("SpawnObject", 0f, spawnInterval); // запускаем повторяющийся вызов метода SpawnObject с интервалом spawnInterval
    }

    private void SpawnObject()
    {
        if (spawnCounter < spawnCount)
        {
            Instantiate(DropPrefab, dropPosition.transform.position, Quaternion.identity); // создаем новый экземпляр префаба в текущей позиции объекта
            spawnCounter++; // увеличиваем счетчик созданных экземпляров
        }
        else
        {
            CancelInvoke(); // отменяем повторяющийся вызов метода, если создали нужное количество экземпляров
        }
    }

}
