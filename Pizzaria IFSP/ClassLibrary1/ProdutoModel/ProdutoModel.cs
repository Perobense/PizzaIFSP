using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProdutoModel
    {

        // Tipo =   1 - pizza     2 - esfiha   3 - refrigerante
        // Tamanho = 1 - grande    2 - media    3 - pequena - 4 250 ML - 5 500 ML 6 - 1 Litros 7- 2 Litros 8 - 3 Litros
        // Variação = 1 - inteira   2 - meia
        // SubVarição= 1 - Doce      2 - Salgada
                    
        public int IdFornecedorProduto { get; set; }
        public int IdFonecedor { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool Promocao { get; set; }
        public string Ingredientes { get; set; }
        public int IdProduto { get; set; }
        public int QtdEstoque { get; set; }
        public bool Status { get; set; }
        public byte[] FotoInteira { get; set; }
        public byte[] FotoMeia { get; set; }
        public decimal ValorPromocao { get; set; }
        // TIPO DO PRODUTO PIZZA - ESFIHA - REFRIGERANTE - E ETC
        public int Tipo { get; set; } // 1 - pizza 2- esfiha 3- refrigerante 

        // TAMANHO DO PRODUTO GRANDE - MEDIA - PEQUENA - 500 ML - 1 LITRO E ETC
        public int Tamanho { get; set; } // 1 - grande - 2 media - 3 pequena

        // VARIAÇÕES EXCLUSIVO PARA PIZZAS, PARA DIFERENCIAR DE MEIA E INTEIRA.
        public int Variacao { get; set; } // 1 - inteira  2 - meia

        // SUBVARIAÇÕES , PARA DIFERENCIAR DE DOCE E SALGADA.
        public int SubVariacao { get; set; } // 1 - Doce  2 - Salgada
    }
}
