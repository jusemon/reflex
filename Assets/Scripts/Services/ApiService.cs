using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ApiService
{
    public static ApiService Instance { get; private set; }

    static ApiService()
    {
        Instance = new ApiService();
    }

    private ApiService() { }

    public IEnumerator GetDataFromAPI<TResponse>(
        string apiUrl,
        Dictionary<string, string> queryParams,
        System.Action<TResponse> callback
    )
    {
        UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}{GetQueryString(queryParams)}");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error while sending request: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("API Response: " + response);

            // Process the API response here
            TResponse data = JsonUtility.FromJson<TResponse>(response);
            callback?.Invoke(data);
        }
    }

    private static string GetQueryString(Dictionary<string, string> queryParams)
    {
        return queryParams != null && queryParams.Count > 0
            ? $"?{string.Join('&', queryParams.Select(param => $"{UnityWebRequest.EscapeURL(param.Key)}={UnityWebRequest.EscapeURL(param.Value)}"))}"
            : string.Empty;
    }

    public IEnumerator GetDataFromAPI<TResponse>(string apiUrl, System.Action<TResponse> callback)
    {
        return this.GetDataFromAPI<TResponse>(apiUrl, null, callback);
    }

    public IEnumerator PostDataToAPI<TRequest, TResponse>(
        string apiUrl,
        TRequest data,
        System.Action<TResponse> callback
    )
    {
        string jsonData = JsonUtility.ToJson(data);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error while sending request: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("API Response: " + response);

            // Process the API response here
            TResponse responseData = JsonUtility.FromJson<TResponse>(response);
            callback?.Invoke(responseData);
        }
    }

    public IEnumerator PutDataToAPI<TRequest, TResponse>(
        string apiUrl,
        TRequest data,
        System.Action<TResponse> callback
    )
    {
        string jsonData = JsonUtility.ToJson(data);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = UnityWebRequest.Put(apiUrl, jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error while sending request: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("API Response: " + response);

            // Process the API response here
            TResponse responseData = JsonUtility.FromJson<TResponse>(response);
            callback?.Invoke(responseData);
        }
    }
}
