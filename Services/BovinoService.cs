using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Bovino;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services
{
    public class BovinoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BovinoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<BovinoResponseDto>> FindAll()
        {
            var bovinos = await _context.Bovinos.ToListAsync();
            return _mapper.Map<ICollection<BovinoResponseDto>>(bovinos);
        }

        public async Task<BovinoResponseDto> FindById(string id)
        {
            var bovino = await GetById(id);
            return _mapper.Map<BovinoResponseDto>(bovino);
        }

        public async Task<BovinoResponseDto> Create(BovinoCreateDto data)
        {
            await ValidarBrinco(data.Brinco, excludeId: null);

            var bovino = _mapper.Map<Bovino>(data);
            await _context.Bovinos.AddAsync(bovino);
            await _context.SaveChangesAsync();

            return _mapper.Map<BovinoResponseDto>(bovino);
        }

        public async Task<BovinoResponseDto> Update(string id, BovinoUpdateDto data)
        {
            var bovino = await GetById(id);

            await ValidarBrinco(data.Brinco, excludeId: id);

            _mapper.Map(data, bovino);
            _context.Bovinos.Update(bovino);
            await _context.SaveChangesAsync();

            return _mapper.Map<BovinoResponseDto>(bovino);
        }

        public async Task Remove(string id)
        {
            var bovino = await GetById(id);
            _context.Bovinos.Remove(bovino);
            await _context.SaveChangesAsync();
        }

        // Busca a entidade ou lança 404
        private async Task<Bovino> GetById(string id)
        {
            var bovino = await _context.Bovinos.FirstOrDefaultAsync(b => b.Id == id);

            if (bovino is null)
                throw new ServiceException(
                    $"Bovino #{id} não encontrado",
                    c => c.NotFound(new { message = $"Bovino #{id} não encontrado" })
                );

            return bovino;
        }

        // Brinco é opcional mas único — verifica conflito excluindo o próprio registro no update
        private async Task ValidarBrinco(string? brinco, string? excludeId)
        {
            if (brinco is null) return;

            var emUso = await _context.Bovinos
                .AnyAsync(b => b.Brinco == brinco && b.Id != excludeId);

            if (emUso)
                throw new ServiceException(
                    $"Brinco '{brinco}' já cadastrado",
                    c => c.Conflict(new { message = $"Brinco '{brinco}' já está em uso" })
                );
        }
    }
}
