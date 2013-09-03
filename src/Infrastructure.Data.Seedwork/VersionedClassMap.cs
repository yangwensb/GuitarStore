using Domain.Seedwork;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork
{
    public abstract class VersionedClassMap<T> : ClassMap<T> where T : IVersionedObject
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version).Column("ts").CustomSqlType("Rowversion").Generated.
            Always().UnsavedValue("null");
        }

    }
}
