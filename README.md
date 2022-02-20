<h1 align="center">
<div align="center">
  <p>
    <strong>Projeto para conversão do modelo de tags exportadas pelo sistema supervisorio Rockwell ME para SE</strong></h1>
  </p>
</div>

## Motivo
Durante o desenvolvimento de um sistema supervisorio para IHM's Rockwell é perceptivel que a forma e formato que são inseridas as tags no sistema é diferente da encontrada para o modelo site edition.
Visto isso, caso seja realizada uma transição dos supervisorios ME para SE, é necessario que se insira tag por tag no programa, o que pode ser uma dor de cabeça!

Dessa forma o presente trabalho teve como objetivo desenvolver um programa com interface que, utilizando dos modelos de exportação e importação em .csv da Rockwell, pudesse criar um arquivo unico de facil importação para o supervisório SE.

*O executavel encontrado na pasta "CreateCSVfileForExport_MEforSE\bin\Release" pode ser utilizado sem necessidade de todo o resto do repositorio.  
