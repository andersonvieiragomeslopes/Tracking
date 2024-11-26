## Objetivos do Projeto

### Descrição Técnica do Projeto
O projeto é um protótipo de **"Gestão de Ordens de Serviço e Navegação dentro dos prestadores dentro do app"**, com o objetivo de solucionar problemas de navegação e eficiência no fluxo de trabalho, focado nas críticas mais frequentes do app **Auvo** no Google Play.

As funcionalidades incluem:
- **Ordens de Serviço em Tempo Real**: Exibidas em tempo real no mapa do mobile quando criadas pelo painel web.
- **Sistema de Roteirização**: Similar ao Uber ou 99, indicando o percurso que o técnico deve realizar.
- **Problemas Resolvidos**:
  - Consumo excessivo de bateria.
  - Endereços imprecisos que aumentam custos com combustível.

---

### Principais Implementações Técnicas

1. **API para Usuários Anônimos**:
   - Associados a ordens de serviço.
   - Integração com APIs gratuitas de geolocalização para endereços exatos e roteirização.

2. **Gerenciamento de Tarefas em Background**:
   - Uso de `WorkManager` e `BGTaskScheduler`:
     - Melhor eficiência de bateria.
     - Respeito às condições de internet e nível de bateria.

3. **Aprimoramento de Performance**:
   - Paralelismo em pontos críticos no web.
   - **Entity Framework** com SQLite para armazenamento local.
   - Política de retry para garantir estabilidade das requisições.
   - Política de bloqueio a requisições excessivas para manter a estabilidade do backend.
   - Monkey cache para armazenamento dos requests e um banco sqlite para salvar local os resultados. 

4. **Comunicação em Tempo Real**:
   - Uso de `SignalR` para atualizações instantâneas.

5. **Interface de Navegação**:
   - Página de navegação com abas.
   - Reaproveitamento máximo de código entre web, mobile e API.

---

### Visualizações do Projeto

#### Mobile
<div style="display: flex; justify-content: space-between; margin-top: 10px;">
  <img src="https://github.com/user-attachments/assets/d1e3d65a-36c5-4263-af28-3c18d7bf954f" alt="Mobile Image 1" width="45%" />
  <img src="https://github.com/user-attachments/assets/32ef5515-9667-4056-a380-a89c9fb0c250" alt="Mobile Image 2" width="45%" />
</div>

---

#### Backend
<img src="https://github.com/user-attachments/assets/d5a16d85-c21d-4f12-819d-dce28f3c2873" alt="Backend Image" width="100%" />

---

#### Web Admin
<img src="https://github.com/user-attachments/assets/1db57b56-50fa-4cf7-b321-d57b05c9d960" alt="Web Admin Image" width="100%" />

---

### Referências e Observações

- **Boas Práticas no Backend**: [Equinox Project - Eduardo Pires](https://github.com/EduardoPires/EquinoxProject)
- **Problema com iOS**:
  - Um erro local impediu os testes no iOS devido a um problema com o MAUI. A solução exigiu a formatação do Mac. 
  - Mais detalhes: [Issue #25904 no repositório .NET MAUI](https://github.com/dotnet/maui/issues/25904)
