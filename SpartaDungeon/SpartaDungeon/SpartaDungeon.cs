using System;

namespace SpartaDungeon
{
    internal class SpartaDungeon
    {
        static void Main(string[] args)
        {
            string input;
            int inputNum;
            int systemNum;
            int[] systemArr = new int[32];  // 아이템 개수가 늘어날 때 배열 늘리기

            Items items = new Items();
            Character chr = new Character();

            // 캐릭터 초기 설정
            chr.Level = 01;
            chr.Atk = 10;
            chr.Def = 5;
            chr.HP = 100;
            chr.Gold = 1500;
            int[] statChanges = { 0, 0, 0, 0, 0 };


            // 게임 시작
            Start();


            #region 편의용 메서드

            // 메서드 - 줄 긋기
            void PrintLine()
            {
                Console.WriteLine();
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("=");
                }
                Console.WriteLine("\n");
            }


            // 메서드 - 노란색 글자
            void Yellow(string text)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(text);
                Console.ResetColor();
            }


            // 메서드 - 마젠타색 글자
            void Magenta(string text)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(text);
                Console.ResetColor();
            }


            // 메서드 - 나가기
            void Exit()
            {
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (input != "0")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "0":
                            Magenta("0"); Console.WriteLine(". 나가기");
                            Start();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }

            #endregion 편의용 메서드


            // 메서드 - 시작 화면
            void Start()
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("마을\n");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

                PrintLine();
                Magenta("1"); Console.WriteLine(". 상태 보기");
                Magenta("2"); Console.WriteLine(". 인벤토리");
                Magenta("3"); Console.WriteLine(". 상점\n");

                while (input != "1" && input != "2" && input != "3")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Magenta("1"); Console.WriteLine(". 상태 보기");
                            ChrStatus();
                            break;
                        case "2":
                            Magenta("2"); Console.WriteLine(". 인벤토리");
                            Inventory();
                            break;
                        case "3":
                            Magenta("3"); Console.WriteLine(". 상점");
                            Shop();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }


            // 메서드 - 상태 보기
            void ChrStatus()
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("상태 보기\n");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine("Lv. " + chr.Level.ToString("D2"));
                Console.WriteLine($"{chr.Name} ({chr.Class})");
                Console.Write("\n공격력: " + chr.Atk);
                if (statChanges[1] != 0) { Console.Write($" ({statChanges[1].ToString("+#;-#;0")})"); }
                Console.WriteLine();
                Console.Write("방어력: " + chr.Def);
                if (statChanges[2] != 0) { Console.Write($" ({statChanges[2].ToString("+#;-#;0")})"); }
                Console.WriteLine();
                Console.Write("체 력 : " + chr.HP);
                if (statChanges[3] != 0) { Console.Write($" ({statChanges[3].ToString("+#;-#;0")})"); }
                Console.WriteLine();
                Console.WriteLine("\nGold: " + chr.Gold + " G");

                PrintLine();
                Exit();
            }


            // 메서드 - 인벤토리
            void Inventory()
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("인벤토리\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.ItemName.Length; i++)
                {
                    if (items.ItemState[i] < 1)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("- "); items.ItemInfo(false, i);
                    }
                }

                PrintLine();
                Magenta("1"); Console.WriteLine(". 장착 관리");
                Magenta("0"); Console.WriteLine(". 나가기\n");


                while (input != "1" && input != "0")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Magenta("1"); Console.WriteLine(". 장착 관리");
                            InvenEquip();
                            break;
                        case "0":
                            Magenta("0"); Console.WriteLine(". 나가기");
                            Start();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }


            // 메서드 - 장착 관리 화면
            void InvenEquip()
            {
                input = "";
                systemNum = 1;
                Console.Clear();

                PrintLine();
                Yellow("인벤토리 - 장착 관리\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.ItemName.Length; i++)
                {
                    if (items.ItemState[i] < 1)
                    {
                        continue;
                    }
                    else
                    {
                        systemArr[systemNum] = i;
                        Console.Write("- "); Magenta($"{systemNum} "); items.ItemInfo(false, i);
                        systemNum++;
                    }
                }

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (true)
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out inputNum))
                    {
                        if (inputNum > 0 && inputNum < systemNum)
                        {
                            Magenta($"{inputNum}"); Console.WriteLine($". {items.ItemName[systemArr[inputNum]]}");
                            Equip(systemArr[inputNum]);
                        }
                        else if (inputNum == 0)
                        {
                            Magenta("0"); Console.WriteLine(". 나가기");
                            Inventory();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }


            // 메서드 - 아이템 장착
            void Equip(int i)
            {
                if (items.ItemState[i] == 1)  // 장착
                {
                    items.ItemState[i] = 2;

                    statChanges[1] += items.ItemAtk[i];
                    statChanges[2] += items.ItemDef[i];
                    statChanges[3] += items.ItemHP[i];
                    chr.Atk = 10 + statChanges[1];
                    chr.Def = 5 + statChanges[2];
                    chr.HP = 100 + statChanges[3];

                    InvenEquip();
                }
                else if (items.ItemState[i] == 2)  // 장착 해제
                {
                    items.ItemState[i] = 1;

                    statChanges[1] -= items.ItemAtk[i];
                    statChanges[2] -= items.ItemDef[i];
                    statChanges[3] -= items.ItemHP[i];
                    chr.Atk = 10 + statChanges[1];
                    chr.Def = 5 + statChanges[2];
                    chr.HP = 100 + statChanges[3];

                    InvenEquip();
                }
                else
                {
                    Console.WriteLine("장착할 수 없는 아이템입니다.");
                }
            }


            // 메서드 - 상점
            void Shop()
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("상점\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있습니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine(chr.Gold + " G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.ItemName.Length; i++)
                {
                    Console.Write("- "); items.ItemInfo(true, i);
                }

                PrintLine();
                Magenta("1"); Console.WriteLine(". 아이템 구매");
                Magenta("2"); Console.WriteLine(". 아이템 판매");
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (input != "1" && input != "0")  // && input != "2" 
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Magenta("1"); Console.WriteLine(". 아이템 구매");
                            ShopBuy(false);
                            break;
                        case "2":
                            Magenta("2"); Console.WriteLine(". 아이템 판매");
                            Console.WriteLine("아직 구현되지 않은 기능입니다.");
                            break;
                        case "0":
                            Magenta("0"); Console.WriteLine(". 나가기");
                            Start();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }


            // 메서드 - 아이템 구매 화면
            void ShopBuy(bool purchase)
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("상점 - 아이템 구매\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있습니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine(chr.Gold + " G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.ItemName.Length; i++)
                {
                    Console.Write("- "); Magenta($"{i + 1} "); items.ItemInfo(true, i);
                }

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                if (purchase)
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    purchase = false;
                }

                while (true)
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out inputNum))
                    {
                        if (inputNum > 0 && inputNum < (items.ItemName.Length + 1))
                        {
                            Magenta($"{inputNum}"); Console.WriteLine($". {items.ItemName[inputNum - 1]}");
                            Buy(inputNum - 1);
                        }
                        else if (inputNum == 0)
                        {
                            Magenta("0"); Console.WriteLine(". 나가기");
                            Shop();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }


            // 메서드 - 아이템 구매
            void Buy(int i)
            {
                if (items.ItemState[i] > 0)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (items.ItemState[i] == 0)
                {
                    if (chr.Gold < items.ItemPrice[i])
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                    else
                    {
                        chr.Gold -= items.ItemPrice[i];
                        items.ItemState[i] = 1;

                        ShopBuy(true);
                    }
                }
                else
                {
                    Console.WriteLine("구매할 수 없는 아이템입니다.");
                }
            }
        }
    }


    class Character
    {
        public string Name = "Draca";
        public string Class = "전사";

        public char[] equipments = { '-', '-', '-' };  // 방어구, 무기, 장신구칸

        // 캐릭터의 레벨, 공격력, 방어력, 체력, 골드를 저장하는 배열
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
    }


    class Items
    {
        public string[] ItemName = { "수련자 갑옷", "무쇠 갑옷", "스파르타의 갑옷",
            "낡은 검", "청동 도끼", "스파르타의 창"};
        public int[] ItemAtk = { 0, 0, 0, 2, 5, 7 };
        public int[] ItemDef = { 5, 10, 20, 0, 0, 0 };
        public int[] ItemHP = { 0, 0, 0, 0, 0, 0 };
        public string[] ItemDesc = { "수련에 도움을 주는 갑옷입니다.",
            "무쇠로 만들어져 튼튼한 갑옷입니다.",
            "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
            "쉽게 볼 수 있는 낡은 검입니다.",
            "어디선가 사용되었던 것 같은 도끼입니다.",
            "스파르타의 전사들이 사용했다는 전설의 창입니다."};
        public int[] ItemPrice = { 1000, 1800, 3500, 600, 1500, 2700 };
        public int[] ItemState = { 0, 0, 0, 0, 0, 0 };  // -1: 미획득, 0: 미구매, 1: 보유, 2: 장착
        public int[] ItemClass = { 1, 1, 1, 2, 2, 2 };  // 1: 방어구, 2: 무기, 3: 장신구


        public void ItemInfo(bool Shop, int i)
        {
            if (ItemState[i] == 2)  // 장착했을 경우 아이템 이름 앞에 [E] 표시
            {
                Console.Write("[E]");
            }
            Console.Write($"{ItemName[i]}");
            if (ItemAtk[i] != 0)
            {
                Console.Write($" | 공격력 {ItemAtk[i].ToString("+#;-#;0")}");
            }
            if (ItemDef[i] != 0)
            {
                Console.Write($" | 방어력 {ItemDef[i].ToString("+#;-#;0")}");
            }
            if (ItemHP[i] != 0)
            {
                Console.Write($" | HP {ItemHP[i].ToString("+#;-#;0")}");
            }
            Console.Write($" | {ItemDesc[i]}");
            if (Shop)  // 상점이라면 가격이나 구매 완료 표시
            {
                if (ItemState[i] == 0)
                {
                    Console.Write($" | {ItemPrice[i]} G");
                }
                else if (ItemState[i] > 0)
                {
                    Console.Write(" | 구매 완료");
                }
            }
            Console.WriteLine();
        }
    }
}
