namespace SEBtask.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IClientRepository _clientRepository;
        private IAgreementRepository _agreementRepository;

        public IClientRepository Client
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_repositoryContext);
                }

                return _clientRepository;
            }
        }

        public IAgreementRepository Agreement
        {
            get
            {
                if (_agreementRepository == null)
                {
                    _agreementRepository = new AgreementRepository(_repositoryContext);
                }

                return _agreementRepository;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
