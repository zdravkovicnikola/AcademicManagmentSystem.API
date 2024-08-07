﻿using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Repository { 

    public class GenericRepository<T> : IGenericRepository<T> where T : class
{

        private readonly AcademicManagmentSystemDbContext _context;

        public GenericRepository(AcademicManagmentSystemDbContext context)
        {
            this._context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity); // ne moze asinhrono
            await _context.SaveChangesAsync();
            }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
          return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
                return null;

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity); // ne moze asinhrono
            await _context.SaveChangesAsync();
        }
}
}
