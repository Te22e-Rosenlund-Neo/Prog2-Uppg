namespace Shops
{

    //Generic class to handle shop, can be created with any objects as long as it inherits from Ibuyable interface
    public class Shop<T> where T : IBuyable
    {

        //list of items that is not specified to one specific item
        private List<T> itemsForSale = new List<T>();
        public int money { get; set; }

        //constructor
        public Shop(int money)
        {
            this.money = money;
        }

        //method to add items from one list to the shops list
        public void AddItemList(List<T> list)
        {
            foreach (T item in list)
            {
                itemsForSale.Add(item);
            }
        }

        //money is handled only in the shop, method changes our money
        public void moneyChange(int Change)
        {
            money += Change;
        }

        //method that can return either the bought item, or nothing (player doesnt need to always buy things)
        public T? BuyItem()
        {

            Console.Clear();
            Console.WriteLine("Shop");
            Console.WriteLine($"your money: {money}");
            Console.WriteLine();
            Console.WriteLine("Choose an item writing it's index or exit by pressin 'e'");

            //Displays all buyable items
            for (int i = 0; i < itemsForSale.Count; i++)
            {
                Console.WriteLine($"Index: {i + 1}");
                itemsForSale[i].ShowStats();
                Console.WriteLine("------------------------");
            }
            //checks if input was an int and within the list's limits
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
                        //everything went well, we return chosen item and subtract money
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


