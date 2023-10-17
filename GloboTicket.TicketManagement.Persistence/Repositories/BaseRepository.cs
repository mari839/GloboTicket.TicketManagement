using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly GloboTicketDbContext _globoTicketDbContext;
        public BaseRepository(GloboTicketDbContext globoTicketDbContext)
        {
            _globoTicketDbContext = globoTicketDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _globoTicketDbContext.AddAsync(entity);
            await _globoTicketDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _globoTicketDbContext.Remove(entity);
            await _globoTicketDbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _globoTicketDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _globoTicketDbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _globoTicketDbContext.Entry(entity).State = EntityState.Modified;
            await _globoTicketDbContext.SaveChangesAsync();
        }
    }
}
