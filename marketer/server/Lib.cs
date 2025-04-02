using SpacetimeDB;
using SpacetimeDB.Internal.TableHandles;

public static partial class Module
{
    [Table(Name = "product", Public = true)]
    public partial class Product
    {
        [PrimaryKey]
        public Identity Identity;
        [AutoInc] public int Id;
        public string Name;
        public bool Sellable = false;
        public float Price = 0f;
    }
    [Table(Name = "market")]
    public partial class Market
    {
        [PrimaryKey]
        public Identity Identity;
        public string Identifiant;
        public string Name;
        public List<Product> Products;
    }
    
    [Reducer]
    public static void MarketAddProduct(ReducerContext ctx, string productName, string marketName)
    { 
        var market = ctx.Db.market.Iter().Where(x => x.Name == marketName).FirstOrDefault();
        if (market == null)
        {
            throw new Exception($"Market {marketName} not found");
        }
        
    }
    
    [Reducer]
    public static void MarketAdd(ReducerContext ctx, string marketName, string marketIdentifiant = "0x0")
    {
        var marketing = new Market
        {
            Name = marketName,
            Identifiant = marketIdentifiant
        };
        ctx.Db.market.Insert(marketing);
    }
    
    [Reducer]
    public static void ProductAdd(ReducerContext ctx, string productName, bool sellable = false, float price = 0f)
    {
        var product = new Product
        {
            Name = productName,
            Price = price,
            Sellable = sellable
        };
        ctx.Db.product.Insert(product);
    }
    
    [Reducer]
    public static void InitializeProducts(ReducerContext ctx)
    {
        ctx.Db.product.Insert(new Product
        {
            Identity = ctx.Identity,
            Name = "Table",
            Price = 0f,
        });
    }
    
    
}
