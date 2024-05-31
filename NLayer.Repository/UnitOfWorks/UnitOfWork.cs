﻿using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        //
        private readonly AppDbContext _context;

        // ctor da tanımlayalım
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        //



        public void Commit()
        {
            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}
