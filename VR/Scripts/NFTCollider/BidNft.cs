using LostThoughtStudios.DemterGift.DataManager;
using LostThoughtStudios.DemterGift.PhysicsTriggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class BidNft : MonoBehaviour
{
    [SerializeField]
    private GameObject BidPriceAmount;

    [SerializeField]
    private GameObject NFTBidPrice;

    [SerializeField]
    private GameObject NFTBidPriceTest;

    [SerializeField]
    private PrivateToken MyToken;

    [SerializeField]
    private DirectionTrigger directionTriggerObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (tag)
            {
                case "0": case "1" : case "2" : case "3" : case "4" : case "5" : case "6" : case "7" : case "8" : case "9":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text += tag;
                    break;
                case "Dot":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text += ".";
                    break;
                case "Erase":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text = BidPriceAmount.GetComponent<TextMeshProUGUI>().text.Remove(BidPriceAmount.GetComponent<TextMeshProUGUI>().text.Length - 1);
                    break;
                case "Execute":
                    ExecuteBidAction();
                    break;
                case "Refresh":
                    RefreshData();
                    break;
            }
        }
    }

    private async void ExecuteBidAction()
    {


        string PostURL = "https://heracoin-tron.onrender.com/api/BidNFT/" + MyToken.currentNFTID.ToString();

        NFTBidPriceTest.GetComponent<TextMeshProUGUI>().text = MyToken.currentNFTID.ToString() + PostURL;
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(PostURL),
            Content = new StringContent("{\t\n\t\"privatekey\":\""+MyToken.PrivateKey +"\",\n  \"BidPrice\":"+ 
                BidPriceAmount.GetComponent<TextMeshProUGUI>().text + "\n}")
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

            NFTBidPriceTest.GetComponent<TextMeshProUGUI>().text = body;

            NFTBidPrice.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    private void RefreshData()
    {
        NFTBidPrice.GetComponent<TextMeshProUGUI>().text = BidPriceAmount.GetComponent<TextMeshProUGUI>().text;

        //Can be improved in the future by requesting the data again from server
        DataSyncer.Instance.EventData[directionTriggerObject.index - 1].NftDonatedPerEvent[MyToken.currentNFTID].BidPrice = BidPriceAmount.GetComponent<TextMeshProUGUI>().text;
    }
}
