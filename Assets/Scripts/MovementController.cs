using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [Header("Character Options")]
    public float speed;
    public float AngularSpeed;
    public Vector3 velocity;
    public float zoom;
    public float sensitivity;
    public float maxZoom;
    public float minZoom;

    [Header("Tile Modifier Options")]
    public float iceSpeed;
    public GridLayout groundGridLayout;
    public Tilemap groundTilemap;

    public static Transform playerTransform;

    private float currentSpeed;
    private float horizontalSpeed;
    private float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        currentSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // rotation
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AngularSpeed * Time.deltaTime);

        // movement
        horizontalSpeed = Input.GetAxis("Horizontal");
        verticalSpeed = Input.GetAxis("Vertical");
        velocity = new Vector3(horizontalSpeed * currentSpeed * Time.deltaTime, verticalSpeed * currentSpeed * Time.deltaTime, 0f);        
        transform.position += velocity;

        // zoom in/out
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -10), Time.deltaTime * speed);
        zoom -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom, Time.deltaTime*currentSpeed);
        
        // ice tile boost
        Vector3Int cellPos = groundGridLayout.WorldToCell(transform.position);
        TileBase tile = groundTilemap.GetTile(cellPos);
        if (tile) {
            if (tile.name == "ice") {
                currentSpeed = Mathf.Lerp(currentSpeed, iceSpeed, 0.05f);
            } else {
                currentSpeed = speed;
            }
        } else {
            currentSpeed = speed;
        }
    }
}
