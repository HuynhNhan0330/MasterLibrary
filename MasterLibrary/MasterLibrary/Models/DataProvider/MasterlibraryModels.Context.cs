﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace MasterLibrary.Models.DataProvider
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class MasterlibraryEntities : DbContext
{
    public MasterlibraryEntities()
        : base("name=MasterlibraryEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<CTHD> CTHDs { get; set; }

    public virtual DbSet<DAYKE> DAYKEs { get; set; }

    public virtual DbSet<GIOHANG> GIOHANGs { get; set; }

    public virtual DbSet<HOADON> HOADONs { get; set; }

    public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }

    public virtual DbSet<NHAPSACH> NHAPSACHes { get; set; }

    public virtual DbSet<SACH> SACHes { get; set; }

    public virtual DbSet<TANG> TANGs { get; set; }

    public virtual DbSet<USERROLE> USERROLEs { get; set; }

}

}

