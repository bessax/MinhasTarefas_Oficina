using MinhasTarefas.API.DTO;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.API.Converter;

public class ListaTarefaDTOConverter
{
    public static ListaTarefaDTO Converter(Tarefa tarefa)
    {
        return new ListaTarefaDTO
        {
            Titulo = tarefa.Titulo,
            Status = tarefa.Status.ToString(),
            Prioridade = tarefa.Prioridade.ToString(),
            DataCriacao = tarefa.DataCriacao,
            DataConclusao = tarefa.DataConclusao
        };
    }
}
