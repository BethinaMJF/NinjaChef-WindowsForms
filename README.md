# ?? NinjaChef - Jogo Estilo Fruta Ninja
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

NinjaChef � um jogo inspirado no cl�ssico Fruta Ninja, onde voc� corta ingredientes, ganha pontos, compra espadas e compete no ranking dos melhores jogadores.

---

![Gameplay](video.mp4)

---


## ?? Tecnologias Utilizadas

- **C#** com **Windows Forms**
- **SQL Server** (para gerenciamento de dados do jogo)
- **Arquitetura MVC** (Model, View, Controller)
- **Criptografia de Senha** (garantindo seguran�a no cadastro e login)

---

## ?? Funcionalidades

- **Tela Inicial:** Introdu��o interativa que apresenta as regras do jogo.
- **Cadastro e Login Seguro:** Perfis protegidos por criptografia de senha.
- **Modos de Jogo e Dificuldades:** Escolha entre modo Class�co ou Arcade e n�veis de velocidade.
- **Pausar e Retomar:** Pause o jogo a qualquer momento sem perder o progresso.
- **Mercado de Espadas:** Use os pontos ganhos para adquirir novas espadas.
- **Ranking:** Competi��o entre jogadores baseado na pontua��o acumulada.
- **Personaliza��o de Perfil:** Atualize sua foto de perfil diretamente no menu.
- **Anima��es Din�micas:** Movimentos fluidos e cortes realistas dos ingredientes.
- **Sistema de Corte:** Clique e arraste sobre os itens para realizar cortes precisos.

---

## ?? Instala��o

### 1?? **Clone este reposit�rio**:

```sh
    git clone https://github.com/BethinaMJF/NinjaChef-WindowsForms.git
```

### 2?? **Configure o Banco de Dados**

- Execute o arquivo **script.sql** para criar o banco de dados e suas tabelas.
- No arquivo **App.config**, ajuste a string de conex�o para seu SQL Server:

```xml
<connectionStrings>
    <add name="dbFrutaNinjaEntities" connectionString="Data Source=SEU_SERVIDOR;/>
</connectionStrings>
```

- Substitua **SEU_SERVIDOR** pelo nome do seu servidor SQL Server.

### 3?? **Agora � s� rodar o projeto no Visual Studio e jogar!**


