using System;
using System.Collections.Generic;
using System.Text;

namespace SouliCool.Tutorials.Contracts
{
    public interface IRepositoryWrapper
    {
        IEntityRepository Entity { get; }
    }
}
