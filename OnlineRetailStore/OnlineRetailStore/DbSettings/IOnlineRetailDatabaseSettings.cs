namespace OnlineRetailStoreApi.DbSettings
{
    public interface IOnlineRetailDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; }
        string OrderCollectionName { get; set; }
    }
}
