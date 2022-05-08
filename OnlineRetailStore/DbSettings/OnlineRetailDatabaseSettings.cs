namespace OnlineRetailStoreApi.DbSettings
{
    public class OnlineRetailDatabaseSettings: IOnlineRetailDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public  string DatabaseName { get; set; }
        public string ProductCollectionName { get; set; }
        public string OrderCollectionName { get; set; }
    }
}
