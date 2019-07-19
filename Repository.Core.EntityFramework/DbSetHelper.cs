using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core.EntityFramework
{
    public static class DbSetHelper
    {
        public static DbSet<tEntity> ToDbSet<tEntity>(this object obj)
            where tEntity : class
        {
            return (DbSet<tEntity>)obj;
        }
    }
}
