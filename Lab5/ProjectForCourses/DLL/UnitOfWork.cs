using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;
using DLL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _db;
        private IRepository<Image> _imageRepository;
        private IRepository<UserImage> _likesFromUserToImageRepository;
        private IRepository<User> _userRepository;

        public UnitOfWork(DbContextOptions<DataContext> options)
        {
            _db = new DataContext(options);
        }
        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new Repository<User>(_db);
                return _userRepository;
            }
        }

        public IRepository<UserImage> LikesFromUserToImage
        {
            get
            {
                if (_likesFromUserToImageRepository == null)
                    _likesFromUserToImageRepository = new Repository<UserImage>(_db);
                return _likesFromUserToImageRepository;
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new Repository<Image>(_db);
                return _imageRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<int> CommitAsync()
        {
            return await _db.SaveChangesAsync();
        }

    }
}
