namespace Testovoe.VendorMachine.Server.Models;

public class Soda
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public required byte[] Image { get; set; }
}
