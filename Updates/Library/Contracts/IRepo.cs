namespace ProductLib
{
    public interface IRepo<TEntity>
        where TEntity : IKey
    {
        void Create(TEntity entity);
        IQueryable<TEntity> GetQueryable();
        public bool Update(TEntity entity);
        public bool Delete(string id);
    }

}
