﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopMSU_PRO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APD65_63011212004Entities2 : DbContext
    {
        public APD65_63011212004Entities2()
            : base("name=APD65_63011212004Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ShopCustomer> ShopCustomers { get; set; }
        public virtual DbSet<ShopEmployee> ShopEmployees { get; set; }
        public virtual DbSet<ShopOrder> ShopOrders { get; set; }
        public virtual DbSet<ShopOrderItem> ShopOrderItems { get; set; }
        public virtual DbSet<ShopProduct> ShopProducts { get; set; }
        public virtual DbSet<ShopSale> ShopSales { get; set; }
        public virtual DbSet<ShopType> ShopTypes { get; set; }
    }
}