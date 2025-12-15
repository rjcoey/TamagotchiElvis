using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string gigCSVPath = "/Editor/CSVs/Gigs.csv";
    private static string requestCSVPath = "/Editor/CSVs/Requests.csv";

    [MenuItem("Utilities/Generate Gigs")]
    public static void GenerateGigs()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + gigCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');

            GigDataSO gigData = ScriptableObject.CreateInstance<GigDataSO>();
            gigData.Initialise(
                splitData[0],
                int.Parse(splitData[1]),
                int.Parse(splitData[2]),
                int.Parse(splitData[3]),
                int.Parse(splitData[4]),
                int.Parse(splitData[5]),
                splitData[6],
                splitData[7],
                splitData[8],
                int.Parse(splitData[9])
            );

            AssetDatabase.CreateAsset(gigData, $"Assets/Scriptable Objects/Gig Data/{gigData.GigLocation}.asset");
        }

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Utilities/Generate Requests")]
    public static void GenerateRequests()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + requestCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');

            RequestDataSO requestData = ScriptableObject.CreateInstance<RequestDataSO>();
            requestData.Initialise(
                splitData[0],
                splitData[1],
                splitData[2],
                splitData[3],
                splitData[4],
                float.Parse(splitData[5]),
                splitData[6],
                float.Parse(splitData[7])
            );

            AssetDatabase.CreateAsset(requestData, $"Assets/Scriptable Objects/Requests/{requestData.RequestTitle}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
