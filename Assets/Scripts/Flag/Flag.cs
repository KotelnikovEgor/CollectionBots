using UnityEngine;

public class Flag : MonoBehaviour
{
    public bool IsPlaced => gameObject.activeSelf;

    public void Place(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
