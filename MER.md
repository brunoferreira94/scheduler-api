## Modelo de Entidade-Relacionamento (MER) do Sistema de Agendamento

### Entidades

#### 1. Agendamento (Appointment)

- Atributos:
  - ID (Chave Primária)
  - Data e Hora
- Relacionamento: Um agendamento pode estar associado a um ou mais serviços.

#### 2. Serviço (Service)

- Atributos:
  - ID (Chave Primária)
  - Nome do Serviço
- Relacionamento: Um serviço pode estar associado a um ou mais agendamentos.

#### 3. Cliente (Customer)

- Atributos:
  - ID (Chave Primária)
  - Nome do Cliente
  - Email do Cliente

### Relacionamentos

- **Agendamento (Appointment) e Serviço (Service)**

  - Relação N:N (muitos para muitos)
  - Uma tabela de junção (AppointmentService) pode ser usada para representar essa relação.
  - Essa tabela de junção conteria chaves estrangeiras para o Agendamento e o Serviço associados.

- **Agendamento (Appointment) e Cliente (Customer)**
  - Relação 1:N (um para muitos)
  - Um cliente pode ter vários agendamentos, mas cada agendamento pertence a apenas um cliente.
