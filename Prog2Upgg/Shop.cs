namespace Shops{

public class Shop<T> where T : Ibuyable{

    private List<T> itemsForSale;
    private int money;


    public Shop(List<T> itemsForSale, int money){
        this.itemsForSale = itemsForSale;
        this.money = money;
    }

    public T? BuyItem(){

        Console.Clear();
        Console.WriteLine("Shop");
        Console.WriteLine($"your money: {money}");

        for(int i = 0; i < itemsForSale.Count; i++){
            Console.WriteLine("Choose an item or exit by pressin 'e'");
            string answer = Console.ReadLine() ?? "";

            if(answer.ToLower() == "e"){
                return null;
            }

            if(int.TryParse(answer, out int choice)){
                if(choice > 0 && choice <= itemsForSale.Count){
                    T selectedItem = itemsForSale[choice - 1];
                    if(money >= selectedItem.)

                }
            }
        }
    }





}


}