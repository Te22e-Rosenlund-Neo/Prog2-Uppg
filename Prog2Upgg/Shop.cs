namespace Shops
{

    public class Shop<T> where T : IBuyable
    {

        private List<T> itemsForSale = new List<T>();
        public int money { get; set; }


        public Shop(int money)
        {
            this.money = money;
        }

        public void AddItemList(List<T> list){
            foreach(T item in list){
                itemsForSale.Add(item);
            }
        }

        public void moneyChange(int Change)
        {
            money += Change;
        }

        //money is an issue?
        public T? BuyItem()
        {

            Console.Clear();
            Console.WriteLine("Shop");
            Console.WriteLine($"your money: {money}");
            Console.WriteLine();
            Console.WriteLine("Choose an item writing it's index or exit by pressin 'e'");

            for (int i = 0; i < itemsForSale.Count; i++)
            {
                Console.WriteLine($"Index: {i + 1}");
                itemsForSale[i].ShowStats();
                Console.WriteLine("------------------------");
            }

            string answer;
            while (true)
            {
                Console.WriteLine("Please enter a valid choice");
                answer = Console.ReadLine() ?? "";

                if (answer.ToLower() == "n")
                {
                    return default;
                }
                if (int.TryParse(answer, out int temp))
                {
                    int indexChosen = Convert.ToInt32(answer) - 1;
                    if (indexChosen >= 0 && indexChosen < itemsForSale.Count)
                    {
                        T item = itemsForSale[indexChosen];
                        if (item.Cost <= money)
                        {
                            money -= item.Cost;
                            return item;
                        }
                    }
                }
            }
        }
    }
}


