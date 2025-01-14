using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public string title;
    public List<string> objectives = new List<string>();
    public List<Vector2> objectiveLocations = new List<Vector2>();
    public List<double> objectiveRadious = new List<double>();

    public int locations = 1;
    public GameObject objectToSpawn;
    public GameObject enemyToSpawn;

    public int questXP, questCoins;

    public bool start = false;

    void Start() {}

    void Update()
    {
        if (objectives.Count == 0 || !start)
        {
            return;
        }

        if ("end" == objectives[0].ToLower())
        {
            XPManager.Instance.AddXP(questXP);

            objectives = new List<string>();
            objectiveLocations = new List<Vector2>();
            objectiveRadious = new List<double>();
        }

        else if (objectives[0].IndexOf("pick", System.StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (locations == 1)
            {
                Match match = Regex.Match(objectives[0], @"\d+");
                if (match.Success)
                {
                    locations = int.Parse(match.Value);
                }

                for (int i = 0; i < locations; i++)
                {
                    Instantiate(objectToSpawn, objectiveLocations[i], Quaternion.identity);
                }
            }

            if (GameObject.FindWithTag("Quest Boxes") == null)
            {
                for (int i = 0; i < locations; i++)
                {
                    objectiveLocations.RemoveAt(0);
                    objectiveRadious.RemoveAt(0);
                }

                objectives.RemoveAt(0);
                locations = 1;
            }
        }

        else if (objectives[0].IndexOf("kill", System.StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (locations == 1)
            {
                Match match = Regex.Match(objectives[0], @"\d+");
                if (match.Success)
                {
                    locations = int.Parse(match.Value);
                }

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    for (int i = 0; i < locations; i++)
                    {
                        GameObject spawnedObject = Instantiate(enemyToSpawn, objectiveLocations[i], Quaternion.identity);
                        spawnedObject.tag = "Quest Enemy";

                        EnemyAI enemyAI = spawnedObject.GetComponent<EnemyAI>();
                        if (enemyAI != null)
                        {
                            enemyAI.player = player;
                        }
                    }
                }
            }

            if (GameObject.FindWithTag("Quest Enemy") == null)
            {
                for (int i = 0; i < locations; i++)
                {
                    objectiveLocations.RemoveAt(0);
                    objectiveRadious.RemoveAt(0);
                }

                objectives.RemoveAt(0);
                locations = 1;
            }
        }

        else if (objectives[0].IndexOf("give", System.StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (locations == 1)
            {
                Match match = Regex.Match(objectives[0], @"\d+");
                if (match.Success)
                {
                    locations = int.Parse(match.Value);
                }

                for (int i = 0; i < locations; i++)
                {
                    Instantiate(objectToSpawn, objectiveLocations[i], Quaternion.identity);
                }
            }
            if (GameObject.FindWithTag("Quest NPC") == null)
            {
                for (int i = 0; i < locations; i++)
                {
                    objectiveLocations.RemoveAt(0);
                    objectiveRadious.RemoveAt(0);
                }

                objectives.RemoveAt(0);
                locations = 1;
            }
        }

        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                float distanceToTarget = Vector2.Distance(player.transform.position, objectiveLocations[0]);

                if (distanceToTarget < objectiveRadious[0])
                {
                    objectives.RemoveAt(0);
                    objectiveLocations.RemoveAt(0);
                    objectiveRadious.RemoveAt(0);
                }
            }
        }
    }
}
