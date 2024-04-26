using System;

namespace SpartaDungeon
{
    internal class SpartaDungeon
    {
        static void Main(string[] args)
        {
            Character chr = new Character();
            Items items = new Items();
            Random random = new Random();

            string input;
            int inputNum;
            int systemNum;
            int randomHP;
            int randomGold;
            int[] systemArr = new int[32];  // 아이템 개수가 늘어날 때 배열 늘리기

            // 캐릭터 초기 설정
            chr.Level = 01;
            chr.Atk = 10;
            chr.Def = 5;
            chr.MaxHP = 100;
            chr.HP = 100;  // 0 이하가 되어도 게임 오버되지 않음, 단 던전 입장 불가
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
                Magenta("3"); Console.WriteLine(". 상점");
                Magenta("4"); Console.WriteLine(". 던전 입장");
                Magenta("5"); Console.WriteLine(". 휴식하기\n");

                while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5")
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
                        case "4":
                            Magenta("4"); Console.WriteLine(". 던전 입장");
                            DungeonEntrance();
                            break;
                        case "5":
                            Magenta("5"); Console.WriteLine(". 휴식하기");
                            Rest(0);
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
                Console.Write($"체 력 : {chr.HP} / {chr.MaxHP}");
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
                        Console.Write("- "); items.ItemInfo(0, i);
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
                        Console.Write("- "); Magenta($"{systemNum} "); items.ItemInfo(0, i);
                        systemNum++;
                    }
                }

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (true)
                {
                    Console.Write("장착 상태를 변경하실 아이템을 선택해 주세요.\n>> ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out inputNum))
                    {
                        if (inputNum > 0 && inputNum < systemNum)
                        {
                            Magenta($"{inputNum}"); Console.WriteLine($". {items.ItemName[systemArr[inputNum]]}");
                            if (items.ItemState[systemArr[inputNum]] == 1)  // 장착
                            {
                                Equip(systemArr[inputNum]);
                            }
                            else if (items.ItemState[systemArr[inputNum]] == 2)  // 장착 해제
                            {
                                Unequip(systemArr[inputNum]);
                                InvenEquip();
                            }
                            else
                            {
                                Console.WriteLine("장착/해제할 수 없는 아이템입니다.");
                            }
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
                switch (items.ItemType[i])
                {
                    case 0:
                        if (chr.Equipments[0] >= 0)  // 방어구일 경우
                        {
                            Unequip(chr.Equipments[0]);
                        }
                        chr.Equipments[0] = i;
                        break;
                    case 1:
                        if (chr.Equipments[1] >= 0)  // 무기일 경우
                        {
                            Unequip(chr.Equipments[1]);
                        }
                        chr.Equipments[1] = i;
                        break;
                    case 2:
                        if (chr.Equipments[2] >= 0)  // 장신구일 경우
                        {
                            Unequip(chr.Equipments[2]);
                        }
                        chr.Equipments[2] = i;
                        break;
                    default:
                        break;
                }

                items.ItemState[i] = 2;

                statChanges[1] += items.ItemAtk[i];
                statChanges[2] += items.ItemDef[i];
                statChanges[3] += items.ItemHP[i];
                chr.Atk = 10 + statChanges[1];
                chr.Def = 5 + statChanges[2];
                chr.MaxHP = 100 + statChanges[3];
                chr.HP += statChanges[3];
                //if (chr.HP <= 0)
                //{
                //    chr.HP = 1;
                //}

                InvenEquip();
            }


            // 메서드 - 아이템 장착 해제
            void Unequip(int i)
            {
                switch (items.ItemType[i])
                {
                    case 0:
                        chr.Equipments[0] = -1;  // 방어구일 경우
                        break;
                    case 1:
                        chr.Equipments[1] = -1;  // 무기일 경우
                        break;
                    case 2:
                        chr.Equipments[2] = -1;  // 장신구일 경우
                        break;
                    default:
                        break;
                }

                statChanges[1] -= items.ItemAtk[i];
                statChanges[2] -= items.ItemDef[i];
                statChanges[3] -= items.ItemHP[i];
                chr.Atk = 10 + statChanges[1];
                chr.Def = 5 + statChanges[2];
                chr.MaxHP = 100 + statChanges[3];
                chr.HP -= statChanges[3];
                //if (chr.HP <= 0)
                //{
                //    chr.HP = 1;
                //}

                items.ItemState[i] = 1;
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
                    Console.Write("- "); items.ItemInfo(1, i);
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
                            ShopSell(0);
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
            void ShopBuy(bool purchased)
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
                    Console.Write("- "); Magenta($"{i + 1} "); items.ItemInfo(1, i);
                }

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                if (purchased)
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    purchased = false;
                }

                while (true)
                {
                    Console.Write("구매하실 아이템을 선택해 주세요.\n>> ");
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


            // 메서드 - 아이템 판매 화면
            void ShopSell(int s)
            {
                input = "";
                systemNum = 1;
                Console.Clear();

                PrintLine();
                Yellow("상점 - 아이템 판매\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있습니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine(chr.Gold + " G\n");

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
                        Console.Write("- "); Magenta($"{systemNum} "); items.ItemInfo(2, i);
                        systemNum++;
                    }
                }

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                if (s == 1)
                {
                    Console.WriteLine("판매를 완료했습니다.");
                    s = 0;
                }
                else if (s == -1)
                {
                    Console.WriteLine("판매를 취소했습니다.");
                    s = 0;
                }

                while (true)
                {
                    Console.Write("판매하실 아이템을 선택해 주세요.\n>> ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out inputNum))
                    {
                        if (inputNum > 0 && inputNum < systemNum)
                        {
                            Magenta($"{inputNum}"); Console.WriteLine($". {items.ItemName[systemArr[inputNum]]}");
                            Sell(systemArr[inputNum]);
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


            // 메서드 - 아이템 판매
            void Sell(int i)
            {
                if (items.ItemState[i] == 1)
                {
                    chr.Gold += (int)(items.ItemPrice[i] * 0.85f);
                    items.ItemState[i] = 0;

                    ShopSell(1);
                }
                else if (items.ItemState[i] == 2)
                {
                    input = "";
                    Console.WriteLine("현재 장착 중인 아이템입니다. 장착을 해제하고 판매하시겠습니까?");
                    Magenta("1"); Console.WriteLine(". 판매");
                    Magenta("0"); Console.WriteLine(". 취소");

                    while (input != "1" && input != "0")
                    {
                        Console.Write(">> ");
                        input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                Magenta("1"); Console.WriteLine(". 판매");

                                Unequip(i);
                                chr.Gold += (int)(items.ItemPrice[i] * 0.85f);
                                items.ItemState[i] = 0;

                                ShopSell(1);
                                break;
                            case "0":
                                Magenta("0"); Console.WriteLine(". 취소");
                                ShopSell(-1);
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("판매할 수 없는 아이템입니다.");
                }
            }


            // 메서드 - 던전 입장
            void DungeonEntrance()
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("던전\n");
                Console.WriteLine("이곳에서 던전으로 들어갈 수 있습니다.");
                Console.WriteLine($"\n현재 체력: {chr.HP} / {chr.MaxHP}");

                if (chr.HP <= 0)
                {
                    Console.WriteLine("체력을 전부 소진해 던전에 입장할 수 없습니다! 휴식을 취하여 체력을 회복해 주세요.");

                    PrintLine();
                    Exit();
                }
                else
                {
                    PrintLine();

                    Magenta("1"); Console.WriteLine(". 쉬운 던전 | 방어력 5 이상 권장");
                    Magenta("2"); Console.WriteLine(". 일반 던전 | 방어력 11 이상 권장");
                    Magenta("3"); Console.WriteLine(". 어려운 던전 | 방어력 17 이상 권장");
                    Magenta("0"); Console.WriteLine(". 나가기\n");

                    while (input != "1" && input != "2" && input != "3" && input != "0")
                    {
                        Console.Write("입장하실 던전을 선택해 주세요.\n>> ");
                        input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                Magenta("1"); Console.WriteLine(". 쉬운 던전");
                                Dungeon(1);
                                break;
                            case "2":
                                Magenta("2"); Console.WriteLine(". 일반 던전");
                                Dungeon(2);
                                break;
                            case "3":
                                Magenta("3"); Console.WriteLine(". 어려운 던전");
                                Dungeon(3);
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
            }


            // 메서드 - 던전 진행
            void Dungeon(int difficulty)
            {
                Console.Clear();

                PrintLine();
                switch (difficulty)
                {
                    case 1: Yellow("쉬운 던전\n"); systemNum = 5; break;
                    case 2: Yellow("일반 던전\n"); systemNum = 11; break;
                    case 3: Yellow("어려운 던전\n"); systemNum = 17; break;
                    default: systemNum = 1; break;
                }
                Console.WriteLine("던전을 공략하는 중입니다...");

                PrintLine();
                Console.Write("아무 키나 눌러 주세요.\n>> ");

                if (chr.Def < systemNum && random.Next(1, 101) <= 40)
                {
                    Console.ReadKey();
                    DungeonFail(difficulty);
                }

                Console.ReadKey();
                DungeonSuccess(difficulty);
            }


            // 메서드 - 던전 공략 실패
            void DungeonFail(int difficulty)
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("던전 공략 실패\n");
                switch (difficulty)
                {
                    case 1: Console.WriteLine("쉬운 던전 공략에 실패했습니다.\n"); break;
                    case 2: Console.WriteLine("일반 던전 공략에 실패했습니다.\n"); break;
                    case 3: Console.WriteLine("어려운 던전 공략에 실패했습니다.\n"); break;
                }

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력: {chr.HP} -> {chr.HP - (chr.MaxHP / 2)}");

                chr.HP -= chr.MaxHP / 2;

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (input != "0")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "0":
                            Magenta("0"); Console.WriteLine(". 나가기");
                            DungeonEntrance();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }


            // 메서드 - 던전 클리어
            void DungeonSuccess(int difficulty)
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("던전 클리어\n");
                switch (difficulty)
                {
                    case 1: Console.WriteLine("축하합니다!!\n쉬운 던전을 클리어했습니다.\n");
                        systemNum = 5; randomGold = 1000; break;
                    case 2: Console.WriteLine("축하합니다!!\n일반 던전을 클리어했습니다.\n");
                        systemNum = 11; randomGold = 1700; break;
                    case 3: Console.WriteLine("축하합니다!!\n어려운 던전을 클리어했습니다.\n");
                        systemNum = 17; randomGold = 2500; break;
                    default: systemNum = 1; randomGold = 0; break;
                }

                randomHP = random.Next(20 + chr.Def - systemNum, 36 + chr.Def - systemNum);
                randomGold += random.Next(chr.Atk, (chr.Atk * 2) + 1);

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력: {chr.HP} -> {chr.HP - randomHP}");
                Console.WriteLine($"Gold: {chr.Gold} G -> {chr.Gold + randomGold} G");

                chr.HP -= randomHP;
                chr.Gold += randomGold;

                PrintLine();
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (input != "0")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "0":
                            Magenta("0"); Console.WriteLine(". 나가기");
                            DungeonEntrance();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }


            // 메서드 - 휴식하기
            void Rest(int r)
            {
                input = "";
                Console.Clear();

                PrintLine();
                Yellow("휴식하기\n");
                Console.WriteLine("500 골드를 내면 체력을 회복할 수 있습니다.\n");

                if (r == -1)
                {
                    Console.WriteLine("골드가 부족합니다.");
                    Console.WriteLine($"현재 체력: {chr.HP} / {chr.MaxHP}");
                    Console.WriteLine($"보유 골드: {chr.Gold} G");
                }
                else if (r == 1)
                {
                    Console.WriteLine("휴식을 완료했습니다.");
                    Console.WriteLine($"현재 체력: {chr.HP} -> {chr.MaxHP} / {chr.MaxHP}");
                    Console.WriteLine($"보유 골드: {chr.Gold} G -> {chr.Gold - 500} G");

                    chr.HP = chr.MaxHP;
                    chr.Gold -= 500;
                }
                else if (r == 2)
                {
                    Console.WriteLine("휴식을 완료했습니다.");
                    Console.WriteLine($"현재 체력: {chr.HP} -> {chr.HP} / {chr.MaxHP}");
                    Console.WriteLine($"보유 골드: {chr.Gold} G -> {chr.Gold - 500} G");

                    chr.Gold -= 500;
                }
                else
                {
                    Console.WriteLine($"현재 체력: {chr.HP} / {chr.MaxHP}");
                    Console.WriteLine($"보유 골드: {chr.Gold} G");
                }

                PrintLine();
                Magenta("1"); Console.WriteLine(". 휴식하기");
                Magenta("0"); Console.WriteLine(". 나가기\n");

                while (input != "1" && input != "0")
                {
                    Console.Write("원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Magenta("1"); Console.WriteLine(". 휴식하기");
                            if (chr.Gold < 500)
                            {
                                Rest(-1);
                            }
                            else if (chr.HP >= chr.MaxHP)
                            {
                                input = "";
                                Console.WriteLine("현재 체력이 최대 체력 이상이므로 휴식을 취해도 체력이 회복되지 않습니다.");
                                Console.WriteLine("그래도 휴식을 취합니까?");
                                Magenta("1"); Console.WriteLine(". 휴식");
                                Magenta("0"); Console.WriteLine(". 취소하고 나가기");
                                while (input != "1" && input != "0")
                                {
                                    Console.Write(">> ");
                                    input = Console.ReadLine();

                                    switch (input)
                                    {
                                        case "1":
                                            Magenta("1"); Console.WriteLine(". 휴식");
                                            Rest(2);
                                            break;
                                        case "0":
                                            Magenta("0"); Console.WriteLine(". 취소하고 나가기");
                                            Start();
                                            break;
                                        default:
                                            Console.WriteLine("잘못된 입력입니다.");
                                            break;
                                    }
                                }
                            }
                            else { Rest(1); }
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
        }
    }


    class Character
    {
        public string Name = "Draca";
        public string Class = "전사";

        public int[] Equipments = { -1, -1, -1 };  // 방어구, 무기, 장신구 칸  // -1: 미착용

        // 캐릭터의 레벨, 공격력, 방어력, 체력, 골드를 저장하는 배열
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
    }


    class Items
    {
        public string[] ItemName = { "천 갑옷", "가죽 갑옷", "무쇠 갑옷", "스파르타의 갑옷",
            "낡은 단도", "청동 검", "강철 도끼", "스파르타의 창"};
        public int[] ItemAtk = { 0, 0, 0, 0, 2, 5, 8, 11 };
        public int[] ItemDef = { 5, 10, 20, 30, 0, 0, 0, 0 };
        public int[] ItemHP = { 0, 0, 0, 0, 0, 0, 0, 0 };
        public string[] ItemDesc = { "천과 솜을 여러 겹 덧대 만들어진 가벼운 갑옷입니다.",
            "경화 처리를 거쳐 단단해진 가죽으로 만든 갑옷입니다.",
            "무쇠로 만들어져 튼튼한 갑옷입니다.",
            "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
            "쉽게 볼 수 있는 낡은 단도입니다.",
            "어디선가 사용되었던 것 같은 검입니다.",
            "두 손으로 쥐고 휘두르는 묵직한 도끼입니다.", 
            "스파르타의 전사들이 사용했다는 전설의 창입니다."};
        public int[] ItemPrice = { 1000, 1800, 3500, 5000, 600, 1500, 2700, 4000 };
        public int[] ItemState = { 0, 0, 0, 0, 0, 0, 0, 0 };  // -1: 미획득, 0: 미구매, 1: 구매, 2: 장착
        public int[] ItemType = { 0, 0, 0, 0, 1, 1, 1, 1 };  // 0: 방어구, 1: 무기, 2: 장신구


        public void ItemInfo(int s, int i)
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
            // 1: 상점이라면 가격이나 구매 완료 표시, 2: 아이템 판매라면 가격의 80% 표시
            if (s == 1)
            {
                if (ItemState[i] == 0)
                {
                    Console.Write($" | {ItemPrice[i]} G");
                }
                else if (ItemState[i] == 1 || ItemState[i] == 2)
                {
                    Console.Write(" | 구매 완료");
                }
            }
            else if (s == 2)
            {
                Console.Write($" | {(int)(ItemPrice[i] * 0.85f)} G");
            }
            Console.WriteLine();
        }
    }
}
