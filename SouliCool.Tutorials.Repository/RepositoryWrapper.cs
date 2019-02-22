using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouliCool.Tutorials.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private EntitiesDbContext _repoContext;
        private IEntityRepository _entity;

        public IEntityRepository Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = new EntityRepository(_repoContext);
                }
                return _entity;
            }
        }

        public RepositoryWrapper(EntitiesDbContext repoContext)
        {
            _repoContext = repoContext;
        }
    }
}
