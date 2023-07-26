# RPA - Buscar Cursos Alura
O Sistema de automatização, que realiza a operação de pesquizar na pagina principal do site <b>https://www.alura.com.br</b>

### Porque foi desenvolvido?
Esse sistema é resultado de um teste, elaborado pelo time de automação da AeC, e repassado para a realização do desenvolvimento, no qual o desafio é desenvolver um rpa que entre no site de alura, informe uma palavra e realiza a pesquisa, e salvar informações como titulo, descrição, carga horaraia e professo.

### Maior Dificuldade
A maior dficuldade encontrado na automação foi que as informações estavão divididas, no qual não existia a informação de carga horaria e  o professor na tela de resultados.

### Como funciona?
Ele funciona com os seguinte:

1. O sistema abre o navegador do google chrome.
2. Insere a palavra RPA no campo de buscas.
3. Clica no botão pesquisar.
4. Lê as informaçõs de pesquiza, guardando em memória o  titulo e oa descrição.
5. abre cada curso para obter as informações de carga horaria e professor.
6. Chama um web service para salvar os dados em banco!

## Informações do Sistema
Utiliza o Selenium com o IWebDriver para automação, 

- Se precisar pesquisar outra palavra é necessario alterar o parametro que é enviado na tela Program.cs
- É necessaro o Sistema de serviço estja rodando.
- Foram escolhidos o selenium  utilizando o Crhome por conta da poupularidade do shrome.

