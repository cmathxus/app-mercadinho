using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace Mercadinho.models {
    public class Produto {

        public int Id { get; set;}
        public string Nome {get; set;} = "";
        public decimal Preco {get; set;}
        public int Quantidade {get; set;}

        public decimal valorTotalEstoque () {
            return Preco * Quantidade;
        }
    }
}