using Microsoft.AspNetCore.Mvc;

namespace MombasaAPI.Exceptions;

// Exceção lançada na camada de serviço para representar erros de negócio.
// Carrega um delegate que o controller usa para retornar o IActionResult adequado,
// evitando dependência de MVC dentro dos services.
public class ServiceException : Exception
{
    private readonly Func<ControllerBase, IActionResult> _result;

    public ServiceException(string message, Func<ControllerBase, IActionResult> result)
        : base(message)
    {
        _result = result;
    }

    public IActionResult ToActionResult(ControllerBase controller)
        => _result(controller);
}
