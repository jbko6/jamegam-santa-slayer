using System;
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

    [Header("Camera Options")]
    public float zoom;
    public float sensitivity;
    public float maxZoom;
    public float minZoom;
    public Vector2 cameraLowerClamp;
    public Vector2 cameraUpperClamp;

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
        // float screenWidth = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0)).x;
        // float screenHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Camera.main.pixelHeight)).y;
        // float clampedCamX = Mathf.Clamp(transform.position.x, cameraLowerClamp.x - (zoom*Camera.main.aspect), cameraUpperClamp.x - (zoom*Camera.main.aspect));
        // float clampedCamY = Mathf.Clamp(transform.position.y, cameraLowerClamp.y - (zoom), cameraUpperClamp.y - (zoom));
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -10), Time.deltaTime * speed);
        Debug.Log(Screen.width);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D other = col.collider;
        if (other.CompareTag("Elf"))
        {
            Health.health--;
            // remove elf without adding to score
            ElfSpawner.elves.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
