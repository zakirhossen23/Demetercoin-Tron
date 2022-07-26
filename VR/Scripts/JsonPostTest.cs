using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class JsonPostTest : MonoBehaviour
{
    // Start is called before the first frame update
    string output;
    async void Start()
    {
        PostData p = new PostData(
            privateKey: "8470f20322e3e7b081ddb60179eb483e14c07022986d1b0c1c5da61ab729add4",
            bidAmount: 0.15
        );

        output = JsonConvert.SerializeObject(p);

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://heracoin-tron.onrender.com/api/BidNFT/0"),
            Content = new StringContent("{\t\n\t\"privatekey\":\"a9cba477f6702e7b175af9914501919d3a3e5a751f08ac3f9b752e56e78d0988\",\n  \"BidPrice\":0.30\n}")
            {
                Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
            }
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator PodaDei()
    {
        string testurl = "https://heracoin-infinity.onrender.com/api/BidNFT/3";

        using UnityWebRequest webrq = new UnityWebRequest(testurl, "POST");


        byte[] rawdata = Encoding.UTF8.GetBytes(output);

        Debug.Log(rawdata.Length);

        webrq.uploadHandler = new UploadHandlerRaw(rawdata);

        webrq.downloadHandler = new DownloadHandlerBuffer();

        webrq.SetRequestHeader("Content-Type", "application/json");

        yield return webrq.SendWebRequest();

        switch (webrq.result)
        {
            case UnityWebRequest.Result.Success:
                string response = webrq.downloadHandler.text;
                Debug.Log(response);
                break;
        }

        yield break;
    }
}
public class PostData
{
    public string privateKey;

    public double bidAmount;

    public PostData(string privateKey, double bidAmount)
    {
        this.privateKey = privateKey;
        this.bidAmount = bidAmount;
    }
}