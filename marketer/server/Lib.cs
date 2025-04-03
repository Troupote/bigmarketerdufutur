using SpacetimeDB;
using SpacetimeDB.Internal.TableHandles;

public static partial class Module
{
    [Table(Name = "product", Public = true)]
    public partial class Product
    {
        //public Identity Identity;
        [PrimaryKey]
        [AutoInc]
        public int Id;
        public string Name;
        public bool Sellable = false;
        public float Price = 0f;
    }
    [Table(Name = "market")]
    public partial class Market
    {
        
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
        //var guidd = Guid.NewGuid().ToByteArray()[0];
        //var identityy = new Identity(guidd);

        //Console.WriteLine(guidd);
        //Console.WriteLine(guidd.ToString());
        //Console.WriteLine(identityy);
        //Console.WriteLine(identityy.ToString());

        ctx.Db.product.Insert(new Product
        {
            //Identity = identityy,
            Name = "Table",
            Price = 0f,
        });
    }

    [Reducer]
    public static void Populate(ReducerContext ctx, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            ctx.Db.product.Insert(new Product
            {
                Name = $"Product {i}",
                Price = ctx.Rng.NextSingle(),
                Sellable = i % 2 == 0
            });
        }
    }

}
