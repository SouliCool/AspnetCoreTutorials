using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Entities.Models;
using SouliCool.Tutorials.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouliCool.Tutorials.Repository
{
    public class EntityRepository:RepositoryBase<Entity> , IEntityRepository
    {
        public EntityRepository(EntitiesDbContext entitiesDbContext)
            : base(entitiesDbContext)
        {
        }
    }
}
