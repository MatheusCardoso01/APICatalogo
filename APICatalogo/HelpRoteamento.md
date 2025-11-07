## Restrições de Rota
## Usado para destinguir tipos de parâmetros em rotas que são parecidos

| Restrição                | Exemplo                        | Descrição                                                                 |
|--------------------------|--------------------------------|---------------------------------------------------------------------------|
| int                      | {id:int}                       | O parâmetro deve ser um número inteiro                                    |
| long                     | {id:long}                      | O parâmetro deve ser um número inteiro longo                              |
| float                    | {valor:float}                  | O parâmetro deve ser um número de ponto flutuante                         |
| double                   | {valor:double}                 | O parâmetro deve ser um número double                                     |
| decimal                  | {valor:decimal}                | O parâmetro deve ser um número decimal                                    |
| bool                     | {ativo:bool}                   | O parâmetro deve ser booleano (true/false)                                |
| datetime                 | {data:datetime}                | O parâmetro deve ser uma data/hora válida                                 |
| guid                     | {id:guid}                      | O parâmetro deve ser um GUID válido                                       |
| length(n)                | {nome:length(5)}               | O parâmetro deve ter exatamente n caracteres                              |
| minLength(n)             | {nome:minlength(3)}            | O parâmetro deve ter no mínimo n caracteres                               |
| maxLength(n)             | {nome:maxlength(10)}           | O parâmetro deve ter no máximo n caracteres                               |
| range(min,max)           | {id:range(1,100)}              | O parâmetro deve estar entre min e max                                    |
| alpha                    | {nome:alpha}                   | O parâmetro deve conter apenas letras                                     |
| regex(pattern)           | {codigo:regex(^[A-Z]+$)}       | O parâmetro deve corresponder ao padrão regex informado                   |
| required                 | {nome:required}                | O parâmetro é obrigatório                                                 |
| exists                   | {id:exists}                    | O parâmetro deve existir (usado em rotas customizadas)                    |
| nonempty                 | {nome:nonempty}                | O parâmetro não pode ser vazio                                            |
| min(n)                   | {valor:min(10)}                | O parâmetro deve ser maior ou igual a n                                   |
| max(n)                   | {valor:max(100)}               | O parâmetro deve ser menor ou igual a n                                   |
| in(values)               | {status:in(ativo,inativo)}     | O parâmetro deve ser um dos valores especificados                         |
| email                    | {email:email}                  | O parâmetro deve ser um e-mail válido                                     |
| phone                    | {telefone:phone}               | O parâmetro deve ser um telefone válido                                   |
| url                      | {site:url}                     | O parâmetro deve ser uma URL válida                                       |
| ipv4                     | {ip:ipv4}                      | O parâmetro deve ser um endereço IPv4 válido                              |
| ipv6                     | {ip:ipv6}                      | O parâmetro deve ser um endereço IPv6 válido                              |
| creditcard               | {cartao:creditcard}            | O parâmetro deve ser um número de cartão de crédito válido                |
| uuid                     | {id:uuid}                      | O parâmetro deve ser um UUID válido                                       |
| uppercase                | {sigla:uppercase}              | O parâmetro deve estar em letras maiúsculas                               |
| lowercase                | {sigla:lowercase}              | O parâmetro deve estar em letras minúsculas                               |
| startswith(value)        | {nome:startswith(A)}           | O parâmetro deve começar com o valor especificado                         |
| endswith(value)          | {nome:endswith(Z)}             | O parâmetro deve terminar com o valor especificado                        |
| contains(value)          | {nome:contains(abc)}           | O parâmetro deve conter o valor especificado                              |