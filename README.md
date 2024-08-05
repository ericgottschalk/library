# library

Demo -> https://www.youtube.com/watch?v=YJ74yFZDD1c

# Configuração da Aplicação

Este documento fornece os passos necessários para configurar e executar a aplicação. Siga as etapas abaixo para configurar o projeto corretamente.

## Passos para Configuração

### 1. Clonar o Repositório

Primeiramente, clone o repositório para sua máquina local usando o comando:

```bash
git clone https://github.com/ericgottschalk/library
```

### 2. Abrir o Projeto no Visual Studio
### 3. Ajustar a Connection String

Localize o arquivo de configuração (web.config) onde a connection string do banco de dados MySQL está definida.

Substitua a connection string existente pela sua string de conexão para o banco de dados MySQL.

### 4. Definir o Startup Project

No Solution Explorer, clique com o botão direito no projeto da aplicação web (Library.Web).
Selecione Set as Startup Project.

### 5. Atualizar o Banco de Dados

Abra o Package Manager Console em Tools > NuGet Package Manager > Package Manager Console.

Certifique-se de que o projeto de dados (Library.Infrastructure.Data) esteja selecionado no menu suspenso Default Project.

Execute o comando a seguir para criar o banco de dados e aplicar as migrações:

```bash
Update-Database
```

### Acessando a Área Logada
Execute a aplicação.
Navegue até a página de registro.
Crie um novo usuário para acessar as áreas logadas da aplicação.
