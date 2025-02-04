using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;

public enum ItemType
{
    ARMOR,
    WEAPON
}

class Item
{
    public ItemType type;
    public string name;
    public string desc;
    public int value;
    public int price;
    public bool IsEquip;
    public bool IsBuy;

    public Item(ItemType type, string name, string desc, int value, int price)
    {
        this.type = type;
        this.name = name;
        this.desc = desc;
        this.value = value;
        this.price = price;
        IsEquip = false;
        IsBuy = false;
    }
}

class Shop
{
    public List<Item> items;

    public Shop()
    {
        items = new List<Item>();
    }

    public void AddItem(ItemType type, string name, string desc, int value, int price)
    {
        Item item = new Item(type, name, desc, value, price);
        items.Add(item);
    }
}

class Player
{
    public int level;
    public string name;
    public string job;
    public int attack;
    public int defense;
    public int health;
    public int gold;

    public List<Item> items;

    public Player(string name)
    {
        level = 1;
        this.name = name;
        job = "전사";
        attack = 10;
        defense = 5;
        health = 100;
        gold = 1500;

        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }
}

class Stage
{
    Player player;
    Shop shop;

    public void Setting()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("원하시는 이름을 입력해주세요");
        Console.WriteLine();
        string name = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine($"입력하신 이름은 {name} 입니다.");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.WriteLine("1. 저장");
        Console.WriteLine("2. 취소");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());
        if (input == 1) player = new Player(name);
        else if (input == 2) Setting();

        shop = new Shop();
        shop.AddItem(ItemType.ARMOR, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 1000);
        shop.AddItem(ItemType.ARMOR, "무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, 2000);
        shop.AddItem(ItemType.ARMOR, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, 3500);
        shop.AddItem(ItemType.WEAPON, "낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 600);
        shop.AddItem(ItemType.WEAPON, "청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 1500);
        shop.AddItem(ItemType.WEAPON, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 3000);
    }

    public void MainMenu()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());

        switch (input)
        {
            case 1:
                Status();
                break;
            case 2:
                Inventory();
                break;
            case 3:
                Shop();
                break;
        }
    }

    public void Status()
    {
        Console.Clear();

        int atk = 0;
        int def = 0;

        foreach (Item item in player.items)
        {
            if (item.IsEquip && item.type == ItemType.WEAPON)
                atk += item.value;
            else if (item.IsEquip && item.type == ItemType.ARMOR)
                def += item.value;
        }

        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.level}");
        Console.WriteLine($"{player.name} ( {player.job} )");
        if (atk != 0)
            Console.WriteLine($"공격력 : {player.attack} (+{atk})");
        else
            Console.WriteLine($"공격력 : {player.attack}");
        if (def != 0)
            Console.WriteLine($"방어력 : {player.defense} (+{def})");
        else
            Console.WriteLine($"방어력 : {player.defense}");
        Console.WriteLine($"체 력 : {player.health}");
        Console.WriteLine($"Gold : {player.gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());

        if (input == 0) return;
    }

    public void Inventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        ShowInventory(false);
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());

        if (input == 0) return;
        else if (input == 1) InventoryEquip();
    }

    public void InventoryEquip()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        ShowInventory(true);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());
        if (input == 0) return;
        else if (input < 0 || input > player.items.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1000);
            return;
        }

        player.items[input - 1].IsEquip = !(player.items[input - 1].IsEquip);
    }

    public void ShowInventory(bool interaction)
    {
        int i = 1;
        foreach (Item item in player.items)
        {
            string equip = "";
            string type = "방어력";
            if (item.IsEquip)
                equip = "[E]";
            if (item.type == ItemType.WEAPON)
                type = "공격력";
            if (interaction)
                Console.WriteLine($"- {i} {equip}{item.name}\tㅣ{type} +{item.value}ㅣ{item.desc}");
            else
                Console.WriteLine($"- {equip}{item.name}\tㅣ{type} +{item.value}ㅣ{item.desc}");
            i++;
        }
    }

    public void Shop()
    {
        Console.Clear();

        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        ShowShop(false);
        Console.WriteLine();
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());

        if (input == 0) return;
        else if (input == 1) ShopBuy();
    }

    public void ShopBuy()
    {
        Console.Clear();

        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        ShowShop(true);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int input = int.Parse(Console.ReadLine());
        if (input == 0) return;
        else if (input < 0 || input > shop.items.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");

            Thread.Sleep(1000);
            return;
        }
        else
        {
            if (shop.items[input - 1].IsBuy)
            {
                Console.WriteLine("이미 구매한 아이템입니다");
            }
            else if (shop.items[input - 1].price <= player.gold)
            {
                player.items.Add(shop.items[input - 1]);
                shop.items[input - 1].IsBuy = true;
                player.gold -= shop.items[input - 1].price;
                Console.WriteLine("구매를 완료했습니다");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다");
            }
            Thread.Sleep(1000);
            return;
        }
    }

    public void ShowShop(bool interaction)
    {
        int i = 1;
        foreach (Item item in shop.items)
        {
            string buy = item.price.ToString();
            string type = "방어력";
            if (item.IsBuy)
                buy = "구매완료";
            if (item.type == ItemType.WEAPON)
                type = "공격력";
            if (interaction)
                Console.WriteLine($"- {i} {item.name}\tㅣ{type} +{item.value}ㅣ{item.desc}\tㅣ{buy}");
            else
                Console.WriteLine($"- {item.name}\tㅣ{type} +{item.value}ㅣ{item.desc}\tㅣ{buy}");
            i++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stage stage = new Stage();
        stage.Setting();
        while (true)
        {
            stage.MainMenu();
        }
    }
}