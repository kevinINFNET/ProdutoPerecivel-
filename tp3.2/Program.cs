public class Produto
{
    private string nome;
    private decimal preco;
    private int quantidade;

 
    public Produto(string nome, decimal preco, int quantidade)
    {
        this.Nome = nome;
        this.Preco = preco;
        this.Quantidade = quantidade;
    }
    public string Nome
    {
        get { return nome; }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                nome = value;
            }
            else
            {
                throw new ArgumentException("Nome não pode ser vazio.");
            }
        }
    }

    public decimal Preco
    {
        get { return preco; }
        set
        {
            if (value >= 0)
            {
                preco = value;
            }
            else
            {
                throw new ArgumentException("Preço não pode ser negativo.");
            }
        }
    }

    public int Quantidade
    {
        get { return quantidade; }
        set
        {
            if (value >= 0)
            {
                quantidade = value;
            }
            else
            {
                throw new ArgumentException("Quantidade não pode ser negativa.");
            }
        }
    }

    public virtual decimal ValorEmEstoque()
    {
        return Preco * Quantidade;
    }
}

public class ProdutoPerecivel : Produto
{
    public DateTime DataDeValidade { get; set; }

    public ProdutoPerecivel(string nome, decimal preco, int quantidade, DateTime dataDeValidade)
        : base(nome, preco, quantidade)
    {
        this.DataDeValidade = dataDeValidade;
    }

    public override decimal ValorEmEstoque()
    {
        decimal valorTotal = base.ValorEmEstoque();
        if (DataDeValidade <= DateTime.Now.AddDays(7))
        {
            valorTotal *= 0.80m;
        }
        return valorTotal;
    }
}

class Program
{
    static void Main()
    {
        ProdutoPerecivel produtoPerecivel = new ProdutoPerecivel("Nesceu", 7.00m, 30, DateTime.Now.AddDays(5));
        Console.WriteLine($"Produto: {produtoPerecivel.Nome}");
        Console.WriteLine($"Preço: {produtoPerecivel.Preco:C}");
        Console.WriteLine($"Quantidade: {produtoPerecivel.Quantidade}");
        Console.WriteLine($"Data de Validade: {produtoPerecivel.DataDeValidade.ToShortDateString()}");
        Console.WriteLine($"Valor em Estoque (com desconto se aplicável): {produtoPerecivel.ValorEmEstoque():C}");
    }
}
