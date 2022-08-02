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
            item.gameObject.SetActive(false); //��� �����ִ°�? ��ġ�� �Űܾ� �ϳ�?
        }
        else
        {
            GameManager.manager.SetMessage("������ ���� á���ϴ�");
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
            Debug.Log("ȣ��" + inventory.Count);
            
            var item = inventory[lastIndex];
            //var itemCode = item.itemCode;
            item.gameObject.SetActive(false);
            inventory.RemoveAt(lastIndex);
            var image = inventoryImage[inventoryImage.Count - 1];
            Destroy(image.gameObject);
            inventoryImage.RemoveAt(lastIndex);



            //�������� �ٽ� ������� �Ѵ�.
            return item;
        }

        return null;
        
        //�̹��� ���ֱ� - ������ ��� ��Ű��?
        //���� �����
    }
}
