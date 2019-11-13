using Projeto.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistence
{
    public class GenericDal<TEntity> where TEntity : class
    {
        public void Insert(TEntity obj)
        {
            using (Conexao con = new Conexao())
            {
                con.Entry(obj).State = EntityState.Added;
                con.SaveChanges();
            }
        }

        public void Update(TEntity obj)
        {
            using (Conexao con = new Conexao())
            {
                con.Entry(obj).State = EntityState.Modified;
                con.SaveChanges();
            }
        }

        public void Delete(TEntity obj)
        {
            using (Conexao con = new Conexao())
            {
                con.Entry(obj).State = EntityState.Deleted;
                con.SaveChanges();
            }
        }

        public List<TEntity> FindAll()
        {
            using (Conexao con = new Conexao())
            {
                return con.Set<TEntity>().ToList();
            }
        }

        public TEntity FindById(int id)
        {
            using (Conexao con = new Conexao())
            {
                return con.Set<TEntity>().Find(id);
            }
        }
    }
}