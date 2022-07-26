using LostThoughtStudios.DemterGift.DataManager;
using LostThoughtStudios.DemterGift.PhysicsTriggers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NFT_Button_1 : MonoBehaviour
{
    [SerializeField]
    private GameObject NFTImage;

    [SerializeField]
    private GameObject NFTName;

    [SerializeField]
    private GameObject NFTBidPrice;

    [SerializeField]
    private DirectionTrigger directionTriggerObject;

    [SerializeField]
    private PrivateToken MyToken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private async void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Animator>().enabled = true;

            MyToken.currentNFTID = int.Parse(tag);

            NFTName.GetComponent<TextMeshProUGUI>().text = DataSyncer.Instance.EventData[directionTriggerObject.index - 1].NftDonatedPerEvent[int.Parse(tag)].NftTitle;

            NFTBidPrice.GetComponent<TextMeshProUGUI>().text = DataSyncer.Instance.EventData[directionTriggerObject.index - 1].NftDonatedPerEvent[int.Parse(tag)].BidPrice;

            NFTImage.GetComponent<UnityEngine.UI.Image>().sprite = await DirectionTrigger.GetIconTexture(DataSyncer.Instance.EventData[directionTriggerObject.index - 1].NftDonatedPerEvent[int.Parse(tag)].NftImageUrl);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }
}
