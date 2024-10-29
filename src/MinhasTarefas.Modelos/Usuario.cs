using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhasTarefas.Modelos;
public class Usuario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
