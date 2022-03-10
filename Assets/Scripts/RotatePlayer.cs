using UnityEngine;

public class RotatePlayer : MonoBehaviour
{

    public float speed = 5f;

    void Start() {
        
    }


    void Update() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-85, Vector3.forward);
        transform.rotation = rotation;
    }
}
