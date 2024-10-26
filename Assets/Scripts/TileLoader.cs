using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the size of the tilemap for the planet, defining
 * the width and height and drawing an outline grid in the Scene view.
 */
public class TileLoader : MonoBehaviour
{
    public float tilemapHeight = 20f;  // Set the height of the tilemap
    public float tilemapWidth = 30f;   // Set the width of the tilemap
    private float gridSize = 1f;       // Size of each grid cell for drawing purposes

    // Draws a grid outline in the Scene view using Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;  // Set the color of the grid

        // Draw the outer boundary of the tilemap
        Vector3 bottomLeft = transform.position - new Vector3(tilemapWidth / 2, tilemapHeight / 2, 0);
        Vector3 bottomRight = transform.position + new Vector3(tilemapWidth / 2, -tilemapHeight / 2, 0);
        Vector3 topLeft = transform.position + new Vector3(-tilemapWidth / 2, tilemapHeight / 2, 0);
        Vector3 topRight = transform.position + new Vector3(tilemapWidth / 2, tilemapHeight / 2, 0);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);

        // Draw internal grid lines
        for (float x = -tilemapWidth / 2; x <= tilemapWidth / 2; x += gridSize)
        {
            Vector3 start = transform.position + new Vector3(x, -tilemapHeight / 2, 0);
            Vector3 end = transform.position + new Vector3(x, tilemapHeight / 2, 0);
            Gizmos.DrawLine(start, end);
        }

        for (float y = -tilemapHeight / 2; y <= tilemapHeight / 2; y += gridSize)
        {
            Vector3 start = transform.position + new Vector3(-tilemapWidth / 2, y, 0);
            Vector3 end = transform.position + new Vector3(tilemapWidth / 2, y, 0);
            Gizmos.DrawLine(start, end);
        }
    }
}

