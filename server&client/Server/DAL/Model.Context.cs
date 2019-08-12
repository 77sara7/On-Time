﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Entities;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
    	var x = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
    	this.Configuration.ProxyCreationEnabled = false;
        ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
            (sender, e) => Apply(e.Entity);
    
        }
    
    	private void Apply(object entity)
            {
                if (entity == null)
                    return;
    
                var properties = entity.GetType().GetProperties()
                    .Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?));
    
                foreach (var property in properties)
                {
                    var dt = property.PropertyType == typeof(DateTime?)
                        ? (DateTime?)property.GetValue(entity)
                        : (DateTime)property.GetValue(entity);
    
                    if (dt == null)
                        continue;
                    property.SetValue(entity, DateTime.SpecifyKind(dt.Value, DateTimeKind.Local));
                }
            }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<v_attached_file> v_attached_file { get; set; }
    
        public virtual int attached_file_add(string file_name, byte[] file_data, ObjectParameter file_id)
        {
            var file_nameParameter = file_name != null ?
                new ObjectParameter("file_name", file_name) :
                new ObjectParameter("file_name", typeof(string));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("attached_file_add", file_nameParameter, file_dataParameter, file_id);
        }
    
        public virtual int attached_file_update(Nullable<System.Guid> file_id, string file_name, byte[] file_data)
        {
            var file_idParameter = file_id.HasValue ?
                new ObjectParameter("file_id", file_id) :
                new ObjectParameter("file_id", typeof(System.Guid));
    
            var file_nameParameter = file_name != null ?
                new ObjectParameter("file_name", file_name) :
                new ObjectParameter("file_name", typeof(string));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("attached_file_update", file_idParameter, file_nameParameter, file_dataParameter);
        }
    
        public virtual int AddFile(string file_name, byte[] file_data, ObjectParameter file_id)
        {
            var file_nameParameter = file_name != null ?
                new ObjectParameter("file_name", file_name) :
                new ObjectParameter("file_name", typeof(string));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddFile", file_nameParameter, file_dataParameter, file_id);
        }
    
        public virtual ObjectResult<Nullable<System.Guid>> file_add(string file_name, byte[] file_data)
        {
            var file_nameParameter = file_name != null ?
                new ObjectParameter("file_name", file_name) :
                new ObjectParameter("file_name", typeof(string));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.Guid>>("file_add", file_nameParameter, file_dataParameter);
        }
    }
}