using UnityEngine;

public class OutOfScreenChecker : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject clearCanvas;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0 || viewportPos.x > 1 ||
            viewportPos.y < 0 || viewportPos.y > 1)
        {
            clearCanvas.SetActive(true);
            Time.timeScale = 0.0f;
            Debug.Log($"{gameObject.name} は画面外に出ました！");
        }
    }
}
