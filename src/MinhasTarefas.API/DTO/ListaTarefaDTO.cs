namespace MinhasTarefas.API.DTO;

public record ListaTarefaDTO
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public string? Categoria { get; set; }
    public string? Status { get; set; }
    public string? Prioridade { get; set; }
    public string? Responsavel { get; set; }
    public string? Criador { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
}
