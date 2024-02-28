namespace Raess.InfraestructureServices.Models;

public class ProductDm
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public float WeightKg { get; set; } = 0;

    public string FamilyCode { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    public int Stock { get; set; } = 0;

    public string Size { get; set; } = string.Empty;

}
