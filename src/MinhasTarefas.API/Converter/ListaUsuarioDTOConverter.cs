using MinhasTarefas.API.DTO;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.API.Converter;

public class ListaUsuarioDTOConverter
{
    public static ListaUsuarioDTO Converter(Usuario usuario)
    {
        return new ListaUsuarioDTO
        {
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }    
}
