﻿using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
      _db = db;
      Category = new CategoryRepository(_db);
      MenuItem = new MenuItemRepository(_db);
      ShoppingCart = new ShoppingCartRepository(_db);
      OrderHeader = new OrderHeaderRepository(_db);
      OrderDetails = new OrderDetailRepository(_db);
      ApplicationUser = new ApplicationUserRepository(_db);
    }
    public ICategoryRepository Category { get; private set; }
    public IMenuItemRepository MenuItem { get; private set; }
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public IOrderHeaderRepository OrderHeader { get; private set; }
    public IOrderDetailRepository OrderDetails { get; private set; }
    public IApplicationUserRepository ApplicationUser { get; private set; }

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Save()
    {
      _db.SaveChanges();
    }
  }
}
