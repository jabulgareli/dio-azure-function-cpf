# Validate CPF Azure Function

Este é um projeto desenvolvido para o curso da **Digital Innovation One (DIO)**. O objetivo é criar uma Azure Function que valida números de CPF (Cadastro de Pessoa Física) utilizando C#.

## Funcionalidades

- Receber um número de CPF via requisição HTTP POST.
- Validar o número de CPF com base no algoritmo oficial.
- Retornar uma resposta indicando se o CPF é válido ou inválido.

## Estrutura do Projeto

- **Function App**: Uma aplicação do Azure Functions baseada em HTTP Trigger.
- **Linguagem**: C#.
- **Validação do CPF**: Implementada seguindo as regras de cálculo dos dígitos verificadores e exclusão de padrões inválidos.

## Como Utilizar

### Pré-requisitos

1. **Azure Functions Core Tools**: Instale as ferramentas de desenvolvimento local para Azure Functions.
2. **.NET SDK**: Certifique-se de ter o SDK do .NET instalado.
3. **Conta Azure**: Para publicar e testar a função na nuvem.

### Configuração Local

1. Clone este repositório:
   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd validate-cpf-azure-function
   ```

2. Instale as dependências:
   ```bash
   dotnet restore
   ```

3. Execute a aplicação localmente:
   ```bash
   func start
   ```

4. Faça uma requisição POST para a função:
   - **URL local**: `http://localhost:7071/api/ValidateCPF`
   - **Exemplo de corpo da requisição**:
     ```json
     {
       "cpf": "123.456.789-09"
     }
     ```

### Publicação no Azure

1. Faça login no Azure CLI:
   ```bash
   az login
   ```

2. Crie um Function App no Azure:
   ```bash
   az functionapp create --resource-group <RESOURCE_GROUP> --consumption-plan-location <LOCATION> --runtime dotnet --functions-version 4 --name <APP_NAME> --storage-account <STORAGE_NAME>
   ```

3. Publique a função:
   ```bash
   func azure functionapp publish <APP_NAME>
   ```

4. Acesse a URL do Function App para testar:
   ```
   https://<APP_NAME>.azurewebsites.net/api/ValidateCPF
   ```

## Exemplo de Resposta

### CPF Válido
```json
{
  "valid": true,
  "message": "CPF is valid."
}
```

### CPF Inválido
```json
{
  "valid": false,
  "message": "CPF is invalid."
}
```

## Aprendizados

Este projeto foi desenvolvido como parte do curso da DIO para demonstrar:

- Criação de Azure Functions.
- Manipulação de dados HTTP no Azure.
- Implementação de algoritmos de validação em C#.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests neste repositório.

## Licença

Este projeto é licenciado sob a [MIT License](LICENSE).
