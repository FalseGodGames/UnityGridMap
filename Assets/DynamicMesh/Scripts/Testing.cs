/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    private void Start() {
        Debug.Log("Test");

        CreateBasicQuadMesh();
        //CreateTileMesh();
        //CreateAnimationMesh();
    }

    private void CreateBasicQuadMesh() {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(  0,   0);
        vertices[1] = new Vector3(  0, 100);
        vertices[2] = new Vector3(100, 100);
        vertices[3] = new Vector3(100,   0);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void CreateTileMesh() {
        Mesh mesh = new Mesh();

        int width = 4;
        int height = 4;
        float tileSize = 10;

        Vector3[] vertices = new Vector3[4 * (width * height)];
        Vector2[] uv = new Vector2[4 * (width * height)];
        int[] triangles = new int[6 * (width * height)];

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                int index = i * height + j;

                vertices[index * 4 + 0] = new Vector3(tileSize * i,         tileSize * j);
                vertices[index * 4 + 1] = new Vector3(tileSize * i,         tileSize * (j + 1));
                vertices[index * 4 + 2] = new Vector3(tileSize * (i + 1),   tileSize * (j + 1));
                vertices[index * 4 + 3] = new Vector3(tileSize * (i + 1),   tileSize * j);
                
                uv[index * 4 + 0] = new Vector2(0, 0);
                uv[index * 4 + 1] = new Vector2(0, 1);
                uv[index * 4 + 2] = new Vector2(1, 1);
                uv[index * 4 + 3] = new Vector2(1, 0);

                triangles[index * 6 + 0] = index * 4 + 0;
                triangles[index * 6 + 1] = index * 4 + 1;
                triangles[index * 6 + 2] = index * 4 + 2;

                triangles[index * 6 + 3] = index * 4 + 0;
                triangles[index * 6 + 4] = index * 4 + 2;
                triangles[index * 6 + 5] = index * 4 + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;

    }

    private void CreateAnimationMesh() {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4 * 2];
        Vector2[] uv = new Vector2[4 * 2];
        int[] triangles = new int[6 * 2];


        // Render Body
        float bodySize = 70f;
        Vector3 bodyPosition = new Vector3(0, -50);
        vertices[0] = bodyPosition + new Vector3( 0,  0);
        vertices[1] = bodyPosition + new Vector3( 0, bodySize);
        vertices[2] = bodyPosition + new Vector3(bodySize, bodySize);
        vertices[3] = bodyPosition + new Vector3(bodySize,  0);

        Vector2 bodyPixelsLowerLeft = new Vector2(0, 256);
        Vector2 bodyPixelsUpperRight = new Vector2(128, 384);

        float textureWidth = 512;
        float textureHeight = 512;

        uv[0] = new Vector2(bodyPixelsLowerLeft.x / textureWidth,  bodyPixelsLowerLeft.y / textureHeight);
        uv[1] = new Vector2(bodyPixelsLowerLeft.x / textureWidth,  bodyPixelsUpperRight.y / textureHeight);
        uv[2] = new Vector2(bodyPixelsUpperRight.x / textureWidth, bodyPixelsUpperRight.y / textureHeight);
        uv[3] = new Vector2(bodyPixelsUpperRight.x / textureWidth, bodyPixelsLowerLeft.y / textureHeight);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;



        // Render Head
        float headSize = 55f;
        Vector3 headPosition = new Vector3(6, -12);

        vertices[4] = headPosition + new Vector3( 0,  0);
        vertices[5] = headPosition + new Vector3( 0, headSize);
        vertices[6] = headPosition + new Vector3(headSize, headSize);
        vertices[7] = headPosition + new Vector3(headSize,  0);

        Vector2 headPixelsLowerLeft = new Vector2(0, 384);
        Vector2 headPixelsUpperRight = new Vector2(128, 512);

        uv[4] = new Vector2(headPixelsLowerLeft.x / textureWidth,  headPixelsLowerLeft.y / textureHeight);
        uv[5] = new Vector2(headPixelsLowerLeft.x / textureWidth,  headPixelsUpperRight.y / textureHeight);
        uv[6] = new Vector2(headPixelsUpperRight.x / textureWidth, headPixelsUpperRight.y / textureHeight);
        uv[7] = new Vector2(headPixelsUpperRight.x / textureWidth, headPixelsLowerLeft.y / textureHeight);

        triangles[6] = 4;
        triangles[7] = 5;
        triangles[8] = 6;

        triangles[ 9] = 4;
        triangles[10] = 6;
        triangles[11] = 7;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        float headPositionY = -12;
        bool headPositionMoveDown = true;
        CodeMonkey.Utils.FunctionUpdater.Create(() => {
            if (headPositionMoveDown) {
                headPositionY += -5f * Time.deltaTime;
                if (headPositionY < -13f) {
                    headPositionMoveDown = false;
                }
            } else {
                headPositionY += +5f * Time.deltaTime;
                if (headPositionY > -10f) {
                    headPositionMoveDown = true;
                }
            }

            headPosition = new Vector3(6, headPositionY);

            vertices[4] = headPosition + new Vector3( 0,  0);
            vertices[5] = headPosition + new Vector3( 0, headSize);
            vertices[6] = headPosition + new Vector3(headSize, headSize);
            vertices[7] = headPosition + new Vector3(headSize,  0);
            
            mesh.vertices = vertices;
        });

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
