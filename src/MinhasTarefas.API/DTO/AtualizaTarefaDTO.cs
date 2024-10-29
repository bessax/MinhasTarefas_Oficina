namespace MinhasTarefas.API.DTO;

public record AtualizaTarefaDTO
{
    public string? Titulo { get; init; }
    public string? Descricao { get; init; }
    public DateTime? DataConclusao { get; init; }
    public string? Categoria { get; init; }
    public int Status { get; init; }
    public int Prioridade { get; init; }
    public string? Responsavel { get; init; }
    public string? Criador { get; init; }
    public DateTime? DataCriacao { get; init; }
    public DateTime? DataUltimaAtualizacao { get; init; }
}
