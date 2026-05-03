# Endpoints da API - Alocação de Veículos

Base URL: `/api/v1`

## Alocação (`/api/v1/Alocacao`)
- `GET` `/api/v1/Alocacao/listar_todos` - Lista todas as alocações.
- `GET` `/api/v1/Alocacao/buscar/{id}` - Busca uma alocação específica pelo ID.
- `DELETE` `/api/v1/Alocacao/deletar/{id}` - Exclui uma alocação.
- `PUT` `/api/v1/Alocacao/baixa/{id}` - Dá baixa em uma alocação (finaliza locação).

### `POST` `/api/v1/Alocacao/criar`
Cria uma nova alocação.
**Exemplo de Body:**
```json
{
  "clienteId": 1,
  "carroPlaca": "ABC1234",
  "dataRetirada": "2023-10-25T14:30:00Z",
  "dataPrevDevolucao": "2023-10-30T14:30:00Z",
  "valorTotal": 500.00
}
```

### `PUT` `/api/v1/Alocacao/editar/{id}`
Edita uma alocação existente.
**Exemplo de Body:**
```json
{
  "clienteId": 1,
  "carroPlaca": "ABC1234",
  "dataRetirada": "2023-10-25T14:30:00Z",
  "dataPrevDevolucao": "2023-10-31T14:30:00Z",
  "valorTotal": 600.00,
  "status": 1
}
```

## Categoria (`/api/v1/Categoria`)
- `GET` `/api/v1/Categoria/listar_todos` - Lista todas as categorias de veículos.
- `GET` `/api/v1/Categoria/buscar/{id}` - Busca uma categoria específica pelo ID.
- `DELETE` `/api/v1/Categoria/deletar/{id}` - Exclui uma categoria.

### `POST` `/api/v1/Categoria/criar`
Cria uma nova categoria.
**Exemplo de Body:**
```json
{
  "nome": "SUV",
  "descricao": "Utilitário Esportivo",
  "valorDiaria": 150.00
}
```

### `PUT` `/api/v1/Categoria/editar/{id}`
Edita uma categoria existente.
**Exemplo de Body:**
```json
{
  "nome": "SUV Premium",
  "descricao": "Utilitário Esportivo de Luxo",
  "valorDiaria": 200.00,
  "ativo": true
}
```

## Cliente (`/api/v1/Cliente`)
- `GET` `/api/v1/Cliente/listar_todos` - Lista todos os clientes.
- `GET` `/api/v1/Cliente/buscar/{id}` - Busca um cliente específico pelo ID.
- `DELETE` `/api/v1/Cliente/deletar/{id}` - Exclui um cliente.

### `POST` `/api/v1/Cliente/criar`
Cria um novo cliente.
**Exemplo de Body:**
```json
{
  "id": 0,
  "nome": "João Silva",
  "cpf": "12345678900",
  "email": "joao@email.com",
  "senha": "senha_segura",
  "dataDeNascimento": "1990-01-01T00:00:00Z",
  "telefone": "11999999999",
  "endereco": "Rua Exemplo, 123"
}
```

### `PUT` `/api/v1/Cliente/editar/{id}`
Edita um cliente existente.
**Exemplo de Body:**
```json
{
  "nome": "João Silva",
  "cpf": "12345678900",
  "email": "joao@email.com",
  "senha": "nova_senha_segura",
  "dataDeNascimento": "1990-01-01T00:00:00Z",
  "telefone": "11999999999",
  "endereco": "Rua Exemplo, 123",
  "ativo": true
}
```

## Veículo (`/api/v1/Veiculo`)
- `GET` `/api/v1/Veiculo/listar_todos` - Lista todos os veículos.
- `GET` `/api/v1/Veiculo/buscar/{placa}` - Busca um veículo específico pela Placa.
- `DELETE` `/api/v1/Veiculo/deletar/{placa}` - Exclui um veículo.

### `POST` `/api/v1/Veiculo/criar`
Cria um novo veículo.
**Exemplo de Body:**
```json
{
  "placa": "ABC1234",
  "marca": "Toyota",
  "modelo": "Corolla",
  "ano": 2022,
  "cor": "Prata",
  "categoriaId": 1,
  "imagemUrl": "https://exemplo.com/imagem.jpg"
}
```

### `PUT` `/api/v1/Veiculo/editar/{placa}`
Edita um veículo existente.
**Exemplo de Body:**
```json
{
  "marca": "Toyota",
  "modelo": "Corolla",
  "ano": 2022,
  "cor": "Preto",
  "categoriaId": 1,
  "imagemUrl": "https://exemplo.com/imagem_nova.jpg",
  "disponivel": true,
  "ativo": true
}
```
