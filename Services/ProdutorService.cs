using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Produtor;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services
{
    public class ProdutorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProdutorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<ProdutorResponseDto>> FindAll()
        {
            var produtores = await _context.Produtores.ToListAsync();
            return _mapper.Map<ICollection<ProdutorResponseDto>>(produtores);
        }

        public async Task<ProdutorResponseDto> FindById(string id)
        {
            var produtor = await GetById(id);
            return _mapper.Map<ProdutorResponseDto>(produtor);
        }

        public async Task<ProdutorResponseDto> Create(ProdutorCreateDto data)
        {
            ValidarEmailSenha(data.Email, data.Senha);
            await ValidarEmailUnico(data.Email, excludeId: null);

            var produtor = _mapper.Map<Produtor>(data);
            await _context.Produtores.AddAsync(produtor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProdutorResponseDto>(produtor);
        }

        public async Task<ProdutorResponseDto> Update(string id, ProdutorUpdateDto data)
        {
            ValidarEmailSenha(data.Email, data.Senha);

            var produtor = await GetById(id);

            await ValidarEmailUnico(data.Email, excludeId: id);

            _mapper.Map(data, produtor);
            _context.Produtores.Update(produtor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProdutorResponseDto>(produtor);
        }

        public async Task Remove(string id)
        {
            var produtor = await GetById(id);
            _context.Produtores.Remove(produtor);
            await _context.SaveChangesAsync();
        }

        // Busca a entidade ou lança 404
        private async Task<Produtor> GetById(string id)
        {
            var produtor = await _context.Produtores.FirstOrDefaultAsync(p => p.Id == id);

            if (produtor is null)
                throw new ServiceException(
                    $"Produtor #{id} não encontrado",
                    c => c.NotFound(new { message = $"Produtor #{id} não encontrado" })
                );

            return produtor;
        }

        // Regra de negócio: email e senha devem ser ambos preenchidos ou ambos nulos (modo offline)
        private static void ValidarEmailSenha(string? email, string? senha)
        {
            var temEmail = !string.IsNullOrWhiteSpace(email);
            var temSenha = !string.IsNullOrWhiteSpace(senha);

            if (temEmail != temSenha)
                throw new ServiceException(
                    "E-mail e senha devem ser informados juntos",
                    c => c.UnprocessableEntity(new { message = "Informe e-mail e senha juntos, ou deixe ambos em branco (modo offline)" })
                );
        }

        // E-mail é único na base — exclui o próprio registro no update
        private async Task ValidarEmailUnico(string? email, string? excludeId)
        {
            if (string.IsNullOrWhiteSpace(email)) return;

            var emUso = await _context.Produtores
                .AnyAsync(p => p.Email == email && p.Id != excludeId);

            if (emUso)
                throw new ServiceException(
                    "E-mail já cadastrado",
                    c => c.Conflict(new { message = "E-mail já está em uso" })
                );
        }
    }
}
