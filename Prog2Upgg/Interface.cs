//interface serves as to make sure all items added to the shop has a cost and a method to show information about it
public interface IBuyable
{
    int cost { get; set;}
    void ShowStats();
}