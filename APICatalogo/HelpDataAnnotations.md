## Referência de Data Annotations para Desenvolvedores (.NET 8)

As Data Annotations são atributos usados para validação, formatação e definição de regras em propriedades de classes de modelo no .NET. Elas facilitam a validação automática de dados e a geração de esquemas em APIs e aplicações web.

---

## Principais Data Annotations

| Atributo                  | Categoria           | Descrição                                             | Exemplo de Uso                                   |
|---------------------------|---------------------|-------------------------------------------------------|--------------------------------------------------|
| `[Required]`              | Validação           | Torna o campo obrigatório.                            | `[Required] public string Nome { get; set; }`    |
| `[StringLength]`          | Validação           | Define o tamanho máximo e mínimo de uma string.       | `[StringLength(100, MinimumLength = 3)]`         |
| `[Range]`                 | Validação           | Restringe o valor a um intervalo específico.          | `[Range(1, 100)]`                                |
| `[EmailAddress]`          | Validação           | Valida se o valor é um e-mail válido.                 | `[EmailAddress]`                                 |
| `[Key]`                   | Esquema/Banco       | Define a propriedade como chave primária.             | `[Key]`                                          |
| `[ForeignKey]`            | Esquema/Banco       | Define uma chave estrangeira.                         | `[ForeignKey("CategoriaId")]`                    |
| `[Display]`               | Formatação          | Define nome, ordem, prompt, etc. para exibição.       | `[Display(Name = "Nome do Produto")]`            |
| `[DataType]`              | Formatação          | Especifica o tipo de dado (Data, Moeda, etc).         | `[DataType(DataType.Date)]`                      |
| `[NotMapped]`             | Esquema/Banco       | Ignora a propriedade no mapeamento do banco.          | `[NotMapped]`                                    |
| `[RegularExpression]`     | Validação           | Valida usando uma expressão regular.                  | `[RegularExpression(@"^\d{4}$")]`                |

---

## 1. Atributos de Validação

| Atributo                  | Descrição                                             | Exemplo de Uso                                   |
|---------------------------|-------------------------------------------------------|--------------------------------------------------|
| `[Required]`              | Torna o campo obrigatório.                            | `[Required] public string Nome { get; set; }`    |
| `[StringLength]`          | Define o tamanho máximo e mínimo de uma string.       | `[StringLength(100, MinimumLength = 3)]`         |
| `[MaxLength]`             | Define o tamanho máximo de uma string ou array.       | `[MaxLength(50)]`                                |
| `[MinLength]`             | Define o tamanho mínimo de uma string ou array.       | `[MinLength(2)]`                                 |
| `[Range]`                 | Restringe o valor a um intervalo específico.          | `[Range(1, 100)]`                                |
| `[EmailAddress]`          | Valida se o valor é um e-mail válido.                 | `[EmailAddress]`                                 |
| `[Phone]`                 | Valida se o valor é um telefone válido.               | `[Phone]`                                        |
| `[Url]`                   | Valida se o valor é uma URL válida.                   | `[Url]`                                          |
| `[Compare]`               | Compara dois campos para igualdade.                   | `[Compare("Senha")]`                             |
| `[CreditCard]`            | Valida se o valor é um cartão de crédito válido.      | `[CreditCard]`                                   |
| `[RegularExpression]`     | Valida usando uma expressão regular.                  | `[RegularExpression(@"^\d{4}$")]`                |

---

## 2. Atributos de Formatação

| Atributo                  | Descrição                                              | Exemplo de Uso                                   |
|---------------------------|--------------------------------------------------------|--------------------------------------------------|
| `[Display]`               | Define nome, ordem, prompt, etc. para exibição.        | `[Display(Name = "Nome do Produto")]`            |
| `[DisplayFormat]`         | Define formatação para exibição e edição.              | `[DisplayFormat(DataFormatString = "{0:C}")]`    |
| `[DataType]`              | Especifica o tipo de dado (Data, Moeda, etc).          | `[DataType(DataType.Date)]`                      |
| `[ScaffoldColumn]`        | Controla se a coluna será exibida em scaffolding.      | `[ScaffoldColumn(false)]`                        |

---

## 3. Atributos de Esquema (Banco de Dados)

| Atributo                  | Descrição                                              | Exemplo de Uso                                          |
|---------------------------|--------------------------------------------------------|---------------------------------------------------------|
| `[Key]`                   | Define a propriedade como chave primária.              | `[Key]`                                                 |
| `[Timestamp]`             | Marca a propriedade para controle de concorrência.     | `[Timestamp]`                                           |
| `[ForeignKey]`            | Define uma chave estrangeira.                          | `[ForeignKey("CategoriaId")]`                           |
| `[InverseProperty]`       | Define a propriedade de navegação inversa.             | `[InverseProperty("Produtos")]`                         |
| `[NotMapped]`             | Ignora a propriedade no mapeamento do banco.           | `[NotMapped]`                                           |
| `[DatabaseGenerated]`     | Controla a geração do valor (Identity, Computed).      | `[DatabaseGenerated(DatabaseGeneratedOption.Identity)]` |

---

## Dicas Úteis

- Combine múltiplos atributos para validações mais completas.
- Para validações personalizadas, herde de `ValidationAttribute`.
- As Data Annotations são utilizadas pelo model binding, validação e scaffolding no ASP.NET Core.

---

**Referências:**
- [Documentação Oficial: Data Annotations](https://learn.microsoft.com/pt-br/dotnet/api/system.componentmodel.dataannotations)
- [Validação de Modelos no ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/mvc/models/validation)
