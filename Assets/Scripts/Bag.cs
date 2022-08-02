using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{

    public List<Item> inventory = new List<Item>();
    public List<Image> inventoryImage = new List<Image>();
    public Dictionary<int, Image> itemImageDictionary = new Dictionary<int, Image>();
    public Transform slots;

    // Start is called before the first frame update
    void Start()
    {
        itemImageDictionary.Add(1, Resources.Load<Image>("Item 1"));
        itemImageDictionary.Add(2, Resources.Load<Image>("Item 2"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PutInBag(Item item)
    {
        if (inventory.Count < 3)
        {
            inventory.Add(item);
            var image = SetBagImage(item.itemCode);
            inventoryImage.Add(image);

            item.PutDown();
            item.gameObject.SetActive(false); //어디에 멈춰있는가? 위치도 옮겨야 하나?
        }
        else
        {
            GameManager.manager.SetMessage("가방이 가득 찼습니다");
        }
    }

    public Image SetBagImage(int itemCode)
    {
        var instance = Instantiate(itemImageDictionary[itemCode], slots);
        instance.name = "item in bag";
        return instance;

    }

    public Item GetOutOfBag()
    {

        if(inventory.Count > 0)
        {
            var lastIndex = inventory.Count - 1;
            Debug.Log("호출" + inventory.Count);
            
            var item = inventory[lastIndex];
            //var itemCode = item.itemCode;
            item.gameObject.SetActive(false);
            inventory.RemoveAt(lastIndex);
            var image = inventoryImage[inventoryImage.Count - 1];
            Destroy(image.gameObject);
            inventoryImage.RemoveAt(lastIndex);



            //아이템을 다시 돌려줘야 한다.
            return item;
        }

        return null;
        
        //이미지 없애기 - 연결을 어떻게 시키지?
        //정보 지우기
    }
}
