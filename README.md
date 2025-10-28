# 🛡️ Steam Tools Seguro (Organizador de Arquivos de Jogos Steam)

Uma ferramenta flutuante e minimalista para Windows, desenvolvida em C# (.NET 8), que automatiza a cópia de arquivos essenciais para diretórios pré-definidos da Steam usando a funcionalidade de Arrastar e Soltar (Drag & Drop).

A motivação para o desenvolvimento veio da falta de segurança que o Steam Tools transmite e esta ferramenta elimina a necessidade de ter o programa chinês duvidoso instalado.

Excluindo-se o Steam Tools, a forma mais segura de "comprar" seus jogos é baixar pelo Ryu e adicionar manualmente os arquivos nas suas respectivas pastas no diretório da Steam sem precisar arrastá-los para o Tools chinês. Porém isso pode se tornar trabalhoso demais. E aí que entra o "Steam Tools Seguro": ele automatiza esse processo de forma prática e sem comprometer seus dados e a sua segurança.

---

## ✨ Funcionalidades

A ferramenta deve funcionar para jogos "comprados" via Ryu (https://generator.ryuu.lol), que vêm em arquivos zipados .manifest e .lua. Tudo que você precisa fazer é extrair esses arquivos, selecionar todos de uma vez e arrastar pra cima do "Steam Tools Seguro".

- **Portátil e leve:** Menos de 200 KB e não há necessidade de instalar o programa em sua máquina.
- **Ícone Flutuante:** Janela minimalista (64x64) com fundo transparente, que pode ser arrastada livremente pela tela.
- **Organização por Extensão:** Identifica a extensão do arquivo solto e instala-o no diretório mapeado.
- **Fechamento Fácil:** Clique com o botão direito do mouse no ícone para acessar a opção "Fechar Programa".

## 📂 Mapeamento de Arquivos

O programa está configurado para mover os seguintes arquivos:

| Extensão | Diretório de Destino |
| :--- | :--- |
| `.manifest` | `C:\Program Files (x86)\Steam\depotcache` |
| `.lua` | `C:\Program Files (x86)\Steam\config\stplug-in` |

## 🚀 Como Usar

1.  **Requisitos:** Certifique-se de que o **[.NET Desktop Runtime 8.0 (x64)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)** esteja instalado. Caso contrário, o programa solicitará o download automaticamente.
2.  **Download:** Baixe o arquivo `steamToolsSeguro-v1.0-FDD.zip` na página de [Releases](LINK_PARA_SEU_RELEASE).
3.  **Início:** Descompacte e execute o `steamToolsSeguro.exe`. O ícone flutuante aparecerá no centro da tela.
4.  **Uso:** Arraste e solte arquivos `.manifest` ou `.lua` no ícone. O programa confirmará a cópia com uma caixa de diálogo.

## 📥 Último Lançamento

[Baixe a versão mais recente aqui!](LINK_PARA_SEU_RELEASE) 
**(Substitua esta linha pelo link direto para a página de Releases!)**
