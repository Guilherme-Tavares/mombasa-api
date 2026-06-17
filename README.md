# Agroware Mombasa

## Descrição

API REST para gestão de propriedades rurais dedicadas à pecuária de corte (fases de recria e engorda) em regime semi-intensivo. O nome referencia o capim-mombaça (*Panicum maximum* cv. Mombaça), forrageira amplamente utilizada nesses sistemas no Brasil.

## Objetivo

Fornecer aos produtores rurais uma ferramenta para controle de rebanho, divisões de pastagem, insumos (alimentação e medicamentos), temporadas produtivas e rastreabilidade dos animais.

## Equipe

Guilherme Tavares

## Stack

- **ASP.NET Core 8** Web API
- **Entity Framework Core 8** com Pomelo.EntityFrameworkCore.MySql 8.0.3
- **AutoMapper** 16.1.1
- **Asp.Versioning** 8.1.1 (versionamento por URL)
- **JwtBearer** 8.0.27 (autenticação)
- **Swashbuckle** 6.6.2 (Swagger UI)
- **MySQL 8.0**, banco `agroware_mombasa_legacy`

## Pré-requisitos

- .NET 8 SDK
- MySQL 8.0

## Banco de dados

Execute o script abaixo no MySQL para criar o banco e todas as tabelas:

```sql
CREATE DATABASE agroware_mombasa_legacy;
USE agroware_mombasa_legacy;

-- ============================================
-- TABELA: produtor
-- ============================================
CREATE TABLE produtor
(
    id_produtor CHAR(36) NOT NULL DEFAULT (UUID()),
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE,
    telefone VARCHAR(20),
    senha VARCHAR(256),
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (id_produtor)
);

-- ============================================
-- TABELA: propriedade
-- ============================================
CREATE TABLE propriedade
(
    id_propriedade CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_produtor CHAR(36) NOT NULL,
    nome VARCHAR(100) NOT NULL,
    area_total_hectares DECIMAL(10,2),
    municipio VARCHAR(100),
    estado CHAR(2),
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ativa BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_propriedade),
    FOREIGN KEY (fk_id_produtor) REFERENCES produtor(id_produtor)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- TABELA: divisao
-- ============================================
CREATE TABLE divisao
(
    id_divisao CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_propriedade CHAR(36) NOT NULL,
    nome VARCHAR(50) NOT NULL,
    tipo ENUM('pasto', 'reserva', 'instalacao') NOT NULL DEFAULT 'pasto',
    area_hectares DECIMAL(10,2),
    ativa BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_divisao),
    FOREIGN KEY (fk_id_propriedade) REFERENCES propriedade(id_propriedade)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ============================================
-- TABELA: rebanho
-- ============================================
CREATE TABLE rebanho
(
    id_rebanho CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_propriedade CHAR(36) NOT NULL,
    nome VARCHAR(100) NOT NULL,
    finalidade ENUM('recria', 'engorda', 'misto') NOT NULL,
    data_formacao DATE NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_rebanho),
    FOREIGN KEY (fk_id_propriedade) REFERENCES propriedade(id_propriedade)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- TABELA: bovino
-- ============================================
CREATE TABLE bovino
(
    id_bovino CHAR(36) NOT NULL DEFAULT (UUID()),
    brinco VARCHAR(20) UNIQUE,
    nome VARCHAR(50) NOT NULL,
    sexo ENUM('m', 'f') NOT NULL,
    raca VARCHAR(50),
    data_nascimento DATE,
    peso_atual_kg DECIMAL(6,2),
    data_ultima_pesagem DATE,
    origem ENUM('comprado', 'doacao'),
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_bovino)
);

-- ============================================
-- TABELA: temporada
-- ============================================
CREATE TABLE temporada
(
    id_temporada CHAR(36) NOT NULL DEFAULT (UUID()),
    nome VARCHAR(100) NOT NULL,
    tipo ENUM('aguas', 'seca', 'transicao') NOT NULL,
    data_inicio DATE NOT NULL,
    data_fim DATE NOT NULL,
    
    PRIMARY KEY (id_temporada),
    CHECK (data_fim > data_inicio)
);

-- ============================================
-- TABELA: forragem
-- ============================================
CREATE TABLE forragem
(
    id_forragem CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_divisao CHAR(36) NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    data_plantio DATE,
    ativa BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_forragem),
    FOREIGN KEY (fk_id_divisao) REFERENCES divisao(id_divisao)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ============================================
-- TABELA: cocho
-- ============================================
CREATE TABLE cocho
(
    id_cocho CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_divisao CHAR(36) NOT NULL,
    identificacao VARCHAR(30),
    tipo_material ENUM('madeira', 'concreto', 'plastico', 'metal'),
    capacidade_kg DECIMAL(8,2) NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    
    PRIMARY KEY (id_cocho),
    FOREIGN KEY (fk_id_divisao) REFERENCES divisao(id_divisao)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CHECK (capacidade_kg > 0)
);

-- ============================================
-- TABELA: medicamento
-- ============================================
CREATE TABLE medicamento
(
    id_medicamento CHAR(36) NOT NULL DEFAULT (UUID()),
    nome_comercial VARCHAR(100) NOT NULL UNIQUE,
    principio_ativo VARCHAR(100),
    tipo ENUM('antibiotico', 'antiparasitario', 'vitamina', 'vacina', 'outro') NOT NULL,
    
    PRIMARY KEY (id_medicamento)
);

-- ============================================
-- TABELA: alimento
-- ============================================
CREATE TABLE alimento
(
    id_alimento CHAR(36) NOT NULL DEFAULT (UUID()),
    nome VARCHAR(100) NOT NULL UNIQUE,
    tipo ENUM('racao', 'sal_mineral', 'silagem', 'farelo', 'suplemento', 'outro') NOT NULL,
    
    PRIMARY KEY (id_alimento)
);

-- ============================================
-- TABELA: estoque_medicamento
-- ============================================
CREATE TABLE estoque_medicamento
(
    id_estoque_med CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_propriedade CHAR(36) NOT NULL,
    fk_id_medicamento CHAR(36) NOT NULL,
    quantidade DECIMAL(10,2) NOT NULL,
    unidade ENUM('ml', 'l', 'g', 'kg', 'doses', 'frascos') NOT NULL,
    data_entrada DATE NOT NULL DEFAULT (CURRENT_DATE),
    estoque_minimo DECIMAL(10,2) NOT NULL DEFAULT 0,
    
    PRIMARY KEY (id_estoque_med),
    UNIQUE (fk_id_propriedade, fk_id_medicamento),
    FOREIGN KEY (fk_id_propriedade) REFERENCES propriedade(id_propriedade)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_medicamento) REFERENCES medicamento(id_medicamento)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (quantidade >= 0)
);

-- ============================================
-- TABELA: lotacao (N:N)
-- ============================================
CREATE TABLE lotacao
(
    id_lotacao CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_rebanho CHAR(36) NOT NULL,
    fk_id_divisao CHAR(36) NOT NULL,
    data_entrada DATE NOT NULL,
    data_saida DATE,
    numero_cabecas INT NOT NULL,
    
    PRIMARY KEY (id_lotacao),
    UNIQUE (fk_id_rebanho, fk_id_divisao, data_entrada),
    FOREIGN KEY (fk_id_rebanho) REFERENCES rebanho(id_rebanho)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_divisao) REFERENCES divisao(id_divisao)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (numero_cabecas > 0)
);

-- ============================================
-- TABELA: pertencimento (N:N)
-- ============================================
CREATE TABLE pertencimento
(
    id_pertencimento CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_bovino CHAR(36) NOT NULL,
    fk_id_rebanho CHAR(36) NOT NULL,
    data_entrada DATE NOT NULL,
    data_saida DATE,
    peso_entrada_kg DECIMAL(6,2),
    
    PRIMARY KEY (id_pertencimento),
    UNIQUE (fk_id_bovino, fk_id_rebanho, data_entrada),
    FOREIGN KEY (fk_id_bovino) REFERENCES bovino(id_bovino)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_rebanho) REFERENCES rebanho(id_rebanho)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CHECK (peso_entrada_kg IS NULL OR peso_entrada_kg > 0)
);

-- ============================================
-- TABELA: passagem_temporada (N:N)
-- ============================================
CREATE TABLE passagem_temporada
(
    id_passagem CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_rebanho CHAR(36) NOT NULL,
    fk_id_temporada CHAR(36) NOT NULL,
    peso_medio_inicial_kg DECIMAL(6,2),
    peso_medio_final_kg DECIMAL(6,2),
    gmd_medio_kg DECIMAL(5,3),
    
    PRIMARY KEY (id_passagem),
    UNIQUE (fk_id_rebanho, fk_id_temporada),
    FOREIGN KEY (fk_id_rebanho) REFERENCES rebanho(id_rebanho)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_temporada) REFERENCES temporada(id_temporada)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- TABELA: aplicacao_medicamento (N:N)
-- ============================================
CREATE TABLE aplicacao_medicamento
(
    id_aplicacao CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_bovino CHAR(36) NOT NULL,
    fk_id_medicamento CHAR(36) NOT NULL,
    data_aplicacao DATETIME NOT NULL,
    dose DECIMAL(8,2) NOT NULL,
    unidade_dose ENUM('ml', 'g', 'doses') NOT NULL,
    
    PRIMARY KEY (id_aplicacao),
    FOREIGN KEY (fk_id_bovino) REFERENCES bovino(id_bovino)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_medicamento) REFERENCES medicamento(id_medicamento)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (dose > 0)
);

-- ============================================
-- TABELA: abastecimento_cocho (N:N)
-- ============================================
CREATE TABLE abastecimento_cocho
(
    id_abastecimento CHAR(36) NOT NULL DEFAULT (UUID()),
    fk_id_cocho CHAR(36) NOT NULL,
    fk_id_alimento CHAR(36) NOT NULL,
    data_abastecimento DATETIME NOT NULL,
    quantidade_inicial_kg DECIMAL(8,2) NOT NULL,
    quantidade_restante_kg DECIMAL(8,2) NOT NULL,
    esgotado BOOLEAN NOT NULL DEFAULT FALSE,
    
    PRIMARY KEY (id_abastecimento),
    FOREIGN KEY (fk_id_cocho) REFERENCES cocho(id_cocho)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (fk_id_alimento) REFERENCES alimento(id_alimento)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (quantidade_inicial_kg > 0 AND quantidade_restante_kg >= 0)
);
```

## Configuração

Crie o arquivo `appsettings.Development.json` na raiz do projeto:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=agroware_mombasa_legacy;User=SEU_USUARIO;Password=SUA_SENHA;"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA_MINIMO_32_CHARS_AQUI"
  }
}
```

## Executar

```bash
dotnet run
```

A API sobe em `http://localhost:5000` por padrão. O Swagger UI fica disponível em `/swagger`.

## Autenticação

Dois endpoints não exigem autenticação: `POST /v1/auth/register` e `POST /v1/auth/login`. Todos os demais requerem o header `Authorization: Bearer {token}`.

**Registro:**
```http
POST /v1/auth/register
Content-Type: application/json

{
  "nome": "Guilherme Tavares",
  "email": "produtor@email.com",
  "senha": "senha123"
}
```

**Login:**
```http
POST /v1/auth/login
Content-Type: application/json

{
  "email": "produtor@email.com",
  "senha": "senha123"
}
```

A resposta do login retorna um `token` que deve ser enviado no header `Authorization: Bearer {token}` nas demais requisições.

## Endpoints

A API possui duas versões:

- **v1** — listagem simples (`GET /v1/bovinos`)
- **v2** — listagem paginada e filtrada (`GET /v2/bovinos?page=1&limit=10&search=nome`)

Todas as 16 entidades seguem o padrão REST: `GET /`, `GET /{id}`, `POST /`, `PUT /{id}`, `DELETE /{id}`.

Entidades disponíveis: `produtores`, `propriedades`, `divisoes`, `rebanhos`, `bovinos`, `temporadas`, `forragens`, `cochos`, `medicamentos`, `alimentos`, `estoques-medicamento`, `lotacoes`, `pertencimentos`, `passagens-temporada`, `aplicacoes-medicamento`, `abastecimentos-cocho`.

## Estrutura

```
MombasaAPI/
+-- Controllers/        # 16 controllers + AuthController
|   +-- Filters/        # Filtros de paginação por entidade
+-- Services/           # Regras de negócio
+-- Dtos/               # DTOs de entrada e saída
+-- Profiles/           # Mapeamentos AutoMapper
+-- Models/             # Entidades do banco
+-- DataContexts/       # AppDbContext (EF Core)
+-- Helpers/
|   +-- Paginated/      # Infraestrutura de paginação
+-- Exceptions/         # ServiceException
```

## Contexto acadêmico

Projeto da disciplina de Programação com Acesso a Banco de Dados, IFRO Campus Ji-Paraná, 2026.1.