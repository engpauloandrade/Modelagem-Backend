using System.ComponentModel.DataAnnotations;

namespace Modelagem.src.Models
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public int Id_Cidade { get; set; }
        public Cidade Cidade { get; set; }
    }
}
