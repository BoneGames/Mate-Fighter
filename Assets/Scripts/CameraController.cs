using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Player[] players;
    Camera cam;
    public float camSize;
    public float camWidthMulti;
    public float camBuffer;

    public float camSizeMin;
    public float camSizeMax;
    void Start()
    {
        players = FindObjectsOfType<Player>();
        print("Found " + players.Length + " in scene");
        cam = GetComponent<Camera>();
        camSize = 5;

        camWidthMulti = GetCamWidthMulti(players);
    }

    float GetCamWidthMulti(Player[] players)
    {
        float separation = Vector3.Distance(players[0].transform.position, players[1].transform.position);
        camWidthMulti = (float)cam.orthographicSize / (float)separation;
        return camWidthMulti;
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length == 0)
            return;

        float xPos = 0;

        foreach (var item in players)
        {
            float playerX = item.transform.position.x;
            xPos += playerX;
        }

        float separation = Vector3.Distance(players[0].transform.position, players[1].transform.position);
        camSize = (separation * camWidthMulti) + camBuffer;

        camSize = Mathf.Clamp(camSize, camSizeMin, camSizeMax);

        float yPos = camSize - 5;

        transform.position = new Vector3(xPos / players.Length, yPos, -1);
        cam.orthographicSize = camSize;

    }
}
