# 🛡️ Steam Tools Seguro (Organizador de Arquivos de Jogos Steam)

Uma ferramenta flutuante e minimalista para Windows, desenvolvida em C# (.NET 8), que automatiza a cópia de arquivos essenciais para diretórios pré-definidos da Steam usando a funcionalidade de Arrastar e Soltar (Drag & Drop).

A motivação para o desenvolvimento veio da falta de segurança que o Steam Tools transmite e esta ferramenta elimina a necessidade de ter o programa chinês duvidoso instalado.

A forma mais segura de "comprar" seus jogos é não ter nenhuma dessas ferramentas instaladas em sua máquina, baixar diretamente os jogos pelo Ryu e adicionar manualmente os arquivos nas suas respectivas pastas no diretório da Steam sem precisar arrastá-los para o Tools chinês.

Porém isso pode se tornar trabalhoso demais. E aí que entra o "Steam Tools Seguro": ele automatiza esse processo de forma prática e sem comprometer seus dados e a sua segurança.

---

## hid.dll

Um ponto crucial sobre o Steam Tools chinês é a temida `hid.dll`, que gerou uma certa polêmica por conter funções duvidosas, porém a mesma foi limpa pelo mano **[Ciskao](https://www.youtube.com/@ciskao)**, como ele bem mostra em seu canal no YouTube, e divulgada e disponibilizada pelo mano **[Bumyy+](https://www.youtube.com/@maisbumyy)** também em seu canal. Com isso qualquer função duvidosa de acesso à sua rede foi removida, tornando a dll mais segura. Assim você poderá baixar a dll limpa e remover a dll chinesa.

O Steam Tools Seguro também facilita esse trabalho para você, trazendo a `hid.dll` limpa pelo ciskao já embuída no instalador `SteamToolsSeguro.exe`, e o programam irá automaticamente instalar no diretório Steam a dll na primeira vez que você executar o programa e ele identificar a ausência da mesma.

No código, observe o arquivo `steamToolsSeguro.csproj`: A linha `<EmbeddedResource Include="hid.dll" />` diz ao compilador para embutir a hid.dll no executável. Ela está presente aqui nos arquivos do projeto e você poderá conferir ou pedir para algum dev fazê-lo e confirmar que é a mesma dll limpa do ciskao. 

## Funcionalidades

A ferramenta deve funcionar para jogos "comprados" via Ryu (https://generator.ryuu.lol), que vêm em arquivos zipados `.manifest` e `.lua.` 

Tudo que você precisa fazer é extrair esses arquivos, selecionar todos de uma vez e arrastar pra cima do "Steam Tools Seguro".

- **Portátil e leve:** Menos de 1 MB e não há necessidade de instalar o programa em sua máquina.
- **Ícone Flutuante:** Janela minimalista que pode ser arrastada livremente pela tela.
- **Organização por Extensão:** Identifica a extensão do arquivo solto e instala-o no diretório mapeado.
- **Fechamento Fácil:** Clique com o botão direito do mouse no ícone para acessar a opção "Fechar Programa".

## Mapeamento de Arquivos

O programa está configurado para instalar os seguintes arquivos:

| Extensão | Diretório de Destino |
| :--- | :--- |
| `.manifest` | `C:\Program Files (x86)\Steam\config\depotcache` |
| `.lua` | `C:\Program Files (x86)\Steam\config\stplug-in` |

## ⚠️ Observações Importantes!

- Certifique-se de que o **[.NET Desktop Runtime 8.0 (x64)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)** esteja instalado. Caso contrário, o programa solicitará o download automaticamente.
- É necessário que a sua steam esteja instalada no diretório padrão que é (C:\Program Files (x86)\Steam).
- É preciso executar o Steam Tools chinês pelo menos uma vez para que o diretório steam passe a aceitar os jogos. Depois você poderá removê-lo e nada mais precisa ficar instalado em sua máquina.

## Passo a passo detalhado

1. Baixe o **[Steam Tools chinês](https://steamtools.net/download.html)** clicando no primeiro link de download e instale normalmente.
2. Execute uma vez, e imediatamente encerre o programa, desinstale-o e remova a `hid.dll` da pasta `C:\Program Files (x86)\Steam`.
3. Baixe o **[Steam Tools Seguro](https://github.com/marciodinizdev/steamToolsSeguro/releases/tag/v1.0)**
4. Descompacte os arquivos e execute como administrador o `SteamToolsSeguro.exe`. (se a `hid.dll` chinesa foi devidamente removida, o programa instalará a versão limpa da mesma)
5. Acesse o **[Ryu](https://generator.ryuu.lol)**, procure e "compre" o jogo desejado. O ícone flutuante aparecerá no centro da tela.
6. Extraia os arquivos do jogo e arraste-os para o ícone flutuante do Steam Tools Seguro. O programa confirmará a cópia com uma caixa de diálogo.
7. Clique com o botão direito e clique em **Reiniciar Steam**

**Pronto!** Seu jogo estará disponível em sua biblioteca Steam para instalação. 
Você já pode fechar o Steam Tools Seguro, pois não é necessário que ele esteja em execução para o jogo funcionar e nada precisa ficar instalado em seu PC!

## 📥 Último Lançamento

[Baixe a versão mais recente aqui!](https://github.com/marciodinizdev/steamToolsSeguro/releases/tag/v1.0)
