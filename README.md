Objetivos do projeto:

Descrição Técnica do Projeto
O projeto é um protótipo de "Gestão de Ordens de Serviço e Navegação dentro dos prestadores dentro do app", com o objetivo de solucionar problemas de navegação e eficiência no fluxo de trabalho, focado nas críticas mais frequentes do app Auvo no google play.

As ordens de serviço aparecem em tempo real no mapa do mobile quando criadas pelo painel web para que os tecnicos tenham agilidade no trabalho no mobile e um sistema de routeirização (estilo uber, 99 e outros) é feito no mapa com o percurso que o técnico deve fazer. 

Os principais problemas relatados pelos usuários nas lojas são: "o app consome muita bateria", "o endereço não é preciso e faz a gente gastar combustivel".


Apesar de não conseguir efetuar login, identifiquei um problema, o app parece fazer algum tipo de trabalho em segundo plano de forma desnecessária, afinal, sou um usuário não autenticado no sistema, mas ainda assim, recebo a notificação.


Será implementada uma api que gera usuários anonimos
usuários podem ser associados a uma ordem de serviços e com isso, poderao atender a demanda em campo com o uso de geolocalização, o endereço exato será devolvido ao usuário via api's gratuitas de endereço e roteirização para o mapa. 


Workamanger e BGtaskScheduler para melhorar a eficiencia de bateria e garantir que as requisições sejam efetuadas corretamente respeitando as condições de internet, bateria e outros relacionados ao projeto.

Paralelismo em alguns pontos do web para melhorar a performance de carregamento de alguns itens na tela.

Entity framework com sqlite

Politica de retry nos requests

Signalr.

Navigation page com tabs

Reaproveitamente máximo de código entre web, mobile e api.


Backend - referencia de boas práticas: https://github.com/EduardoPires/EquinoxProject

Um problema local com o iOS impediu os testes, sendo necessário formatar o mac para conseguir efetuar o debug corretamente. - https://github.com/dotnet/maui/issues/25904
