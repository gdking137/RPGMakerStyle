using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private DatabaseManager theDatabase;
    private OrderManager theOrder;
    private AudioManager theAudio;
    private OkOrCancel theOOC;

    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound;
    public string beep_sound;

    private InventorySlot[] slots; // �κ��丮 ���Ե�

    private List<Item> inventoryItemList; // �÷��̾ ������ ������ ����Ʈ.
    private List<Item> inventoryTabList; // ������ �ǿ� ���� �ٸ��� ������ ������ ����Ʈ.

    public Text Description_Text; // �ο� ����.
    public string[] tabDescription; // �� �ο� ����.

    public Transform tf; // slot �θ�ü.

    public GameObject go; // �κ��丮 Ȱ��ȭ ��Ȱ��ȭ.
    public GameObject[] selectedTabImages;
    public GameObject go_OOC; // ������ Ȱ��ȭ ��Ȱ��ȭ.

    private int selectedItem; // ���õ� ������.
    private int selectedTab; // ���õ� ��.

    private bool activated; // �κ��丮 Ȱ��ȭ�� true;
    private bool tabActivated; // �� Ȱ��ȭ�� true
    private bool itemActivated; // ������ Ȱ��ȭ�� true.
    private bool stopKeyInput; // Ű�Է� ���� (�Һ��� �� ���ǰ� ���� �ٵ�, �� �� Ű�Է� ����)
    private bool preventExec; // �ߺ����� ����.

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Use this for initialization
    void Start()
    {
        instance = this;
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        theOOC = FindObjectOfType<OkOrCancel>();

        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // �κ��丮 ���� �ʱ�ȭ

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } // �� Ȱ��ȭ

    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    } // ���õ� ���� �����ϰ� �ٸ� ��� ���� �÷� ���İ� 0���� ����.

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // ���õ� �� ��¦�� ȿ��

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 3:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        } // �ǿ� ���� ������ �з�. �װ��� �κ��丮 �� ����Ʈ�� �߰�

        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } // �κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�

        SelectedItem();
    } // ������ Ȱ��ȭ (inventoryTabList�� ���ǿ� �´� �����۵鸸 �־��ְ�, �κ��丮 ���Կ� ���)

    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "�ش� Ÿ���� �������� �����ϰ� ���� �ʽ��ϴ�.";
    } // ���õ� �������� �����ϰ�, �ٸ� ��� ���� �÷� ���İ��� 0���� ����.

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // ���õ� ������ ��¦�� ȿ��.

    // Update is called once per frame
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;

                if (activated)
                {
                    theAudio.Play(open_sound);
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            } // �κ��丮 ���� �ݱ�

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        theAudio.Play(key_sound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;
                        theAudio.Play(key_sound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        theAudio.Play(enter_sound);
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }

                } // �� Ȱ��ȭ�� Ű�Է� ó��.

                else if (itemActivated)
                {
                    if (inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedTab == 0) // �Ҹ�ǰ
                            {
                                StartCoroutine(OOCCoroutine("���", "���"));
                            }
                            else if (selectedTab == 1)
                            {
                                StartCoroutine(OOCCoroutine("����", "���"));
                            }
                            else // ������ ���
                            {
                                theAudio.Play(beep_sound);
                            }

                        }
                    }

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(cancel_sound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                } // ������ Ȱ��ȭ�� Ű�Է� ó��.

                if (Input.GetKeyUp(KeyCode.Z)) // �ߺ� ���� ����.
                    preventExec = false;
            } // �κ��丮�� ������ Ű�Է�ó�� Ȱ��ȭ.
        }
    }

    IEnumerator OOCCoroutine(string _up, string _down)
    {
        theAudio.Play(enter_sound);
        stopKeyInput = true;

        go_OOC.SetActive(true);
        theOOC.ShowTwoChoice(_up, _down);

        yield return new WaitUntil(() => !theOOC.activated);
        if (theOOC.GetResult())
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    if (selectedTab == 0)
                    {
                        theDatabase.UseItem(inventoryItemList[i].itemID);

                        if (inventoryItemList[i].itemCount > 1)
                            inventoryItemList[i].itemCount--;
                        else
                            inventoryItemList.RemoveAt(i);

                        // theAudio.Play() // ������ �Դ� �Ҹ� ���.

                        ShowItem();
                        break;
                    }
                    else if (selectedTab == 1)
                    {
                        theEquip.EquipItem(inventoryItemList[i]);
                        inventoryItemList.RemoveAt(i);
                        ShowItem();
                        break;
                    }
                }
            }
        }
        stopKeyInput = false;
        go_OOC.SetActive(false);
    }

}
