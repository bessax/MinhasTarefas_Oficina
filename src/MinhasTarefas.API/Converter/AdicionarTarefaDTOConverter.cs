using MinhasTarefas.API.DTO;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.API.Converter;

public class AdicionarTarefaDTOConverter
{
    public static Tarefa Converter(AdicionarTarefaDTO adicionarTarefaDTO)
    {
        return new Tarefa
        {
            Titulo = adicionarTarefaDTO.Titulo,
            Status = (Status)adicionarTarefaDTO.Status,
            Prioridade = (Prioridade)adicionarTarefaDTO.Prioridade,
            DataCriacao = adicionarTarefaDTO.DataCriacao,
            DataConclusao = adicionarTarefaDTO.DataConclusao
        };
    }
}
