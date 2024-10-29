using MinhasTarefas.API.DTO;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.API.Converter;

public class AtualizaTarefaDTOConverter
{
    public static Tarefa Converter(AtualizaTarefaDTO atualizaTarefaDTO, Tarefa tarefa)
    {
        tarefa.Titulo = atualizaTarefaDTO.Titulo;
        tarefa.Status = (Status)atualizaTarefaDTO.Status;
        tarefa.Prioridade = (Prioridade)atualizaTarefaDTO.Prioridade;
        tarefa.DataConclusao = atualizaTarefaDTO.DataConclusao;

        return tarefa;
    }
}
