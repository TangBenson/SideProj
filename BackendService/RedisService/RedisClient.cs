using StackExchange.Redis;

namespace RedisService;

public sealed class RedisClient
{
    // 宣告一個 Lazy物件用於延遲初始化 ConnectionMultiplexer物件
    private static readonly Lazy<ConnectionMultiplexer> s_connectionLazy;

    private static string _setting = "";

    /*
    這是一個 C# 8.0 引入的簡化屬性（Simplified Property）的寫法，
    等同 private ConnectionMultiplexer Instance{ get{ return s_connectionLazy.Value; }}。
    而 private ConnectionMultiplexer Instance = s_connectionLazy.Value;
    這是一個字段（field）的定義，它的值在初始化後不會再改變。這就是字段和屬性之間的區別。
    */
    // 通過 s_connectionLazy.Value取得 ConnectionMultiplexer實例
    public static ConnectionMultiplexer Instance => s_connectionLazy.Value;

    public static IDatabase Database => Instance.GetDatabase();

    /*
    GPT:使用 static 關鍵字而不是將建構子設為 private 的主要原因是因為這個類別
    被設計為一個靜態類別，旨在提供對 Redis 客戶端的全局唯一訪問點。
    這種設計模式被稱為單例模式（Singleton Pattern）的一種變體，
    但這裡是通過靜態成員和方法實現的，而不是通過常見的單例設計模式來實現。

    靜態建構子用於初始化類別的靜態成員。它在類別被加載到記憶體中時自動被呼叫，
    並且只會被執行一次。由於靜態建態子是自動執行的，因此不需要也不能設為 private

    為什麼不使用私有建構子？
    在傳統的單例設計模式中，私有建構子確保外部代碼不能直接創建類別的實例。
    然而，在你的例子中，因為類別是用靜態成員和方法來實現的，所以根本沒有實例化的概念，
    因此不需要私有建構子。相反，靜態建構子用於初始化靜態成員。

    總結
    這種設計允許全局訪問 Redis 客戶端，無需創建 RedisClient 類別的實例。
    透過靜態成員和方法，確保所有對 Redis 的訪問都是通過同一個 ConnectionMultiplexer 
    實例進行，從而實現資源共享和管理的效率。這種設計模式在需要全局訪問點和資源管理的場景下很有用，
    但也要注意避免過度使用靜態成員，因為它們可能會使代碼測試和維護變得更加困難。

    在 C# 中，靜態建構子（static constructor）是特殊的，因為它既不可以被聲明為 public 
    也不可以被聲明為 private，或者任何其他的訪問修飾符。靜態建構子的訪問級別是隱含的，
    並且它們總是私有的（private）。這意味著靜態建構子只能由類別本身內部調用，
    並且在類別第一次被加載或者第一次被引用到任何靜態成員之前，由 .NET 運行時自動調用一次
    */
    static RedisClient()
    {
        /*
        初始化了 s_connectionLazy 对象。s_connectionLazy 的初始化是通过一个 Lazy 委托来进行的，
        该委托会在首次访问 Instance 属性时执行
        */
        s_connectionLazy = new Lazy<ConnectionMultiplexer>(() =>
        {
            if (string.IsNullOrWhiteSpace(_setting))
            {
                return ConnectionMultiplexer.Connect("localhost");
            }
            return ConnectionMultiplexer.Connect(_setting);
        });
    }
    public static void Init(string setting)
    {
        _setting = setting;
    }
}
