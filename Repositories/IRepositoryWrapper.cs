namespace SEBtask.Repositories
{
    public interface IRepositoryWrapper
    {
        public IClientRepository Client { get; }
        public IAgreementRepository Agreement { get; }
        public void Save();
    }
}
