using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class InspectManager : Singleton<InspectManager>
{
    private readonly Dictionary<string, string> inspect = new Dictionary<string, string>();
    [SerializeField] private readonly string fileName = "inspect";

    private void Start()
    {
        InitJson();
    }

    public static string GetMessageContent(string key)
    {
        return Instance.inspect[key];
    }

    private void InitJson()
    {
        string jsonText = Resources.Load<TextAsset>(fileName).text;
        JObject jObject = JObject.Parse(jsonText);
        
        foreach (var obj in jObject)
        {
            if (obj.Key.StartsWith("__COMMENT")) continue;
            inspect.Add(obj.Key, obj.Value.ToString());
        }
    }
}