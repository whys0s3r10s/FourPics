using System;

namespace FourPics
{
    public abstract class RepositoryBase
    {
        protected IStorage Storage { get; }

        public RepositoryBase(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
    }
}