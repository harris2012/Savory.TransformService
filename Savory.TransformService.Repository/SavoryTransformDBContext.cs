using Savory.Repository;
using Savory.Repository.TransformDB.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Repository
{
    public class SavoryTransformDBContext : DbContext
    {
#if DEBUG
        public SavoryTransformDBContext() : base("SavoryTransformDB")
        {
        }
#else
        public SavoryWikiDBContext() : base(ConnStringProvider.GetConnString("SavoryTransformDB"))
        {
        }
#endif

        public virtual DbSet<TransformEntity> Transform { get; set; }

        public virtual DbSet<TemplateEntity> Template { get; set; }
    }
}
